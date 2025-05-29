using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;
using UnityEngine;
/// <summary>
/// 模拟服务器
/// </summary>
public class NetManager : Singleton<NetManager>
{
    Socket socket;

    byte[] dataByte = new byte[1024];
    public void Start()
    {
        socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        socket.BeginConnect("127.0.0.1", 1668, GameConnectNetHandle, null);
    }

    private void GameConnectNetHandle(IAsyncResult ar)
    {
        try
        {
            socket.EndConnect(ar);
            socket.BeginReceive(dataByte, 0, dataByte.Length, SocketFlags.None, ReceiveHandle, null);
        }
        catch
        {

        }
    }

    private void ReceiveHandle(IAsyncResult ar)
    {
        try
        {

        }
        catch (Exception)
        {

            throw;
        }
    }
    public void SendMessage(int netID, string data)
    {
        byte[] idByte = BitConverter.GetBytes(netID);
        byte[] dByte = Encoding.UTF8.GetBytes(data);
        Buffer.BlockCopy(dataByte, 0, idByte, 0, idByte.Length);
        Buffer.BlockCopy(dataByte, idByte.Length, dByte, 0, dByte.Length);
        socket.BeginSend(dataByte, 0, dataByte.Length, SocketFlags.None, SenHandle, null);
    }
    /// <summary>
    /// 最后一步网络数据封包
    /// </summary>
    /// <param name="tiByte"></param>
    /// <returns></returns>
    byte[] MakeByte(byte[] tiByte)
    {
        using (MyMemoryStream ms = new MyMemoryStream())
        {
            //最终包头
            ms.WriteUShort((ushort)tiByte.Length);
            //最终包体
            ms.Write(tiByte, 0, tiByte.Length);
            ms.Flush();
            return ms.ToArray();
        }
    }
    /// <summary>
    /// 客服端发服务器
    /// </summary>
    /// <param name="netID"></param>
    /// <param name="data"></param>
    public void SendMessage(int netID, byte[] data)
    {
        byte[] idByte = BitConverter.GetBytes(netID);
        byte[] tiByte = new byte[idByte.Length + data.Length];
        //将上面需要发送给服务器数据Byte 镀金客户端处理网络 的Byte数组
        Buffer.BlockCopy(tiByte, 0, idByte, 0, idByte.Length);
        Buffer.BlockCopy(tiByte, idByte.Length, data, 0, data.Length);
        byte[] snedData = MakeByte(tiByte);
        socket.BeginSend(snedData, 0, snedData.Length, SocketFlags.None, SenHandle, null);
    }

    private void SenHandle(IAsyncResult ar)
    {

    }
}
