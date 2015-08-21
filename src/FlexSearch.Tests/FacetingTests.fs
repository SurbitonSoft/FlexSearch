﻿module FacetingTests

open FlexSearch.Core
open Swensen.Unquote
open System
open System.Collections.Generic
open SearchTests

let getFacetQuery indexName = new FacetQuery(indexName)
let withCount count (fq : FacetQuery) = fq.Count <- count; fq
let addGroup (fieldName, value, count) (fq : FacetQuery) = 
    let group = new FacetGroup(fq.IndexName)
    group.Count <- count
    group.FieldValue <- value
    group.FieldName <- fieldName
    fq.GroupBy <- fq.GroupBy |> Array.append [| group |]
    fq
let searchAndExtractFacet (searchService : ISearchService) (fq : FacetQuery) = 
    let result = searchService.Search fq
    test <@ succeeded result @>
    extract result

type ``Faceting Tests``(index : Index, 
                        indexService : IIndexService, 
                        documentService : IDocumentService,
                        searchService : ISearchService) =
    let addFacetField fieldName (index : Index) =
        let ff = new Field(fieldName, FieldDataType.Text)
        ff.AllowFaceting <- true
        index.Fields <- index.Fields |> Array.append [| ff |]
    
    do
        let testData = """
id,category,t1
1,Fish,Salmon
2,Insects,Mosquito
3,Fish,Piranha"""

        index |> addFacetField "category"

        indexTestData(testData, index, indexService, documentService)
    
    member __.``Should be able to use faceting field for normal searches``() =
        let result = 
            getQuery (index.IndexName, "category eq 'Fish'")
            |> withColumns [| "category"; "t1" |]
            |> searchAndExtract searchService
        result |> assertReturnedDocsCount 2
        result |> assertFieldCount 2
        result |> assertFieldValue 0 "category" "Fish"
        result |> assertFieldValue 0 "t1" "Salmon"

    member __.``Should be able to do faceting searches with a facet field``() =
        let result =
            getFacetQuery index.IndexName
            |> addGroup ("category", defString, 10)
            |> searchAndExtractFacet searchService
        test <@ not << String.IsNullOrEmpty <| result @>
        printfn "%s" result