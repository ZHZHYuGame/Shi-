using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ????
/// </summary>
public struct TestProto : IProto
{
    public int Id;
    public string Name;
    public int Type;
    public double Pirce;
    public readonly ushort ProtoID { get { return 1001; } }
    public byte[] ToArray()
    {
        using (MyMemoryStream ms = new MyMemoryStream())
        {
            ms.WriteUShort(ProtoID);
            ms.WriteInt(Id);
            ms.WriteUString(Name);
            ms.WriteInt(Type);
            ms.WriteFloat((float)Pirce);
            return ms.ToArray();
        }
    }
    public static TestProto GetProto(byte[] buffer)
    {
        TestProto proto = new TestProto();
        using (MyMemoryStream ms = new MyMemoryStream(buffer))
        {
            proto.Id = ms.ReadInt();
            proto.Name = ms.ReadString();
            proto.Type = ms.ReadInt();
            proto.Pirce = ms.ReadFloat();
            return proto;
        }
    }
}
