//****************************************************************/
//  Generated by:  ToTypeScriptD
//  Website:       http://github.com/ToTypeScriptD/ToTypeScriptD
//  Version:       v0.1.5081.42854 - SHA1:66c203d - Debug
//  Date:          15/06/2015 01:17:47 PM
//
//  Assemblies:
//    FlexSearch.Core.dll
//
//****************************************************************/

module FlexSearch.Core {

    export class AnalysisRequest {
        Text: string;
    }

    export class Country {
        Id: string;
        CountryName: string;
        Exports: string;
        Imports: string;
        Independence: string;
        MilitaryExpenditure: string;
        NetMigration: string;
        Area: string;
        InternetUsers: string;
        LabourForce: string;
        Population: number;
        AgriProducts: string;
        AreaComparative: string;
        Background: string;
        Capital: string;
        Climate: string;
        Economy: string;
        GovernmentType: string;
        MemberOf: string;
        CountryCode: string;
        Nationality: string;
        Coordinates: string;
    }

    export class CreateResponse {
        Id: string;
    }

    export class CsvIndexingRequest {
        IndexName: string;
        HasHeaderRecord: boolean;
        Headers: string[];
        Path: string;
    }

    enum DirectoryTypeDto {
        Undefined,
        FileSystem,
        MemoryMapped,
        Ram
    }

    export class DocumentDto {
        Fields: string[];
        Id: string;
        TimeStamp: number;
        IndexName: string;
        Highlights: string[];
        Score: number;
        Default: FlexSearch.Core.DocumentDto;
    }

    export class FieldDto {
        FieldName: string;
        Analyze: boolean;
        Index: boolean;
        Store: boolean;
        IndexAnalyzer: string;
        SearchAnalyzer: string;
        FieldType: FieldTypeDto;
        Similarity: FieldSimilarityDto;
        IndexOptions: FieldIndexOptionsDto;
        TermVector: FieldTermVectorDto;
        OmitNorms: boolean;
        ScriptName: string;
    }

    enum FieldIndexOptionsDto {
        Undefined,
        DocsOnly,
        DocsAndFreqs,
        DocsAndFreqsAndPositions,
        DocsAndFreqsAndPositionsAndOffsets
    }

    enum FieldSimilarityDto {
        Undefined,
        BM25,
        TFIDF
    }

    enum FieldTermVectorDto {
        Undefined,
        DoNotStoreTermVector,
        StoreTermVector,
        StoreTermVectorsWithPositions,
        StoreTermVectorsWithPositionsandOffsets
    }

    enum FieldTypeDto {
        Undefined,
        Int,
        Double,
        ExactText,
        Text,
        Highlight,
        Bool,
        Date,
        DateTime,
        Custom,
        Stored,
        Long
    }

    export class HighlightOptionDto {
        FragmentsToReturn: number;
        HighlightedFields: string[];
        PostTag: string;
        PreTag: string;
    }
    
    export class IndexConfigurationDto {
        CommitTimeSeconds: number;
        CommitEveryNFlushes: number;
        CommitOnClose: boolean;
        AutoCommit: boolean;
        DirectoryType: DirectoryTypeDto;
        DefaultWriteLockTimeout: number;
        RamBufferSizeMb: number;
        MaxBufferedDocs: number;
        RefreshTimeMilliseconds: number;
        AutoRefresh: boolean;
        IndexVersion: IndexVersionDto;
        UseBloomFilterForId: boolean;
        DefaultFieldSimilarity: FieldSimilarityDto;
    }

    export class IndexExistsResponse {
        Exists: boolean;
    }

    enum IndexVersionDto {
        Undefined,
        Lucene_4_x_x,
        Lucene_5_0_0
    }

    export class Job {
        JobId: string;
        TotalItems: number;
        ProcessedItems: number;
        FailedItems: number;
        Status: JobStatus;
        Message: string;
    } 

    enum JobStatus {
        Undefined,
        Initializing,
        Initialized,
        InProgress,
        Completed,
        CompletedWithErrors
    }

    export class SearchQueryDto {
        QueryName: string;
        Columns: string[];
        Count: number;
        Highlights: FlexSearch.Core.HighlightOptionDto;
        IndexName: string;
        OrderBy: string;
        CutOff: number;
        DistinctBy: string;
        Skip: number;
        QueryString: string;
        ReturnFlatResult: boolean;
        ReturnScore: boolean;
        SearchProfile: string;
        SearchProfileScript: string;
        OverrideProfileOptions: boolean;
        ReturnEmptyStringForNull: boolean;
    }

    export class SearchResults {
        Documents: FlexSearch.Core.DocumentDto[];
        RecordsReturned: number;
        TotalAvailable: number;
    }

    export class SqlIndexingRequest {
        IndexName: string;
        Query: string;
        ConnectionString: string;
        ForceCreate: boolean;
        CreateJob: boolean;
    }
}

module FlexSearch.DuplicateDetection {

    export class DuplicateDetectionReportRequest {
        SourceFileName: string;
        ProfileName: string;
        IndexName: string;
        QueryString: string;
        CutOff: number;
    }

    export class DuplicateDetectionRequest {
        SelectionQuery: string;
        DisplayName: string;
        ThreadCount: number;
        IndexName: string;
        ProfileName: string;
        MaxRecordsToScan: number;
    }

    export class Session {
        Id: string;
        SessionId: string;
        IndexName: string;
        ProfileName: string;
        JobStartTime: Date;
        JobEndTime: Date;
        SelectionQuery: string;
        DisplayFieldName: string;
        RecordsReturned: number;
        RecordsAvailable: number;
        ThreadCount: number;
    }

    export class SourceRecord {
        SessionId: string;
        SourceId: string;
        SourceRecordId: string;
        SourceDisplayName: string;
        SourceStatus: string;
        TotalDupes: number;
    }

    export class Stats {
        TotalRecords: number;
        MatchedRecords: number;
        NoMatchRecords: number;
        OneMatchRecord: number;
        TwoMatchRecord: number;
        MoreThanTwoMatchRecord: number;
    }

    export class TargetRecord {
        TargetId: string;
        TargetRecordId: string;
        TargetDisplayName: string;
        TrueDuplicate: boolean;
        Quality: string;
        TargetScore: number;
    }

}