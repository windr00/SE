﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

// Generated from: LA.proto
namespace PredefinedProto
{
	[global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"LA")]
	public partial class LA : global::ProtoBuf.IExtensible
	{
		public LA() {}
		
		private string _AssetName;
		[global::ProtoBuf.ProtoMember(1, IsRequired = true, Name=@"AssetName", DataFormat = global::ProtoBuf.DataFormat.Default)]
		public string AssetName
		{
			get { return _AssetName; }
			set { _AssetName = value; }
		}
		private global::ProtoBuf.IExtension extensionObject;
		global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
			{ return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
	}
	
}