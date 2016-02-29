﻿module ScriptTests

open FlexSearch.Api.Model
open FlexSearch.Core
open Swensen.Unquote
open System.Collections.Generic
open System.IO
open System.Linq

type ``Script - Compilation test``() = 
    member __.``Script should compile``() = 
        let scriptSrc = """
#if INTERACTIVE
#I "bin\debug"
#r "ProjectZ.dll"
#load "FileA.fs"
#load "FileB.fs"
#endif

module Script

open System
open FlexSearch.Api.Model
open FlexSearch.Api.Constants
open System.Collections.Generic

let calculate () = 3 + 4
let preIndex(document : Document) = 
    document.Fields.["test"] <- "test"
let preSearchTest(query : SearchQuery) = ()"""
        let fn = Path.GetTempFileName()
        let sn = Path.ChangeExtension(fn, "fsx")
        File.WriteAllText(sn, scriptSrc)
        let result = FSharpCompiler.compile (sn) |> extract
        test <@ result.PreIndexScript.IsSome @>
        test <@ result.PreSearchScripts.Keys.ToArray() = [| "Test" |] @>
        let doc = new Document()
        result.PreIndexScript.Value.Invoke(doc)
        test <@ doc.Fields.["test"] = "test" @>

