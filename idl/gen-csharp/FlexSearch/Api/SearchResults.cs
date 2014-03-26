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
using Thrift.Protocol;
using Thrift.Transport;

namespace FlexSearch.Api
{

  #if !SILVERLIGHT
  [Serializable]
  #endif
  [DataContract(Namespace="")]
  public partial class SearchResults : TBase
  {
    private List<Document> _Documents;
    private int _RecordsReturned;
    private int _TotalAvailable;

    [DataMember(Order = 1)]
    public List<Document> Documents
    {
      get
      {
        return _Documents;
      }
      set
      {
        __isset.Documents = true;
        this._Documents = value;
      }
    }

    [DataMember(Order = 2)]
    public int RecordsReturned
    {
      get
      {
        return _RecordsReturned;
      }
      set
      {
        __isset.RecordsReturned = true;
        this._RecordsReturned = value;
      }
    }

    [DataMember(Order = 3)]
    public int TotalAvailable
    {
      get
      {
        return _TotalAvailable;
      }
      set
      {
        __isset.TotalAvailable = true;
        this._TotalAvailable = value;
      }
    }


    public Isset __isset;
    #if !SILVERLIGHT
    [Serializable]
    #endif
    [DataContract]
    public struct Isset {
      public bool Documents;
      public bool RecordsReturned;
      public bool TotalAvailable;
    }

    public SearchResults() {
      this._Documents = new List<Document>();
      this.__isset.Documents = true;
    }

    public void Read (TProtocol iprot)
    {
      TField field;
      iprot.ReadStructBegin();
      while (true)
      {
        field = iprot.ReadFieldBegin();
        if (field.Type == TType.Stop) { 
          break;
        }
        switch (field.ID)
        {
          case 1:
            if (field.Type == TType.List) {
              {
                Documents = new List<Document>();
                TList _list56 = iprot.ReadListBegin();
                for( int _i57 = 0; _i57 < _list56.Count; ++_i57)
                {
                  Document _elem58 = new Document();
                  _elem58 = new Document();
                  _elem58.Read(iprot);
                  Documents.Add(_elem58);
                }
                iprot.ReadListEnd();
              }
            } else { 
              TProtocolUtil.Skip(iprot, field.Type);
            }
            break;
          case 2:
            if (field.Type == TType.I32) {
              RecordsReturned = iprot.ReadI32();
            } else { 
              TProtocolUtil.Skip(iprot, field.Type);
            }
            break;
          case 3:
            if (field.Type == TType.I32) {
              TotalAvailable = iprot.ReadI32();
            } else { 
              TProtocolUtil.Skip(iprot, field.Type);
            }
            break;
          default: 
            TProtocolUtil.Skip(iprot, field.Type);
            break;
        }
        iprot.ReadFieldEnd();
      }
      iprot.ReadStructEnd();
    }

    public void Write(TProtocol oprot) {
      TStruct struc = new TStruct("SearchResults");
      oprot.WriteStructBegin(struc);
      TField field = new TField();
      if (Documents != null && __isset.Documents) {
        field.Name = "Documents";
        field.Type = TType.List;
        field.ID = 1;
        oprot.WriteFieldBegin(field);
        {
          oprot.WriteListBegin(new TList(TType.Struct, Documents.Count));
          foreach (Document _iter59 in Documents)
          {
            _iter59.Write(oprot);
          }
          oprot.WriteListEnd();
        }
        oprot.WriteFieldEnd();
      }
      if (__isset.RecordsReturned) {
        field.Name = "RecordsReturned";
        field.Type = TType.I32;
        field.ID = 2;
        oprot.WriteFieldBegin(field);
        oprot.WriteI32(RecordsReturned);
        oprot.WriteFieldEnd();
      }
      if (__isset.TotalAvailable) {
        field.Name = "TotalAvailable";
        field.Type = TType.I32;
        field.ID = 3;
        oprot.WriteFieldBegin(field);
        oprot.WriteI32(TotalAvailable);
        oprot.WriteFieldEnd();
      }
      oprot.WriteFieldStop();
      oprot.WriteStructEnd();
    }

    public override bool Equals(object that) {
      var other = that as SearchResults;
      if (other == null) return false;
      if (ReferenceEquals(this, other)) return true;
      return ((__isset.Documents == other.__isset.Documents) && ((!__isset.Documents) || (TCollections.Equals(Documents, other.Documents))))
        && ((__isset.RecordsReturned == other.__isset.RecordsReturned) && ((!__isset.RecordsReturned) || (System.Object.Equals(RecordsReturned, other.RecordsReturned))))
        && ((__isset.TotalAvailable == other.__isset.TotalAvailable) && ((!__isset.TotalAvailable) || (System.Object.Equals(TotalAvailable, other.TotalAvailable))));
    }

    public override int GetHashCode() {
      int hashcode = 0;
      unchecked {
        hashcode = (hashcode * 397) ^ (!__isset.Documents ? 0 : (TCollections.GetHashCode(Documents)));
        hashcode = (hashcode * 397) ^ (!__isset.RecordsReturned ? 0 : (RecordsReturned.GetHashCode()));
        hashcode = (hashcode * 397) ^ (!__isset.TotalAvailable ? 0 : (TotalAvailable.GetHashCode()));
      }
      return hashcode;
    }

    public override string ToString() {
      StringBuilder sb = new StringBuilder("SearchResults(");
      sb.Append("Documents: ");
      sb.Append(Documents);
      sb.Append(",RecordsReturned: ");
      sb.Append(RecordsReturned);
      sb.Append(",TotalAvailable: ");
      sb.Append(TotalAvailable);
      sb.Append(")");
      return sb.ToString();
    }

  }

}
