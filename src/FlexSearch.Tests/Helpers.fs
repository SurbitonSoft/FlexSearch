﻿[<AutoOpenAttribute>]
module Helpers

open Fixie
open FlexSearch.Core
open Ploeh.AutoFixture
open Ploeh.AutoFixture.Kernel
open System
open System.Collections.Generic
open System.Linq
open System.IO
open System.Reflection
open Swensen.Unquote


/// <summary>
/// Represents the lookup name for the plug-in
/// </summary>
[<Sealed>]
[<System.AttributeUsage(System.AttributeTargets.Method)>]
type ExampleAttribute(fileName : string, title : string) = 
    inherit Attribute()
    member this.FileName = fileName
    member this.Title = title

[<AttributeUsage(AttributeTargets.Method)>]
type IgnoreAttribute() = inherit Attribute()

module Global =
    let mutable RequestLogPath = String.Empty

[<AutoOpenAttribute>]
module DataHelpers = 
    open Autofac
    open System.Diagnostics
    open Client
    open Microsoft.Extensions.DependencyInjection

    let writer = new TextWriterTraceListener(System.Console.Out)
    Debug.Listeners.Add(writer) |> ignore

    let rootFolder = AppDomain.CurrentDomain.SetupInformation.ApplicationBase

    /// Basic test index with all field types
    let getTestIndex() = 
        let index = new Index(IndexName = Guid.NewGuid().ToString("N"))
        index.IndexConfiguration <- new IndexConfiguration(CommitOnClose = false, AutoCommit = false, AutoRefresh = false)
        index.Active <- true
        index.IndexConfiguration.DirectoryType <- DirectoryType.MemoryMapped
        index.Fields <- [| new Field("b1", FieldDataType.Bool)
                           new Field("b2", FieldDataType.Bool)
                           new Field("d1", FieldDataType.Date)
                           new Field("dt1", FieldDataType.DateTime)
                           new Field("db1", FieldDataType.Double)
                           new Field("et1", FieldDataType.ExactText, AllowSort = true)
                           new Field("h1", FieldDataType.Highlight)
                           new Field("i1", FieldDataType.Int)
                           new Field("i2", FieldDataType.Int, AllowSort = true)
                           new Field("l1", FieldDataType.Long)
                           new Field("t1", FieldDataType.Text)
                           new Field("t2", FieldDataType.Text)
                           new Field("s1", FieldDataType.Stored) |]
        index

    /// Utility method to add data to an index
    let indexTestData (testData : string, index : Index, indexService : IIndexService, 
                       documentService : IDocumentService) = 
        test <@ succeeded <| indexService.AddIndex(index) @>
        let lines = testData.Split([| "\r\n"; "\n" |], StringSplitOptions.RemoveEmptyEntries)
        if lines.Count() < 2 then failwithf "No data to index"
        let headers = lines.[0].Split([| "," |], StringSplitOptions.RemoveEmptyEntries)
        let linesToLoop = lines.Skip(1).ToArray()
        for line in linesToLoop do
            let items = line.Split([| "," |], StringSplitOptions.RemoveEmptyEntries)
            let document = new Document()
            document.Id <- items.[0].Trim()
            document.IndexName <- index.IndexName
            for i in 1..items.Length - 1 do
                document.Fields.Add(headers.[i].Trim(), items.[i].Trim())
            test <@ succeeded <| documentService.AddDocument(document) @>
        test <@ succeeded <| indexService.Refresh(index.IndexName) @>

    let container = Main.setupDependencies true <| Settings.T.GetDefault() <| new ServiceCollection()
    let serverSettings = container.GetService<Settings.T>()
    let handlerModules = container.GetService<Dictionary<string, IHttpHandler>>()
    
    /// <summary>
    /// Basic index configuration
    /// </summary>
    let mockIndexSettings = 
        let index = new Index()
        index.IndexName <- "contact"
        index.IndexConfiguration <- new IndexConfiguration(CommitOnClose = false, AutoCommit = false, AutoRefresh = false)
        index.Active <- true
        index.IndexConfiguration.DirectoryType <- DirectoryType.Ram
        index.Fields <- 
         [| new Field("firstname", FieldDataType.Text)
            new Field("lastname", FieldDataType.Text)
            new Field("email", FieldDataType.ExactText)
            new Field("country", FieldDataType.Text)
            new Field("ipaddress", FieldDataType.ExactText)
            new Field("cvv2", FieldDataType.Int)
            new Field("description", FieldDataType.Highlight)
            new Field("fullname", FieldDataType.Text) |]
        
        let indexService = container.GetService<IIndexService>()
        test <@ indexService.AddIndex(index) |> succeeded @>
        index

    let createDemoIndex = 
        // Make sure the demo index folder is empty
        let folder = Constants.DataFolder + "/country"
        if Directory.Exists(folder) then Directory.Delete(folder, true)
        // Create the demo index
        let demoService = container.GetService<DemoIndexService>()
        test <@ demoService.Setup() |> succeeded @>

    let demoIndexData = container.GetService<DemoIndexService>().DemoData().Value 

    /// Autofixture customizations
    let fixtureCustomization() = 
        let fixture = new Ploeh.AutoFixture.Fixture()
        // We override Auto fixture's string generation mechanism to return this string which will be
        // used as index name
        fixture.Register<String>(fun _ -> Guid.NewGuid().ToString("N"))
        fixture.Register<Index>(fun _ -> getTestIndex()) |> ignore
        fixture.Inject<IIndexService>(container.GetService<IIndexService>()) |> ignore
        fixture.Inject<ISearchService>(container.GetService<ISearchService>()) |> ignore
        fixture.Inject<IDocumentService>(container.GetService<IDocumentService>()) |> ignore
        fixture.Inject<IJobService>(container.GetService<IJobService>()) |> ignore
        fixture.Inject<IQueueService>(container.GetService<IQueueService>()) |> ignore
        fixture.Inject<Dictionary<string, IFlexQueryFunction>>(container.GetService<Dictionary<string, IFlexQueryFunction>>()) |> ignore
        fixture.Inject<Country list>(demoIndexData)
        fixture
     
