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

open FlexLucene.Analysis
open FlexLucene.Analysis.Core
open FlexLucene.Analysis.Miscellaneous
open FlexLucene.Analysis.Standard
open FlexLucene.Analysis.Tokenattributes
open FlexLucene.Analysis.Util
open FlexLucene.Document
open FlexLucene.Index
open FlexLucene.Queries
open FlexLucene.Queryparser.Classic
open FlexLucene.Queryparser.Flexible
open FlexLucene.Search
open FlexLucene.Search.Highlight
open FlexLucene.Search.Postingshighlight
open FlexSearch.Core
open System
open System.Collections.Concurrent
open System.Collections.Generic
open System.ComponentModel.Composition
open System.Linq
open java.io
open java.util

/// Determines whether it's an infinite value and if it's minimum or maximum
type Infinite = 
    | MinInfinite
    | MaxInfinite
    | NoInfinite

[<AutoOpenAttribute>]
module JavaHelpers = 
    // These are needed to satisfy certain Lucene query requirements
    let JavaLongMax = java.lang.Long(java.lang.Long.MAX_VALUE)
    let JavaLongMin = java.lang.Long(java.lang.Long.MIN_VALUE)
    let JavaDoubleMax = java.lang.Double(java.lang.Double.MAX_VALUE)
    let JavaDoubleMin = java.lang.Double(java.lang.Double.MIN_VALUE)
    let JavaFloatMax = java.lang.Float(java.lang.Float.MAX_VALUE)
    let JavaFloatMin = java.lang.Float(java.lang.Float.MIN_VALUE)
    let JavaIntMax = java.lang.Integer(java.lang.Integer.MAX_VALUE)
    let JavaIntMin = java.lang.Integer(java.lang.Integer.MIN_VALUE)
    let javaInt (value : int) = new java.lang.Integer(value)
    let javaDouble (value : double) = new java.lang.Double(value)
    let javaLong (value : int64) = new java.lang.Long(value)
    let javaFloat (value : float32) = new java.lang.Float(value)

    /// Get a new Java hashmap
    let hashMap() = new HashMap()
    
    /// Put an item in the hashmap and continue
    let putC (key, value) (hashMap : HashMap) = 
        hashMap.put (key, value.ToString()) |> ignore
        hashMap
    
    /// Put an item in the hashmap
    let put (key, value) (hashMap : HashMap) = hashMap.put (key, value) |> ignore
    
    let inline getJavaDouble infinite (value : float) = 
        match infinite with
        | MaxInfinite -> JavaDoubleMax
        | MinInfinite -> JavaDoubleMin
        | _ -> java.lang.Double(value)
    
    let inline getJavaInt infinite (value : int) = 
        match infinite with
        | MaxInfinite -> JavaIntMax
        | MinInfinite -> JavaIntMin
        | _ -> java.lang.Integer(value)
    
    let inline getJavaLong infinite (value : int64) = 
        match infinite with
        | MaxInfinite -> JavaLongMax
        | MinInfinite -> JavaLongMin
        | _ -> java.lang.Long(value)
    
    let parseNumber<'T, 'U> (schemaName, dataType) (number : string) (infiniteValue : 'U) 
        (parse : string -> (bool * 'T)) (converter : 'T -> 'U) = 
        if number = Constants.Infinite then ok <| infiniteValue
        else
            match parse number with
            | true, v -> ok <| converter v
            | _ -> fail <| DataCannotBeParsed(schemaName, dataType, number)
            
    let rec getItemAt n (iterator : java.util.Iterator) =
        if n = 0 then iterator.next()
        else if iterator.hasNext() 
             then iterator.next() |> ignore; getItemAt (n-1) iterator
             else null
    let parseDouble (schemaName) (number : string) (infiniteValue : JDouble) = 
        parseNumber<Double, JDouble> (schemaName, "Double") number infiniteValue Double.TryParse javaDouble
    let parseFloat (schemaName) (number : string) (infiniteValue : JFloat) = 
        parseNumber<Single, JFloat> (schemaName, "Float") number infiniteValue Single.TryParse javaFloat
    let parseInt (schemaName) (number : string) (infiniteValue : JInt) = 
        parseNumber<int32, JInt> (schemaName, "Integer") number infiniteValue Int32.TryParse javaInt
    let parseLong (schemaName) (number : string) (infiniteValue : JLong) = 
        parseNumber<int64, JLong> (schemaName, "Long") number infiniteValue Int64.TryParse javaLong

[<AutoOpenAttribute>]
module QueryHelpers = 
    open FlexLucene.Search
    
    type String with
        /// Get term for the given field
        member this.Term(fld : string) = new Term(fld, this)
    
    /// Get term for the given fieldname and value
    let inline getTerm (fieldName : string) (text : string) = new Term(fieldName, text)

    // ----------------------------------------------------------------------------
    // Queries
    // ----------------------------------------------------------------------------
    let inline getMatchAllDocsQuery() = new MatchAllDocsQuery() :> Query
    let inline getMatchNoDocsQuery() = new MatchNoDocsQuery() :> Query
    let inline getBoostQuery (subQuery : Query, boost) = new BoostQuery(subQuery, boost) :> Query
    
    let inline getConstantScoreQuery (subQuery : Query, score) = 
        let q = new ConstantScoreQuery(subQuery) :> Query
        q.SetBoost(score)
        q
    
    let inline getBooleanQuery() = new BooleanQuery()
    let inline getTermQuery fieldName text = new TermQuery(getTerm fieldName text) :> Query
    let inline getFuzzyQuery fieldName slop prefixLength text = 
        new FuzzyQuery((getTerm fieldName text), slop, prefixLength) :> Query
    let inline getPhraseQuery slop = 
        let p = new PhraseQuery()
        p.SetSlop(slop)
        p
    let inline getWildCardQuery fieldName text = new WildcardQuery(getTerm fieldName text) :> Query
    let inline getRegexpQuery fieldName text = new RegexpQuery(getTerm fieldName text) :> Query
    
    // ----------------------------------------------------------------------------
    // Clauses
    // ----------------------------------------------------------------------------
    let inline getBooleanClause (parameters : Dictionary<string, string> option) = 
        match parameters with
        | Some(p) -> 
            match p.TryGetValue("clausetype") with
            | true, b -> 
                match b with
                | InvariantEqual "or" -> BooleanClauseOccur.SHOULD
                | _ -> BooleanClauseOccur.MUST
            | _ -> BooleanClauseOccur.MUST
        | _ -> BooleanClauseOccur.MUST
    
    let inline addBooleanClause inheritedQuery occur (baseQuery : BooleanQuery) = 
        baseQuery.Add(new BooleanClause(inheritedQuery, occur))
        baseQuery
    
    let inline addMustClause inheritedQuery (baseQuery : BooleanQuery) = 
        baseQuery.Add(new BooleanClause(inheritedQuery, BooleanClauseOccur.MUST))
        baseQuery
    
    let inline addMustNotClause inheritedQuery (baseQuery : BooleanQuery) = 
        baseQuery.Add(new BooleanClause(inheritedQuery, BooleanClauseOccur.MUST_NOT))
        baseQuery
    
    let inline addShouldClause inheritedQuery (baseQuery : BooleanQuery) = 
        baseQuery.Add(new BooleanClause(inheritedQuery, BooleanClauseOccur.SHOULD))
        baseQuery
    
    let inline addMatchAllClause (baseQuery : BooleanQuery) = 
        baseQuery.Add(new BooleanClause(getMatchAllDocsQuery(), BooleanClauseOccur.SHOULD))
        baseQuery

    let inline addFilterClause inheritedQuery (baseQuery : BooleanQuery) = 
        baseQuery.Add(new BooleanClause(inheritedQuery, BooleanClauseOccur.FILTER))
        baseQuery
