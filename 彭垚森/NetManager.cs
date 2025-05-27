using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;
using UnityEngine;
using GameMessage;
/// <summary>
/// 客户端Socket连接服务器
/// 1.连接处理
/// 2.接收数据处理
/// 3.发送数据处理
/// </summary>
public class NetManager : Singleton<NetManager>
{
    //网络套接字
    Socket socket;
    //客户端处理网络数据的Byte数组
    byte[] dataByte = new byte[1024];
    public void Start()
    {
        socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        //客户端连接服务请求
        socket.BeginConnect("127.0.0.1", 1668, GameConnectNetHandle, null);
    }
    /// <summary>
    /// 连接服务器请求结果
    /// </summary>
    /// <param name="ar"></param>
    private void GameConnectNetHandle(IAsyncResult ar)
    {
        try
        {
            //连接成功
            socket.EndConnect(ar);
            //客户端接收服务器数据监听
            socket.BeginReceive(dataByte, 0, dataByte.Length, SocketFlags.None, ReceiveHandle, null);
        }
        catch (Exception e)
        {
            //连接异常
            Debug.LogError("连接服务器失败: " + e.Message);
        }

    }
    /// <summary>
    /// 客户端接收服务器数据监听，网络数据处理（线程处理）
    /// </summary>
    /// <param name="ar"></param>
    /// <exception cref="NotImplementedException"></exception>
    private void ReceiveHandle(IAsyncResult ar)
    {
        try
        {

        }
        catch (Exception)
        {

        }
    }
    /// <summary>
    /// 客户端发送数据到服务器
    /// </summary>
    /// <param name="netId">行为</param>
    /// <param name="data">具体做什么</param>
    public void SendMessage(int netId, string data)
    {
        //将ID转换成Byte
        byte[] idByte = BitConverter.GetBytes(netId);
        //将变换数据转换成Byte
        byte[] daByte = Encoding.UTF8.GetBytes(data);

        //将上面需要发送给服务器的数据Byte,读进客户端处理网络数据的Byte数组
        Buffer.BlockCopy(dataByte, 0, idByte, 0, idByte.Length);
        Buffer.BlockCopy(dataByte, idByte.Length, daByte, 0, daByte.Length);

        socket.BeginSend(dataByte, 0, dataByte.Length, SocketFlags.None, SendHandle, null);
    }
    /// <summary>
    /// 客户端发送数据到服务器
    /// </summary>
    /// <param name="netId">行为</param>
    /// <param name="data">具体做什么</param>
    public void SendMessage(int netId, byte[] data)
    {
        //将ID转换成Byte
        byte[] idByte = BitConverter.GetBytes(netId);
        //将上面需要发送给服务器的数据Byte,读进客户端处理网络数据的Byte数组
        Buffer.BlockCopy(dataByte, 0, idByte, 0, idByte.Length);
        Buffer.BlockCopy(dataByte, idByte.Length, data, 0, data.Length);
        socket.BeginSend(dataByte, 0, dataByte.Length, SocketFlags.None, SendHandle, null);
    }
    private void SendHandle(IAsyncResult ar)
    {

    }
}
