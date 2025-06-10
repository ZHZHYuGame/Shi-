using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 主协议
/// </summary>
public struct MainProto : IProto
{
    public int Id;
    public string Name;
    public int Type;
    public double Pirce;
    public readonly ushort ProtoID { get { return 1002; } }
    public bool isSucces;
    public int ErrorCode;

    public byte[] ToArray()
    {
        using (MyMemoryStream ms = new MyMemoryStream())
        {
            ms.WriteUShort(ProtoID);
            ms.WriteInt(Id);
            ms.WriteUString(Name);
            ms.WriteInt(Type);
            ms.WriteFloat((float)Pirce);
            ms.WriteBool(isSucces);
            ms.WriteInt(ErrorCode);
            return ms.ToArray();
        }
    }
    public static MainProto GetProto(byte[] buffer)
    {
        MainProto proto = new MainProto();
        using (MyMemoryStream ms = new MyMemoryStream(buffer))
        {
            proto.Id = ms.ReadInt();
            proto.Name = ms.ReadString();
            proto.Type = ms.ReadInt();
            proto.Pirce = ms.ReadFloat();
            proto.isSucces = ms.ReadBool();
            proto.ErrorCode = ms.ReadInt();
            return proto;
        }
    }
}