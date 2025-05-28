using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using Unity.VisualScripting;
using UnityEngine;

public class NetManager : Singleton<NetManager>
{
    /// <summary>
    /// 通常叫"套接字"，描述IP地址和端口
    /// 网络套接字
    /// </summary>
    Socket st;
    //Socket非常类似于电话插座。以一个电话网为例:电话的通话双方相当于相互通信的2个程序，
    //电话号码就是ip地址。任何用户在通话之前，首先要占有一部电话机，
    //相当于申请一个socket;同时要知道对方的号码，相当于对方有一个固定的socket。
    //然后向对方拨号呼叫，相当于发出连接请求。
    //对方假如在场并空闲，拿起电话话筒，双方就可以正式通话，相当于连接成功。
    //双方通话的过程，是一方向电话机发出信号和对方从电话机接收信号的过程，
    //相当于向socket发送数据和从socket接收数据。通话结束后，一方挂起电话机相当于关闭socket，撤销连接。

    /// <summary>
    /// 客户端处理网络数据的byte数组
    /// </summary>
    byte[] dataByte=new byte[1024];
    public void Start()
    {
        //固定的写法
        st = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        //客户端连接服务器请求
        //服务器Id地址 端口号 方法 null
        st.BeginConnect("127.0.0.1", 1668, GameConectNetHangle, null);
    }
    /// <summary>
    /// 连接服务器请求结果
    /// </summary>
    /// <param name="ar"></param>
    private void GameConectNetHangle(IAsyncResult ar)
    {
        try
        {
            //连接成功
            st.EndConnect(ar);
            //客户端接收服务器
            st.BeginReceive(dataByte, 0, dataByte.Length, SocketFlags.None, ReceivHangdle, null);
        }
        catch(Exception)
        {
            //连接异常
            throw;
        }
    }
    /// <summary>
    /// 客户端接受服务器数据监听，网络数据处理（线成处理）
    /// </summary>
    /// <param name="ar"></param>
    /// <exception cref="NotImplementedException"></exception>
    private void ReceivHangdle(IAsyncResult ar)
    {
        throw new NotImplementedException();
    }
}
