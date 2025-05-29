using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;
using UnityEngine;
/// <summary>
/// 客户端Socket连接服务器
/// 1.连接处理
/// 2.接收数据处理
/// 3.发送数据处理
/// </summary>
public class NetManager : Singleton<NetManager>
{
    /// <summary>
    /// 网络 套接字
    /// </summary>
    Socket st;
    /// <summary>
    /// 客户端处理网络数据的Byte数组
    /// </summary>
    Byte[] dataByte=new byte[1024];
    public void Strat()
    {
        //AddressFamily.InterNetwork：表示使用 IPv4 地址族
        //SocketType.Stream：表示面向连接的可靠字节流套接字，通常用于 TCP 协议
        //ProtocolType.Tcp：表示使用 TCP 协议
        st = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

        //客户端链接服务请求
        //BeginConnect(用于异步启动一个连接到远程主机的请求)
        //"127.0.0.1"：表示连接到本地主机的IP地址（即本机）
        //1668：表示要连接的服务器端口号
        //GameConnectNetHandle：连接完成时调用的回调方法
        //null：没有传递额外的状态对象
        st.BeginConnect("127.0.0.1",1668,GameConnectNetHandle,null);
    }
    /// <summary>
    /// 连接服务器请求
    /// </summary>
    /// <param name="ar"></param>
    private void GameConnectNetHandle(IAsyncResult ar)
    {
        try
        {
            //链接成功
            st.EndConnect(ar);
            //客户端接受服务器数据监听
            //dataByte 是接收数据的缓冲区
            //0 是缓冲区的起始偏移量
            //dataByte.Length 是接收的最大字节数
            //SocketFlags.None 表示不使用特殊选项
            st.BeginReceive(dataByte,0,dataByte.Length,SocketFlags.None,ReceiveHandle,null);
        }
        catch(Exception)  
        {
            //连接异常
        }
    }
    /// <summary>
    /// 客户端连接服务器数据监听，网络数据处理（线程处理）
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

            throw;
        }
    }
    /// <summary>
    /// 客户端发送数据到服务器
    /// </summary>
    /// <param name="netId">行为</param>
    /// <param name="data">具体做什么变化</param>
    public void SendMessager(int netId,string data)
    {
        //将id转换成Byte
        //BitConverter.GetBytes 可以将基本数据类型转换成字节数组
        byte[] idByte = BitConverter.GetBytes(netId);

        //将变化数据转成Byte
        //Encoding.UTF8.GetBytes 用于将字符串编码为字节数组
        byte[] dByte = Encoding.UTF8.GetBytes(data);

        //将上面需要发送给服务器的数据Byte，读进客户端处理网络数据的Byte数组
        //src：源数组
        //srcOffset：源数组中的起始位置（以字节为单位）
        //dst：目标数组
        //dstOffset：目标数组中的起始位置（以字节为单位）
        //count：要复制的字节数
        Buffer.BlockCopy(dataByte, 0, idByte, 0, idByte.Length);
        Buffer.BlockCopy(dataByte, idByte.Length, dByte, 0, dByte.Length);

        st.BeginSend(dataByte,0,dataByte.Length,SocketFlags.None,SendHandle,null);
    }
    /// <summary>
    /// 客户端发送数据到服务器
    /// </summary>
    /// <param name="netId"></param>
    /// <param name="data"></param>
    public void SendMessager(int netId, byte[] data)
    {
        //将id转换成Byte
        byte[] idByte = BitConverter.GetBytes(netId);

        //将上面需要发送给服务器的数据Byte，读进客户端处理网络数据的Byte数组
        Buffer.BlockCopy(dataByte, 0, idByte, 0, idByte.Length);
        Buffer.BlockCopy(dataByte, idByte.Length, data, 0, data.Length);

        st.BeginSend(dataByte, 0, dataByte.Length, SocketFlags.None, SendHandle, null);
    }
    private void SendHandle(IAsyncResult ar)
    {
        
    }
}
