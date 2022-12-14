// <auto-generated>
//     Generated by the protocol buffer compiler.  DO NOT EDIT!
//     source: product-aggregate.proto
// </auto-generated>
#pragma warning disable 1591, 0612, 3021, 8981
#region Designer generated code

using pb = global::Google.Protobuf;
using pbc = global::Google.Protobuf.Collections;
using pbr = global::Google.Protobuf.Reflection;
using scg = global::System.Collections.Generic;
namespace Ecommerce.Domain.Events.Exported {

  /// <summary>Holder for reflection information generated from product-aggregate.proto</summary>
  public static partial class ProductAggregateReflection {

    #region Descriptor
    /// <summary>File descriptor for product-aggregate.proto</summary>
    public static pbr::FileDescriptor Descriptor {
      get { return descriptor; }
    }
    private static pbr::FileDescriptor descriptor;

    static ProductAggregateReflection() {
      byte[] descriptorData = global::System.Convert.FromBase64String(
          string.Concat(
            "Chdwcm9kdWN0LWFnZ3JlZ2F0ZS5wcm90bxobcHJvZHVjdC1ldmVudC1jcmVh",
            "dGVkLnByb3RvGhtwcm9kdWN0LWV2ZW50LXVwZGF0ZWQucHJvdG8irgEKEFBy",
            "b2R1Y3RBZ2dyZWdhdGUSEQoJZXZlbnRUeXBlGAEgASgJEh0KFWV2ZW50UHJv",
            "Y2Vzc2luZ1RpbWVNcxgCIAEoAxIuCg5wcm9kdWN0Q3JlYXRlZBgDIAEoCzIU",
            "LlByb2R1Y3RDcmVhdGVkRXZlbnRIABIuCg5wcm9kdWN0VXBkYXRlZBgEIAEo",
            "CzIULlByb2R1Y3RVcGRhdGVkRXZlbnRIAEIICgZyZXN1bHRCI6oCIEVjb21t",
            "ZXJjZS5Eb21haW4uRXZlbnRzLkV4cG9ydGVkYgZwcm90bzM="));
      descriptor = pbr::FileDescriptor.FromGeneratedCode(descriptorData,
          new pbr::FileDescriptor[] { global::Ecommerce.Domain.Events.Exported.ProductEventCreatedReflection.Descriptor, global::Ecommerce.Domain.Events.Exported.ProductEventUpdatedReflection.Descriptor, },
          new pbr::GeneratedClrTypeInfo(null, null, new pbr::GeneratedClrTypeInfo[] {
            new pbr::GeneratedClrTypeInfo(typeof(global::Ecommerce.Domain.Events.Exported.ProductAggregate), global::Ecommerce.Domain.Events.Exported.ProductAggregate.Parser, new[]{ "EventType", "EventProcessingTimeMs", "ProductCreated", "ProductUpdated" }, new[]{ "Result" }, null, null, null)
          }));
    }
    #endregion

  }
  #region Messages
  public sealed partial class ProductAggregate : pb::IMessage<ProductAggregate>
  #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
      , pb::IBufferMessage
  #endif
  {
    private static readonly pb::MessageParser<ProductAggregate> _parser = new pb::MessageParser<ProductAggregate>(() => new ProductAggregate());
    private pb::UnknownFieldSet _unknownFields;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public static pb::MessageParser<ProductAggregate> Parser { get { return _parser; } }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public static pbr::MessageDescriptor Descriptor {
      get { return global::Ecommerce.Domain.Events.Exported.ProductAggregateReflection.Descriptor.MessageTypes[0]; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    pbr::MessageDescriptor pb::IMessage.Descriptor {
      get { return Descriptor; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public ProductAggregate() {
      OnConstruction();
    }

    partial void OnConstruction();

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public ProductAggregate(ProductAggregate other) : this() {
      eventType_ = other.eventType_;
      eventProcessingTimeMs_ = other.eventProcessingTimeMs_;
      switch (other.ResultCase) {
        case ResultOneofCase.ProductCreated:
          ProductCreated = other.ProductCreated.Clone();
          break;
        case ResultOneofCase.ProductUpdated:
          ProductUpdated = other.ProductUpdated.Clone();
          break;
      }

      _unknownFields = pb::UnknownFieldSet.Clone(other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public ProductAggregate Clone() {
      return new ProductAggregate(this);
    }

    /// <summary>Field number for the "eventType" field.</summary>
    public const int EventTypeFieldNumber = 1;
    private string eventType_ = "";
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public string EventType {
      get { return eventType_; }
      set {
        eventType_ = pb::ProtoPreconditions.CheckNotNull(value, "value");
      }
    }

    /// <summary>Field number for the "eventProcessingTimeMs" field.</summary>
    public const int EventProcessingTimeMsFieldNumber = 2;
    private long eventProcessingTimeMs_;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public long EventProcessingTimeMs {
      get { return eventProcessingTimeMs_; }
      set {
        eventProcessingTimeMs_ = value;
      }
    }

    /// <summary>Field number for the "productCreated" field.</summary>
    public const int ProductCreatedFieldNumber = 3;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public global::Ecommerce.Domain.Events.Exported.ProductCreatedEvent ProductCreated {
      get { return resultCase_ == ResultOneofCase.ProductCreated ? (global::Ecommerce.Domain.Events.Exported.ProductCreatedEvent) result_ : null; }
      set {
        result_ = value;
        resultCase_ = value == null ? ResultOneofCase.None : ResultOneofCase.ProductCreated;
      }
    }

    /// <summary>Field number for the "productUpdated" field.</summary>
    public const int ProductUpdatedFieldNumber = 4;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public global::Ecommerce.Domain.Events.Exported.ProductUpdatedEvent ProductUpdated {
      get { return resultCase_ == ResultOneofCase.ProductUpdated ? (global::Ecommerce.Domain.Events.Exported.ProductUpdatedEvent) result_ : null; }
      set {
        result_ = value;
        resultCase_ = value == null ? ResultOneofCase.None : ResultOneofCase.ProductUpdated;
      }
    }

    private object result_;
    /// <summary>Enum of possible cases for the "result" oneof.</summary>
    public enum ResultOneofCase {
      None = 0,
      ProductCreated = 3,
      ProductUpdated = 4,
    }
    private ResultOneofCase resultCase_ = ResultOneofCase.None;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public ResultOneofCase ResultCase {
      get { return resultCase_; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public void ClearResult() {
      resultCase_ = ResultOneofCase.None;
      result_ = null;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public override bool Equals(object other) {
      return Equals(other as ProductAggregate);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public bool Equals(ProductAggregate other) {
      if (ReferenceEquals(other, null)) {
        return false;
      }
      if (ReferenceEquals(other, this)) {
        return true;
      }
      if (EventType != other.EventType) return false;
      if (EventProcessingTimeMs != other.EventProcessingTimeMs) return false;
      if (!object.Equals(ProductCreated, other.ProductCreated)) return false;
      if (!object.Equals(ProductUpdated, other.ProductUpdated)) return false;
      if (ResultCase != other.ResultCase) return false;
      return Equals(_unknownFields, other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public override int GetHashCode() {
      int hash = 1;
      if (EventType.Length != 0) hash ^= EventType.GetHashCode();
      if (EventProcessingTimeMs != 0L) hash ^= EventProcessingTimeMs.GetHashCode();
      if (resultCase_ == ResultOneofCase.ProductCreated) hash ^= ProductCreated.GetHashCode();
      if (resultCase_ == ResultOneofCase.ProductUpdated) hash ^= ProductUpdated.GetHashCode();
      hash ^= (int) resultCase_;
      if (_unknownFields != null) {
        hash ^= _unknownFields.GetHashCode();
      }
      return hash;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public override string ToString() {
      return pb::JsonFormatter.ToDiagnosticString(this);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public void WriteTo(pb::CodedOutputStream output) {
    #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
      output.WriteRawMessage(this);
    #else
      if (EventType.Length != 0) {
        output.WriteRawTag(10);
        output.WriteString(EventType);
      }
      if (EventProcessingTimeMs != 0L) {
        output.WriteRawTag(16);
        output.WriteInt64(EventProcessingTimeMs);
      }
      if (resultCase_ == ResultOneofCase.ProductCreated) {
        output.WriteRawTag(26);
        output.WriteMessage(ProductCreated);
      }
      if (resultCase_ == ResultOneofCase.ProductUpdated) {
        output.WriteRawTag(34);
        output.WriteMessage(ProductUpdated);
      }
      if (_unknownFields != null) {
        _unknownFields.WriteTo(output);
      }
    #endif
    }

    #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    void pb::IBufferMessage.InternalWriteTo(ref pb::WriteContext output) {
      if (EventType.Length != 0) {
        output.WriteRawTag(10);
        output.WriteString(EventType);
      }
      if (EventProcessingTimeMs != 0L) {
        output.WriteRawTag(16);
        output.WriteInt64(EventProcessingTimeMs);
      }
      if (resultCase_ == ResultOneofCase.ProductCreated) {
        output.WriteRawTag(26);
        output.WriteMessage(ProductCreated);
      }
      if (resultCase_ == ResultOneofCase.ProductUpdated) {
        output.WriteRawTag(34);
        output.WriteMessage(ProductUpdated);
      }
      if (_unknownFields != null) {
        _unknownFields.WriteTo(ref output);
      }
    }
    #endif

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public int CalculateSize() {
      int size = 0;
      if (EventType.Length != 0) {
        size += 1 + pb::CodedOutputStream.ComputeStringSize(EventType);
      }
      if (EventProcessingTimeMs != 0L) {
        size += 1 + pb::CodedOutputStream.ComputeInt64Size(EventProcessingTimeMs);
      }
      if (resultCase_ == ResultOneofCase.ProductCreated) {
        size += 1 + pb::CodedOutputStream.ComputeMessageSize(ProductCreated);
      }
      if (resultCase_ == ResultOneofCase.ProductUpdated) {
        size += 1 + pb::CodedOutputStream.ComputeMessageSize(ProductUpdated);
      }
      if (_unknownFields != null) {
        size += _unknownFields.CalculateSize();
      }
      return size;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public void MergeFrom(ProductAggregate other) {
      if (other == null) {
        return;
      }
      if (other.EventType.Length != 0) {
        EventType = other.EventType;
      }
      if (other.EventProcessingTimeMs != 0L) {
        EventProcessingTimeMs = other.EventProcessingTimeMs;
      }
      switch (other.ResultCase) {
        case ResultOneofCase.ProductCreated:
          if (ProductCreated == null) {
            ProductCreated = new global::Ecommerce.Domain.Events.Exported.ProductCreatedEvent();
          }
          ProductCreated.MergeFrom(other.ProductCreated);
          break;
        case ResultOneofCase.ProductUpdated:
          if (ProductUpdated == null) {
            ProductUpdated = new global::Ecommerce.Domain.Events.Exported.ProductUpdatedEvent();
          }
          ProductUpdated.MergeFrom(other.ProductUpdated);
          break;
      }

      _unknownFields = pb::UnknownFieldSet.MergeFrom(_unknownFields, other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public void MergeFrom(pb::CodedInputStream input) {
    #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
      input.ReadRawMessage(this);
    #else
      uint tag;
      while ((tag = input.ReadTag()) != 0) {
        switch(tag) {
          default:
            _unknownFields = pb::UnknownFieldSet.MergeFieldFrom(_unknownFields, input);
            break;
          case 10: {
            EventType = input.ReadString();
            break;
          }
          case 16: {
            EventProcessingTimeMs = input.ReadInt64();
            break;
          }
          case 26: {
            global::Ecommerce.Domain.Events.Exported.ProductCreatedEvent subBuilder = new global::Ecommerce.Domain.Events.Exported.ProductCreatedEvent();
            if (resultCase_ == ResultOneofCase.ProductCreated) {
              subBuilder.MergeFrom(ProductCreated);
            }
            input.ReadMessage(subBuilder);
            ProductCreated = subBuilder;
            break;
          }
          case 34: {
            global::Ecommerce.Domain.Events.Exported.ProductUpdatedEvent subBuilder = new global::Ecommerce.Domain.Events.Exported.ProductUpdatedEvent();
            if (resultCase_ == ResultOneofCase.ProductUpdated) {
              subBuilder.MergeFrom(ProductUpdated);
            }
            input.ReadMessage(subBuilder);
            ProductUpdated = subBuilder;
            break;
          }
        }
      }
    #endif
    }

    #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    void pb::IBufferMessage.InternalMergeFrom(ref pb::ParseContext input) {
      uint tag;
      while ((tag = input.ReadTag()) != 0) {
        switch(tag) {
          default:
            _unknownFields = pb::UnknownFieldSet.MergeFieldFrom(_unknownFields, ref input);
            break;
          case 10: {
            EventType = input.ReadString();
            break;
          }
          case 16: {
            EventProcessingTimeMs = input.ReadInt64();
            break;
          }
          case 26: {
            global::Ecommerce.Domain.Events.Exported.ProductCreatedEvent subBuilder = new global::Ecommerce.Domain.Events.Exported.ProductCreatedEvent();
            if (resultCase_ == ResultOneofCase.ProductCreated) {
              subBuilder.MergeFrom(ProductCreated);
            }
            input.ReadMessage(subBuilder);
            ProductCreated = subBuilder;
            break;
          }
          case 34: {
            global::Ecommerce.Domain.Events.Exported.ProductUpdatedEvent subBuilder = new global::Ecommerce.Domain.Events.Exported.ProductUpdatedEvent();
            if (resultCase_ == ResultOneofCase.ProductUpdated) {
              subBuilder.MergeFrom(ProductUpdated);
            }
            input.ReadMessage(subBuilder);
            ProductUpdated = subBuilder;
            break;
          }
        }
      }
    }
    #endif

  }

  #endregion

}

#endregion Designer generated code
