/**
 * Autogenerated by Thrift Compiler (0.9.1)
 *
 * DO NOT EDIT UNLESS YOU ARE SURE THAT YOU KNOW WHAT YOU ARE DOING
 *  @generated
 */
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.IO;
using Thrift;
using Thrift.Collections;
using System.ServiceModel;
using System.Runtime.Serialization;

namespace FlexSearch.Api.Exception
{
  public static class ExceptionConstants
  {
    public static InvalidOperation INDEX_NOT_FOUND = new InvalidOperation();
    public static InvalidOperation INDEX_ALREADY_EXISTS = new InvalidOperation();
    public static InvalidOperation INDEX_SHOULD_BE_OFFLINE = new InvalidOperation();
    public static InvalidOperation INDEX_IS_OFFLINE = new InvalidOperation();
    public static InvalidOperation INDEX_IS_OPENING = new InvalidOperation();
    public static InvalidOperation INDEX_REGISTERATION_MISSING = new InvalidOperation();
    public static InvalidOperation INDEXING_DOCUMENT_ID_MISSING = new InvalidOperation();
    public static InvalidOperation ERROR_OPENING_INDEXWRITER = new InvalidOperation();
    static ExceptionConstants()
    {
      INDEX_NOT_FOUND.DeveloperMessage = "The requested index does not exist.";
      INDEX_NOT_FOUND.UserMessage = "The requested index does not exist.";
      INDEX_NOT_FOUND.ErrorCode = 1000;
      INDEX_ALREADY_EXISTS.DeveloperMessage = "The requested index already exist.";
      INDEX_ALREADY_EXISTS.UserMessage = "The requested index already exist.";
      INDEX_ALREADY_EXISTS.ErrorCode = 1002;
      INDEX_SHOULD_BE_OFFLINE.DeveloperMessage = "Index should be made offline before attempting to update index settings.";
      INDEX_SHOULD_BE_OFFLINE.UserMessage = "Index should be made offline before attempting the operation.";
      INDEX_SHOULD_BE_OFFLINE.ErrorCode = 1003;
      INDEX_IS_OFFLINE.DeveloperMessage = "The index is offline or closing. Please bring the index online to use it.";
      INDEX_IS_OFFLINE.UserMessage = "The index is offline or closing. Please bring the index online to use it.";
      INDEX_IS_OFFLINE.ErrorCode = 1004;
      INDEX_IS_OPENING.DeveloperMessage = "The index is in opening state. Please wait some time before making another request.";
      INDEX_IS_OPENING.UserMessage = "The index is in opening state. Please wait some time before making another request.";
      INDEX_IS_OPENING.ErrorCode = 1005;
      INDEX_REGISTERATION_MISSING.DeveloperMessage = "Registeration information associated with the index is missing.";
      INDEX_REGISTERATION_MISSING.UserMessage = "Registeration information associated with the index is missing.";
      INDEX_REGISTERATION_MISSING.ErrorCode = 1006;
      INDEXING_DOCUMENT_ID_MISSING.DeveloperMessage = "Document id missing.";
      INDEXING_DOCUMENT_ID_MISSING.UserMessage = "Document Id is required in order to index an document. Please specify _documentid and submit the document for indexing.";
      INDEXING_DOCUMENT_ID_MISSING.ErrorCode = 1007;
      ERROR_OPENING_INDEXWRITER.DeveloperMessage = "{To be populated by the developer code}";
      ERROR_OPENING_INDEXWRITER.UserMessage = "Unable to open index writer.";
      ERROR_OPENING_INDEXWRITER.ErrorCode = 1008;
    }
  }
}
