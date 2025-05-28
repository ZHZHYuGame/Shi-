using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using UnityEngine;


/// <summary>
/// 客户端Socket连接服务器
/// 1.连接处理
/// 2.接受数据处理
/// 3.发送数据处理
/// </summary>

public class NetManager : Singleton<NetManager>
{

    // 定义一个Socket对象，用于与服务器建立连接
    Socket socket;

    /// <summary>
    /// 客户端处理网络数据的Byte数组
    /// </summary>

    // 定义一个字节数组，用于存储接收到的数据
    byte[] data = new byte[1024];
    // 定义一个队列，用于存储接收到的消息体数据
    Queue<byte[]> queue = new Queue<byte[]>();

    // 初始化客户端的方法
    public void Init()
    {
        // 创建一个新的Socket对象，使用IPv4地址族、流式传输和TCP协议
        socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        // 开始异步连接到指定的服务器IP地址和端口，Cooect方法为回调函数
        socket.BeginConnect("10.161.13.123", 6666, Cooect, null);
    }

    // 异步连接到服务器的回调函数
    private void Cooect(IAsyncResult ar)
    {
        try
        {
            // 结束异步连接操作
            socket.EndConnect(ar);
            // 在Unity的控制台输出连接成功的信息
            Debug.Log("连接成功");
            // 客户端接受服务器数据监听
            // 开始异步接收服务器发送的数据，Receive方法为回调函数
            socket.BeginReceive(data, 0, data.Length, SocketFlags.None, Receive, null);
        }
        catch (Exception ex)
        {
            // 监听异常
            // 捕获异常并在Unity的控制台输出错误信息
            Debug.LogError(ex);
        }
    }

    /// <summary>
    /// 客户端接受服务器数据监听，网络数据处理(线程处理)
    /// </summary>
    /// <param name="ar"></param>

    // 异步接收服务器数据的回调函数
    private void Receive(IAsyncResult ar)
    {
        try
        {
            // 结束异步接收操作，获取接收到的数据长度
            int len = socket.EndReceive(ar);
            if (len > 0)
            {
                // 创建一个新的字节数组，用于存储接收到的数据
                byte[] rdata = new byte[len];
                // 将接收到的数据复制到新的字节数组中
                Buffer.BlockCopy(data, 0, rdata, 0, len);
                // 循环处理接收到的数据，直到数据长度小于等于4
                while (rdata.Length > 4)
                {
                    // 从数据的前4个字节中获取消息体的长度
                    int bodylen = BitConverter.ToInt32(rdata, 0);
                    // 创建一个新的字节数组，用于存储消息体数据
                    byte[] bodydata = new byte[bodylen];
                    // 将消息体数据复制到新的字节数组中
                    Buffer.BlockCopy(rdata, 4, bodydata, 0, bodylen);
                    // 将消息体数据添加到队列中
                    queue.Enqueue(bodydata);

                    // 计算剩余数据的长度
                    int sylen = rdata.Length - 4 - bodylen;
                    // 创建一个新的字节数组，用于存储剩余数据
                    byte[] sydata = new byte[sylen];
                    // 将剩余数据复制到新的字节数组中
                    Buffer.BlockCopy(rdata, 4 + bodylen, sydata, 0, sydata.Length);
                    // 更新rdata数组，继续处理剩余数据
                    rdata = sydata;
                }
            }
            // 继续异步接收服务器发送的数据
            socket.BeginReceive(data, 0, data.Length, SocketFlags.None, Receive, null);
        }
        catch (Exception ex)
        {
            // 捕获异常并在Unity的控制台输出错误信息
            Debug.LogError(ex);
        }
    }

    // 向服务器发送数据的方法
    public void OnSendCli(int id, byte[] data)
    {
        // 计算消息体的总长度，包括消息ID和消息内容的长度
        int bodylen = 4 + data.Length;
        // 创建一个空的字节数组
        byte[] enddata = new byte[0];
        // 将消息体长度、消息ID和消息内容数据拼接成一个新的字节数组
        enddata = enddata.Concat(BitConverter.GetBytes(bodylen)).Concat(BitConverter.GetBytes(id)).Concat(data).ToArray();
        // 开始异步发送数据，Send方法为回调函数
        socket.BeginSend(enddata, 0, enddata.Length, SocketFlags.None, Send, null);
    }

    // 异步发送数据的回调函数
    private void Send(IAsyncResult ar)
    {
        // 结束异步发送操作，获取发送的数据长度
        int len = socket.EndSend(ar);
    }

    // 处理接收到的消息的方法，通常在Update方法中调用
    public void NetUpdata()
    {
        // 循环处理队列中的消息，直到队列为空
        while (queue.Count > 0)
        {
            // 从队列中取出一个消息体数据
            byte[] data = queue.Dequeue();
            // 从消息体数据的前4个字节中获取消息ID
            int msgid = BitConverter.ToInt32(data, 0);
            // 创建一个新的字节数组，用于存储消息内容数据
            byte[] infodata = new byte[data.Length - 4];
            // 将消息内容数据复制到新的字节数组中
            Buffer.BlockCopy(data, 4, infodata, 0, infodata.Length);
            // 调用MsgManager的OnBroadcast方法，广播消息
            MsgManager<byte[]>.Ins.OnBroadcast(msgid, infodata);
        }
    }
}