[<AutoOpenAttribute>]
module ResponseHelpers = 
    let rSucceeded (r : ResponseContext<_>) = 
        match r with
        | SuccessResponse(_) -> true
        | SomeResponse(Ok(_), _, _) -> true
        | _ -> false

// ----------------------------------------------------------------------------
// Convention Section for Fixie
// ----------------------------------------------------------------------------
/// Custom attribute to create parameterised test
[<AttributeUsage(AttributeTargets.Method, AllowMultiple = true)>]
type InlineDataAttribute([<System.ParamArrayAttribute>] parameters : obj []) = 
    inherit Attribute()
    member val Parameters = parameters

type InputParameterSource() = 
    interface ParameterSource with
        member __.GetParameters(methodInfo : MethodInfo) = 
            // Check if the method contains inline data attribute. If not then use AutoFixture
            // to generate input value
            let customAttribute = methodInfo.GetCustomAttributes<InlineDataAttribute>(true)
            if customAttribute.Any() then customAttribute.Select(fun input -> input.Parameters)
            else 
                let fixture = fixtureCustomization()
                let create (builder : ISpecimenBuilder, typ : Type) = (new SpecimenContext(builder)).Resolve(typ)
                let parameterTypes = methodInfo.GetParameters().Select(fun x -> x.ParameterType)
                let parameterValues = parameterTypes.Select(fun x -> create (fixture, x)).ToArray()
                seq { yield parameterValues }

type SingleInstancePerClassConvention() as self = 
    inherit Convention()
    
    let fixtureFactory (typ : Type) = 
        let fixture = fixtureCustomization()
        (new SpecimenContext(fixture)).Resolve(typ)
    
    do
        printfn "RequestLogPath: %A" self.Options.["requestlogpath"]
        if self.Options.["requestlogpath"].Count = 1 then
            Global.RequestLogPath <- self.Options.["requestlogpath"].[0]

        self.Classes.NameEndsWith([| "Tests"; "Test"; "test"; "tests" |]) |> ignore
        // Temporarily ignore parametric tests because Fixie doesn't handle them in VS 2015
        // Comment out this line if you want to also execute ignored tests
        //self.Methods.Where(fun m -> m.HasOrInherits<IgnoreAttribute>() |> not) |> ignore
        self.ClassExecution.CreateInstancePerClass().UsingFactory(fun typ -> fixtureFactory (typ)) |> ignore
        self.Parameters.Add<InputParameterSource>() |> ignore


// Runs a test against the given result and checks if it succeeded
let (?) r = test <@ succeeded r @>
