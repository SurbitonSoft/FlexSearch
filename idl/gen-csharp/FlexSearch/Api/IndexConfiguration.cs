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
  public partial class IndexConfiguration : TBase
  {
    private int _CommitTimeSec;
    private DirectoryType _DirectoryType;
    private int _DefaultWriteLockTimeout;
    private int _RamBufferSizeMb;
    private int _RefreshTimeMilliSec;
    private IndexVersion _IndexVersion;
    private FieldPostingsFormat _IdFieldPostingsFormat;
    private FieldPostingsFormat _DefaultIndexPostingsFormat;
    private Codec _DefaultCodec;
    private bool _EnableVersioning;
    private FieldSimilarity _DefaultFieldSimilarity;
    private FieldDocValuesFormat _IdFieldDocValuesFormat;
    private FieldDocValuesFormat _DefaultDocValuesFormat;

    [DataMember(Order = 1)]
    public int CommitTimeSec
    {
      get
      {
        return _CommitTimeSec;
      }
      set
      {
        __isset.CommitTimeSec = true;
        this._CommitTimeSec = value;
      }
    }

    /// <summary>
    /// 
    /// <seealso cref="DirectoryType"/>
    /// </summary>
    [DataMember(Order = 2)]
    public DirectoryType DirectoryType
    {
      get
      {
        return _DirectoryType;
      }
      set
      {
        __isset.DirectoryType = true;
        this._DirectoryType = value;
      }
    }

    [DataMember(Order = 3)]
    public int DefaultWriteLockTimeout
    {
      get
      {
        return _DefaultWriteLockTimeout;
      }
      set
      {
        __isset.DefaultWriteLockTimeout = true;
        this._DefaultWriteLockTimeout = value;
      }
    }

    [DataMember(Order = 4)]
    public int RamBufferSizeMb
    {
      get
      {
        return _RamBufferSizeMb;
      }
      set
      {
        __isset.RamBufferSizeMb = true;
        this._RamBufferSizeMb = value;
      }
    }

    [DataMember(Order = 5)]
    public int RefreshTimeMilliSec
    {
      get
      {
        return _RefreshTimeMilliSec;
      }
      set
      {
        __isset.RefreshTimeMilliSec = true;
        this._RefreshTimeMilliSec = value;
      }
    }

    /// <summary>
    /// 
    /// <seealso cref="IndexVersion"/>
    /// </summary>
    [DataMember(Order = 6)]
    public IndexVersion IndexVersion
    {
      get
      {
        return _IndexVersion;
      }
      set
      {
        __isset.IndexVersion = true;
        this._IndexVersion = value;
      }
    }

    /// <summary>
    /// 
    /// <seealso cref="FieldPostingsFormat"/>
    /// </summary>
    [DataMember(Order = 7)]
    public FieldPostingsFormat IdFieldPostingsFormat
    {
      get
      {
        return _IdFieldPostingsFormat;
      }
      set
      {
        __isset.IdFieldPostingsFormat = true;
        this._IdFieldPostingsFormat = value;
      }
    }

    /// <summary>
    /// 
    /// <seealso cref="FieldPostingsFormat"/>
    /// </summary>
    [DataMember(Order = 8)]
    public FieldPostingsFormat DefaultIndexPostingsFormat
    {
      get
      {
        return _DefaultIndexPostingsFormat;
      }
      set
      {
        __isset.DefaultIndexPostingsFormat = true;
        this._DefaultIndexPostingsFormat = value;
      }
    }

    /// <summary>
    /// 
    /// <seealso cref="Codec"/>
    /// </summary>
    [DataMember(Order = 9)]
    public Codec DefaultCodec
    {
      get
      {
        return _DefaultCodec;
      }
      set
      {
        __isset.DefaultCodec = true;
        this._DefaultCodec = value;
      }
    }

    [DataMember(Order = 10)]
    public bool EnableVersioning
    {
      get
      {
        return _EnableVersioning;
      }
      set
      {
        __isset.EnableVersioning = true;
        this._EnableVersioning = value;
      }
    }

    /// <summary>
    /// 
    /// <seealso cref="FieldSimilarity"/>
    /// </summary>
    [DataMember(Order = 11)]
    public FieldSimilarity DefaultFieldSimilarity
    {
      get
      {
        return _DefaultFieldSimilarity;
      }
      set
      {
        __isset.DefaultFieldSimilarity = true;
        this._DefaultFieldSimilarity = value;
      }
    }

    /// <summary>
    /// 
    /// <seealso cref="FieldDocValuesFormat"/>
    /// </summary>
    [DataMember(Order = 12)]
    public FieldDocValuesFormat IdFieldDocValuesFormat
    {
      get
      {
        return _IdFieldDocValuesFormat;
      }
      set
      {
        __isset.IdFieldDocValuesFormat = true;
        this._IdFieldDocValuesFormat = value;
      }
    }

    /// <summary>
    /// 
    /// <seealso cref="FieldDocValuesFormat"/>
    /// </summary>
    [DataMember(Order = 13)]
    public FieldDocValuesFormat DefaultDocValuesFormat
    {
      get
      {
        return _DefaultDocValuesFormat;
      }
      set
      {
        __isset.DefaultDocValuesFormat = true;
        this._DefaultDocValuesFormat = value;
      }
    }


    public Isset __isset;
    #if !SILVERLIGHT
    [Serializable]
    #endif
    [DataContract]
    public struct Isset {
      public bool CommitTimeSec;
      public bool DirectoryType;
      public bool DefaultWriteLockTimeout;
      public bool RamBufferSizeMb;
      public bool RefreshTimeMilliSec;
      public bool IndexVersion;
      public bool IdFieldPostingsFormat;
      public bool DefaultIndexPostingsFormat;
      public bool DefaultCodec;
      public bool EnableVersioning;
      public bool DefaultFieldSimilarity;
      public bool IdFieldDocValuesFormat;
      public bool DefaultDocValuesFormat;
    }

    public IndexConfiguration() {
      this._CommitTimeSec = 60;
      this.__isset.CommitTimeSec = true;
      this._DirectoryType = DirectoryType.MemoryMapped;
      this.__isset.DirectoryType = true;
      this._DefaultWriteLockTimeout = 1000;
      this.__isset.DefaultWriteLockTimeout = true;
      this._RamBufferSizeMb = 100;
      this.__isset.RamBufferSizeMb = true;
      this._RefreshTimeMilliSec = 25;
      this.__isset.RefreshTimeMilliSec = true;
      this._IndexVersion = IndexVersion.Lucene_4_9;
      this.__isset.IndexVersion = true;
      this._IdFieldPostingsFormat = FieldPostingsFormat.Bloom_4_1;
      this.__isset.IdFieldPostingsFormat = true;
      this._DefaultIndexPostingsFormat = FieldPostingsFormat.Lucene_4_1;
      this.__isset.DefaultIndexPostingsFormat = true;
      this._DefaultCodec = Codec.Lucene_4_9;
      this.__isset.DefaultCodec = true;
      this._EnableVersioning = false;
      this.__isset.EnableVersioning = true;
      this._DefaultFieldSimilarity = FieldSimilarity.TFIDF;
      this.__isset.DefaultFieldSimilarity = true;
      this._IdFieldDocValuesFormat = FieldDocValuesFormat.Lucene_4_9;
      this.__isset.IdFieldDocValuesFormat = true;
      this._DefaultDocValuesFormat = FieldDocValuesFormat.Lucene_4_9;
      this.__isset.DefaultDocValuesFormat = true;
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
            if (field.Type == TType.I32) {
              CommitTimeSec = iprot.ReadI32();
            } else { 
              TProtocolUtil.Skip(iprot, field.Type);
            }
            break;
          case 2:
            if (field.Type == TType.I32) {
              DirectoryType = (DirectoryType)iprot.ReadI32();
            } else { 
              TProtocolUtil.Skip(iprot, field.Type);
            }
            break;
          case 3:
            if (field.Type == TType.I32) {
              DefaultWriteLockTimeout = iprot.ReadI32();
            } else { 
              TProtocolUtil.Skip(iprot, field.Type);
            }
            break;
          case 4:
            if (field.Type == TType.I32) {
              RamBufferSizeMb = iprot.ReadI32();
            } else { 
              TProtocolUtil.Skip(iprot, field.Type);
            }
            break;
          case 5:
            if (field.Type == TType.I32) {
              RefreshTimeMilliSec = iprot.ReadI32();
            } else { 
              TProtocolUtil.Skip(iprot, field.Type);
            }
            break;
          case 6:
            if (field.Type == TType.I32) {
              IndexVersion = (IndexVersion)iprot.ReadI32();
            } else { 
              TProtocolUtil.Skip(iprot, field.Type);
            }
            break;
          case 7:
            if (field.Type == TType.I32) {
              IdFieldPostingsFormat = (FieldPostingsFormat)iprot.ReadI32();
            } else { 
              TProtocolUtil.Skip(iprot, field.Type);
            }
            break;
          case 8:
            if (field.Type == TType.I32) {
              DefaultIndexPostingsFormat = (FieldPostingsFormat)iprot.ReadI32();
            } else { 
              TProtocolUtil.Skip(iprot, field.Type);
            }
            break;
          case 9:
            if (field.Type == TType.I32) {
              DefaultCodec = (Codec)iprot.ReadI32();
            } else { 
              TProtocolUtil.Skip(iprot, field.Type);
            }
            break;
          case 10:
            if (field.Type == TType.Bool) {
              EnableVersioning = iprot.ReadBool();
            } else { 
              TProtocolUtil.Skip(iprot, field.Type);
            }
            break;
          case 11:
            if (field.Type == TType.I32) {
              DefaultFieldSimilarity = (FieldSimilarity)iprot.ReadI32();
            } else { 
              TProtocolUtil.Skip(iprot, field.Type);
            }
            break;
          case 12:
            if (field.Type == TType.I32) {
              IdFieldDocValuesFormat = (FieldDocValuesFormat)iprot.ReadI32();
            } else { 
              TProtocolUtil.Skip(iprot, field.Type);
            }
            break;
          case 13:
            if (field.Type == TType.I32) {
              DefaultDocValuesFormat = (FieldDocValuesFormat)iprot.ReadI32();
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
      TStruct struc = new TStruct("IndexConfiguration");
      oprot.WriteStructBegin(struc);
      TField field = new TField();
      if (__isset.CommitTimeSec) {
        field.Name = "CommitTimeSec";
        field.Type = TType.I32;
        field.ID = 1;
        oprot.WriteFieldBegin(field);
        oprot.WriteI32(CommitTimeSec);
        oprot.WriteFieldEnd();
      }
      if (__isset.DirectoryType) {
        field.Name = "DirectoryType";
        field.Type = TType.I32;
        field.ID = 2;
        oprot.WriteFieldBegin(field);
        oprot.WriteI32((int)DirectoryType);
        oprot.WriteFieldEnd();
      }
      if (__isset.DefaultWriteLockTimeout) {
        field.Name = "DefaultWriteLockTimeout";
        field.Type = TType.I32;
        field.ID = 3;
        oprot.WriteFieldBegin(field);
        oprot.WriteI32(DefaultWriteLockTimeout);
        oprot.WriteFieldEnd();
      }
      if (__isset.RamBufferSizeMb) {
        field.Name = "RamBufferSizeMb";
        field.Type = TType.I32;
        field.ID = 4;
        oprot.WriteFieldBegin(field);
        oprot.WriteI32(RamBufferSizeMb);
        oprot.WriteFieldEnd();
      }
      if (__isset.RefreshTimeMilliSec) {
        field.Name = "RefreshTimeMilliSec";
        field.Type = TType.I32;
        field.ID = 5;
        oprot.WriteFieldBegin(field);
        oprot.WriteI32(RefreshTimeMilliSec);
        oprot.WriteFieldEnd();
      }
      if (__isset.IndexVersion) {
        field.Name = "IndexVersion";
        field.Type = TType.I32;
        field.ID = 6;
        oprot.WriteFieldBegin(field);
        oprot.WriteI32((int)IndexVersion);
        oprot.WriteFieldEnd();
      }
      if (__isset.IdFieldPostingsFormat) {
        field.Name = "IdFieldPostingsFormat";
        field.Type = TType.I32;
        field.ID = 7;
        oprot.WriteFieldBegin(field);
        oprot.WriteI32((int)IdFieldPostingsFormat);
        oprot.WriteFieldEnd();
      }
      if (__isset.DefaultIndexPostingsFormat) {
        field.Name = "DefaultIndexPostingsFormat";
        field.Type = TType.I32;
        field.ID = 8;
        oprot.WriteFieldBegin(field);
        oprot.WriteI32((int)DefaultIndexPostingsFormat);
        oprot.WriteFieldEnd();
      }
      if (__isset.DefaultCodec) {
        field.Name = "DefaultCodec";
        field.Type = TType.I32;
        field.ID = 9;
        oprot.WriteFieldBegin(field);
        oprot.WriteI32((int)DefaultCodec);
        oprot.WriteFieldEnd();
      }
      if (__isset.EnableVersioning) {
        field.Name = "EnableVersioning";
        field.Type = TType.Bool;
        field.ID = 10;
        oprot.WriteFieldBegin(field);
        oprot.WriteBool(EnableVersioning);
        oprot.WriteFieldEnd();
      }
      if (__isset.DefaultFieldSimilarity) {
        field.Name = "DefaultFieldSimilarity";
        field.Type = TType.I32;
        field.ID = 11;
        oprot.WriteFieldBegin(field);
        oprot.WriteI32((int)DefaultFieldSimilarity);
        oprot.WriteFieldEnd();
      }
      if (__isset.IdFieldDocValuesFormat) {
        field.Name = "IdFieldDocValuesFormat";
        field.Type = TType.I32;
        field.ID = 12;
        oprot.WriteFieldBegin(field);
        oprot.WriteI32((int)IdFieldDocValuesFormat);
        oprot.WriteFieldEnd();
      }
      if (__isset.DefaultDocValuesFormat) {
        field.Name = "DefaultDocValuesFormat";
        field.Type = TType.I32;
        field.ID = 13;
        oprot.WriteFieldBegin(field);
        oprot.WriteI32((int)DefaultDocValuesFormat);
        oprot.WriteFieldEnd();
      }
      oprot.WriteFieldStop();
      oprot.WriteStructEnd();
    }

    public override bool Equals(object that) {
      var other = that as IndexConfiguration;
      if (other == null) return false;
      if (ReferenceEquals(this, other)) return true;
      return ((__isset.CommitTimeSec == other.__isset.CommitTimeSec) && ((!__isset.CommitTimeSec) || (System.Object.Equals(CommitTimeSec, other.CommitTimeSec))))
        && ((__isset.DirectoryType == other.__isset.DirectoryType) && ((!__isset.DirectoryType) || (System.Object.Equals(DirectoryType, other.DirectoryType))))
        && ((__isset.DefaultWriteLockTimeout == other.__isset.DefaultWriteLockTimeout) && ((!__isset.DefaultWriteLockTimeout) || (System.Object.Equals(DefaultWriteLockTimeout, other.DefaultWriteLockTimeout))))
        && ((__isset.RamBufferSizeMb == other.__isset.RamBufferSizeMb) && ((!__isset.RamBufferSizeMb) || (System.Object.Equals(RamBufferSizeMb, other.RamBufferSizeMb))))
        && ((__isset.RefreshTimeMilliSec == other.__isset.RefreshTimeMilliSec) && ((!__isset.RefreshTimeMilliSec) || (System.Object.Equals(RefreshTimeMilliSec, other.RefreshTimeMilliSec))))
        && ((__isset.IndexVersion == other.__isset.IndexVersion) && ((!__isset.IndexVersion) || (System.Object.Equals(IndexVersion, other.IndexVersion))))
        && ((__isset.IdFieldPostingsFormat == other.__isset.IdFieldPostingsFormat) && ((!__isset.IdFieldPostingsFormat) || (System.Object.Equals(IdFieldPostingsFormat, other.IdFieldPostingsFormat))))
        && ((__isset.DefaultIndexPostingsFormat == other.__isset.DefaultIndexPostingsFormat) && ((!__isset.DefaultIndexPostingsFormat) || (System.Object.Equals(DefaultIndexPostingsFormat, other.DefaultIndexPostingsFormat))))
        && ((__isset.DefaultCodec == other.__isset.DefaultCodec) && ((!__isset.DefaultCodec) || (System.Object.Equals(DefaultCodec, other.DefaultCodec))))
        && ((__isset.EnableVersioning == other.__isset.EnableVersioning) && ((!__isset.EnableVersioning) || (System.Object.Equals(EnableVersioning, other.EnableVersioning))))
        && ((__isset.DefaultFieldSimilarity == other.__isset.DefaultFieldSimilarity) && ((!__isset.DefaultFieldSimilarity) || (System.Object.Equals(DefaultFieldSimilarity, other.DefaultFieldSimilarity))))
        && ((__isset.IdFieldDocValuesFormat == other.__isset.IdFieldDocValuesFormat) && ((!__isset.IdFieldDocValuesFormat) || (System.Object.Equals(IdFieldDocValuesFormat, other.IdFieldDocValuesFormat))))
        && ((__isset.DefaultDocValuesFormat == other.__isset.DefaultDocValuesFormat) && ((!__isset.DefaultDocValuesFormat) || (System.Object.Equals(DefaultDocValuesFormat, other.DefaultDocValuesFormat))));
    }

    public override int GetHashCode() {
      int hashcode = 0;
      unchecked {
        hashcode = (hashcode * 397) ^ (!__isset.CommitTimeSec ? 0 : (CommitTimeSec.GetHashCode()));
        hashcode = (hashcode * 397) ^ (!__isset.DirectoryType ? 0 : (DirectoryType.GetHashCode()));
        hashcode = (hashcode * 397) ^ (!__isset.DefaultWriteLockTimeout ? 0 : (DefaultWriteLockTimeout.GetHashCode()));
        hashcode = (hashcode * 397) ^ (!__isset.RamBufferSizeMb ? 0 : (RamBufferSizeMb.GetHashCode()));
        hashcode = (hashcode * 397) ^ (!__isset.RefreshTimeMilliSec ? 0 : (RefreshTimeMilliSec.GetHashCode()));
        hashcode = (hashcode * 397) ^ (!__isset.IndexVersion ? 0 : (IndexVersion.GetHashCode()));
        hashcode = (hashcode * 397) ^ (!__isset.IdFieldPostingsFormat ? 0 : (IdFieldPostingsFormat.GetHashCode()));
        hashcode = (hashcode * 397) ^ (!__isset.DefaultIndexPostingsFormat ? 0 : (DefaultIndexPostingsFormat.GetHashCode()));
        hashcode = (hashcode * 397) ^ (!__isset.DefaultCodec ? 0 : (DefaultCodec.GetHashCode()));
        hashcode = (hashcode * 397) ^ (!__isset.EnableVersioning ? 0 : (EnableVersioning.GetHashCode()));
        hashcode = (hashcode * 397) ^ (!__isset.DefaultFieldSimilarity ? 0 : (DefaultFieldSimilarity.GetHashCode()));
        hashcode = (hashcode * 397) ^ (!__isset.IdFieldDocValuesFormat ? 0 : (IdFieldDocValuesFormat.GetHashCode()));
        hashcode = (hashcode * 397) ^ (!__isset.DefaultDocValuesFormat ? 0 : (DefaultDocValuesFormat.GetHashCode()));
      }
      return hashcode;
    }

    public override string ToString() {
      StringBuilder sb = new StringBuilder("IndexConfiguration(");
      sb.Append("CommitTimeSec: ");
      sb.Append(CommitTimeSec);
      sb.Append(",DirectoryType: ");
      sb.Append(DirectoryType);
      sb.Append(",DefaultWriteLockTimeout: ");
      sb.Append(DefaultWriteLockTimeout);
      sb.Append(",RamBufferSizeMb: ");
      sb.Append(RamBufferSizeMb);
      sb.Append(",RefreshTimeMilliSec: ");
      sb.Append(RefreshTimeMilliSec);
      sb.Append(",IndexVersion: ");
      sb.Append(IndexVersion);
      sb.Append(",IdFieldPostingsFormat: ");
      sb.Append(IdFieldPostingsFormat);
      sb.Append(",DefaultIndexPostingsFormat: ");
      sb.Append(DefaultIndexPostingsFormat);
      sb.Append(",DefaultCodec: ");
      sb.Append(DefaultCodec);
      sb.Append(",EnableVersioning: ");
      sb.Append(EnableVersioning);
      sb.Append(",DefaultFieldSimilarity: ");
      sb.Append(DefaultFieldSimilarity);
      sb.Append(",IdFieldDocValuesFormat: ");
      sb.Append(IdFieldDocValuesFormat);
      sb.Append(",DefaultDocValuesFormat: ");
      sb.Append(DefaultDocValuesFormat);
      sb.Append(")");
      return sb.ToString();
    }

  }

}
