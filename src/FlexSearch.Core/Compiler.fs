﻿// ----------------------------------------------------------------------------
//  Licensed to FlexSearch under one or more contributor license 
//  agreements. See the NOTICE file distributed with this work 
//  for additional information regarding copyright ownership. 
//
//  This source code is subject to terms and conditions of the 
//  Apache License, Version 2.0. A copy of the license can be 
//  found in the License.txt file at the root of this distribution. 
//  You may also obtain a copy of the License at:
// 
//     http://www.apache.org/licenses/LICENSE-2.0
// 
//  By using this source code in any fashion, you are agreeing
//  to be bound by the terms of the Apache License, Version 2.0.
//
//  You must not remove this notice, or any other, from this software.
// ----------------------------------------------------------------------------
namespace FlexSearch.Core

open System.Collections.Generic

type Scripts = 
    { PreIndexScript : PreIndexDelegate option
      PostSearchScripts : IReadOnlyDictionary<string, PostSearchDelegate>
      PreSearchScripts : IReadOnlyDictionary<string, PreSearchDelegate> }
    static member Default = 
        { PreIndexScript = None
          PostSearchScripts = new Dictionary<string, PostSearchDelegate>()
          PreSearchScripts = new Dictionary<string, PreSearchDelegate>() }

type ScriptType = 
    /// Default signature which is used by computed scripts
    | PreIndexScript of script : PreIndexDelegate
    /// Script which can be used to filter search query results
    /// Format -> searchQuery, id, score, fields -> filtering result * score
    | PostSearchScript of script : PostSearchDelegate
    /// Script to process search query data
    | PreSearchScript of script : PreSearchDelegate

/// Scripts can be used to automate various processing in FlexSearch. Script Type signifies
/// the type of operation that the current script can perform. These can vary from scripts
/// used for computing fields dynamically at index time or scripts which can be used to alter
/// FlexSearch's default scoring.
module FSharpCompiler = 
    open Microsoft.FSharp.Compiler.SimpleSourceCodeServices
    open System.IO
    open System.Reflection
    open System.Linq
    open FlexSearch.Api.Model
    open System
    
    let apiDllPath = Constants.rootFolder +/ "FlexSearch.Api.dll"
    let helpersFile = Constants.ScriptFolder +/ "helpers.fsx"
    
    let generateDelegate (m : MethodInfo) = 
        let parameters = m.GetParameters()
        if m.ReturnType = typeof<System.Void> && parameters.Count() = 1 then 
            if parameters.[0].ParameterType = typeof<Document> && m.Name = "preIndex" then 
                Some <| PreIndexScript(Delegate.CreateDelegate(typeof<PreIndexDelegate>, m) :?> PreIndexDelegate)
            else if parameters.[0].ParameterType = typeof<SearchQuery> && m.Name.StartsWith("preSearch") then 
                Some <| PreSearchScript(Delegate.CreateDelegate(typeof<PreSearchDelegate>, m) :?> PreSearchDelegate)
            else None
        else None
    
    let compile (filePath : string) = 
        let scs = SimpleSourceCodeServices()
        
        let errors, exitCode, dynAssembly = 
            let libFolder = if Directory.Exists <| Constants.rootFolder +/ "Lib"
                            then Constants.rootFolder +/ "Lib"
                            else Constants.rootFolder

            let flags =
                [|  yield "--out:" + Path.GetTempFileName()
                    yield "--target:library"
                    yield "--noframework"
                    yield "--simpleresolution"
                    yield "--optimize-"
                    yield "--fullpaths"
                    yield "--lib:" + libFolder
                    yield helpersFile
                    yield filePath
                    
                    let references =
                        [   yield "System.Runtime"
                            yield apiDllPath
                            yield typeof<System.Object>.Assembly.Location; // mscorlib
                            yield typeof<System.Console>.Assembly.Location; // System.Console
                            yield typeof<System.ComponentModel.DefaultValueAttribute>.Assembly.Location; // System.Runtime
                            yield typeof<System.ComponentModel.PropertyChangedEventArgs>.Assembly.Location; // System.ObjectModel             
                            yield typeof<System.IO.BufferedStream>.Assembly.Location; // System.IO
                            yield typeof<System.Linq.Enumerable>.Assembly.Location; // System.Linq
                            yield typeof<System.Xml.Linq.XDocument>.Assembly.Location; // System.Xml.Linq
                            yield typeof<System.Net.WebRequest>.Assembly.Location; // System.Net.Requests
                            yield typeof<System.Numerics.BigInteger>.Assembly.Location; // System.Runtime.Numerics
                            yield typeof<System.Threading.Tasks.TaskExtensions>.Assembly.Location; // System.Threading.Tasks
                            //yield typeof<Microsoft.FSharp.Core.MeasureAttribute>.Assembly.Location; // FSharp.Core
                            // The reason why I am not taking this FSharp.Core from GAC is that the sigdata and opdata files are not present
                        ]

                    for r in references do
                        yield "-r:" + r
                |]
                 
            scs.CompileToDynamicAssembly(flags, execute = None)
        
        let preSearchScripts = new Dictionary<string, PreSearchDelegate>(StringComparer.InvariantCultureIgnoreCase)
        let mutable preIndexScript = None
        match dynAssembly with
        | Some(asm) -> 
            // Get all the types named Script. Because of F#, the single fsx `Script` module 
            // actually generates 3 types.
            let types = asm.DefinedTypes |> Seq.where (fun t -> t.Name = "Script")
            if types |> Seq.isEmpty then 
                fail << Logger.LogR 
                <| ScriptCannotBeCompiled(filePath, "Script does not contain mandatory 'Script' module.")
            else 
                for t in types do
                    for m in t.GetMethods() do
                        match generateDelegate m with
                        | Some(scriptType) -> 
                            match scriptType with
                            | PreIndexScript(d) -> preIndexScript <- Some d
                            | PreSearchScript(d) -> 
                                let scriptName = m.Name.Replace("preSearch", "")
                                preSearchScripts.Add(scriptName, d)
                            | _ -> ()
                        | _ -> ()
                ok <| { PreIndexScript = preIndexScript
                        PreSearchScripts = preSearchScripts
                        PostSearchScripts = new Dictionary<string, PostSearchDelegate>() }
        | _ -> 
            fail << Logger.LogR <| ScriptCannotBeCompiled(filePath, 
                                                          errors
                                                          |> Array.map (fun x -> sprintf "%s\n" x.Message)
                                                          |> String.Concat)
