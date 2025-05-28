using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Text;
using UnityEngine;

/// <summary>
/// 客户端Socket连接服务器
/// 1.连接处理
/// 2.接受数据处理
/// 3.发送数据处理
/// </summary>
public class NetManger : SingLeton<NetManger>
{ 
    /// <summary>
   /// 网络套字节
   /// </summary>
    Socket st;
    /// <summary>
    /// 客户端处理网络数据的Byte数组
    /// </summary>
    byte[] dataByte = new byte[1024];
    /// <summary>
    /// 网络套字节
    /// </summary>
    // Start is called before the first frame update
    void Start()
    {
        //IPv4 v6, 套字节类型，流式套字节，协议
        st=new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        //客户端连接服务器请求
        st.BeginConnect("127.0.0.1", 1668, GameConnectNetHandle, null);
    }
    /// <summary>
    /// 连接服务器请求结束
    /// </summary>
    /// <param name="ar"></param>
    private void GameConnectNetHandle(IAsyncResult ar)
    {
        try
        {
            st.EndConnect(ar);
            st.BeginReceive(dataByte, 0, dataByte.Length, SocketFlags.None, ReceiveHandle, null);
        }
        catch (Exception)
        {
            //连接异常
        }
    }
    /// <summary>
    /// 客户端接收服务器数据监听，网络数据处理（线程处理）
    /// </summary>
    /// <param name="ar"></param>
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
    /// <param name="data">具体做什么变化</param>
    public void SendMessage(int netId,string data)
    {
        //将ID转换成Byte
        byte[]idByte=BitConverter.GetBytes(netId);
        //将变化数据转换成Byte
        byte[]dByte=Encoding.UTF8.GetBytes(data);
        //将上面需要发送给服务器的数据Byte，读进客户端处理网络数据的Byte数组
        Buffer.BlockCopy(dataByte,0,idByte,0,dByte.Length);
        Buffer.BlockCopy(dataByte,idByte.Length,dByte,0,dataByte.Length);

        st.BeginSend(dataByte, 0, dataByte.Length, SocketFlags.None,SendHandle,null);

    }
    public void SendMessage(int netId, byte[] data)
    {
        //将ID转换成Byte
        byte[] idByte = BitConverter.GetBytes(netId);
        //将上面需要发送给服务器的数据Byte，读进客户端处理网络数据的Byte数组
        Buffer.BlockCopy(dataByte, 0, idByte, 0, idByte.Length);
        Buffer.BlockCopy(dataByte, idByte.Length, data, 0, idByte.Length);

        st.BeginSend(dataByte, 0, dataByte.Length, SocketFlags.None, SendHandle, null);
    }
    private void SendHandle(IAsyncResult ar)
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}