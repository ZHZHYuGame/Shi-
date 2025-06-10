using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using Unity.VisualScripting;
using UnityEngine;

public class NetWorkSocket : MonoBehaviour
{
    // Start is called before the first frame update
    #region 单例
    private static NetWorkSocket instance;
    public static NetWorkSocket Instance
    {
        get
        {
            if (instance == null)
            {
                GameObject obj = new GameObject("NetWorkSocket");
                DontDestroyOnLoad(obj);
                instance = obj.GetOrAddComponent<NetWorkSocket>();
            }
            return instance;
        }
    }
    #endregion

    private byte[] buffer = new byte[1024];

    #region 发送消息所需变量
    //发送消息队列
    private Queue<byte[]> m_SendQueue = new Queue<byte[]>();

    //检查队列的委托
    private Action m_CheckSendQueue;
    #endregion
    #region 接收消息所需变量
    //接受数据包的字节数组缓冲区
    private byte[] m_ReceiveBuffer = new byte[10240];

    //接受数据包的缓冲数据流
    private MyMemoryStream m_ReceiveMS = new MyMemoryStream();

    //接收消息的队列 
    private Queue<byte[]> m_ReceiveQueue = new Queue<byte[]>();

    private int m_ReceiceCount = 0;
    #endregion



    //客户端Socket
    private Socket m_Client;

    void Update()
    {
        #region 从队列中获取数据
        while (true)
        {
            if (m_ReceiceCount <= 5)
            {
                m_ReceiceCount++;
                lock (m_ReceiveQueue)
                {
                    if (m_ReceiveQueue.Count > 0)
                    {
                        byte[] buffer = m_ReceiveQueue.Dequeue();
                        ushort protoCode = 0;
                        byte[] protoContent = new byte[buffer.Length - 2];
                        using (MyMemoryStream ms = new MyMemoryStream(buffer))
                        {
                            //协议编号
                            protoCode = ms.ReadUshort();
                            ms.Read(protoContent, 0, protoContent.Length);
                            //临时
                            EventDispatcher.GetInstance().Dispath(protoCode, protoContent);
                            //GlobalInit.instance.onReceiveProto(protoCode, protoContent);
                        }
                    }
                    else
                    {
                        break;
                    }
                }
            }
            else
            {
                m_ReceiceCount = 0;
                break;
            }
        }
        #endregion
    }
    void OnDestroy()
    {
        if (m_Client != null && m_Client.Connected)
        {
            m_Client.Shutdown(SocketShutdown.Both);
            m_Client.Close();
        }
    }
    //==============================发送给服务端的代码=================================
    #region 链接到Socket服务器
    /// <summary>
    /// 链接Socket服务器
    /// </summary>
    /// <param name="ip">ip</param>
    /// <param name="port">端口号</param> <summary>
    public void Connect(string ip, int port)
    {
        //如果socket 已经存在 并且处于连接中状态 则直接返回
        if (m_Client != null && m_Client.Connected) return;
        m_Client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        try
        {
            m_Client.Connect(new IPEndPoint(IPAddress.Parse(ip), port));
            m_CheckSendQueue = OnCheckSendQueueCallBack;

            ReceiveMsg();
            Debug.Log("连接成功");
        }
        catch (Exception ex)
        {


            Debug.Log("连接失败" + ex.Message);
        }
    }
    #endregion

    #region 检查队列的委托回调
    /// <summary>
    /// 检查队列的委托回调
    /// </summary> <summary>
    private void OnCheckSendQueueCallBack()
    {
        lock (m_SendQueue)
        {
            //如果队列中有数据包 则发送数据包
            if (m_SendQueue.Count > 0)
            {
                //发送数据包
                Send(m_SendQueue.Dequeue());
            }
        }
    }

    #endregion

    #region 封装数据包
    /// <summary>
    /// 封装数据包
    /// </summary>
    /// <param name="data"></param>
    private byte[] MakeData(byte[] data)
    {
        byte[] retBuffer = null;
        using (MyMemoryStream ms = new MyMemoryStream())
        {
            ms.WriteUShort((ushort)(data.Length));
            ms.Write(data, 0, data.Length);
            retBuffer = ms.ToArray();
        }
        return retBuffer;
    }
    #endregion

    #region 发送消息 把消息加入队列
    /// <summary>
    /// 
    /// </summary>
    /// <param name="buffer"></param> <summary>
    public void SendMsg(byte[] buffer)
    {
        //得到封装后的数据包
        byte[] sendBuffer = MakeData(buffer);

        lock (m_SendQueue)
        {
            //把数据包加入队列
            m_SendQueue.Enqueue(sendBuffer);

            //启动委托(执行委托)
            m_CheckSendQueue.BeginInvoke(null, null);
        }

    }
    #endregion

    #region 真正发送到服务器的数据包
    /// <summary>
    /// 真正发送到服务器的数据包
    /// </summary>
    /// <param name="buffer"></param> <summary>
    private void Send(byte[] buffer)
    {
        m_Client.BeginSend(buffer, 0, buffer.Length, SocketFlags.None, SendCallBack, m_Client);
    }
    #endregion

    #region 发送数据包的回调
    /// <summary>
    /// 发送数据包的回调
    /// </summary>
    /// <param name="ar"></param> <summary>
    private void SendCallBack(IAsyncResult ar)
    {
        m_Client.EndSend(ar);

        //继续检查队列
        OnCheckSendQueueCallBack();
    }
    #endregion
    //==================================接受服务端消息的代码===============================

    #region ReceiveMsg 接收数据
    /// <summary>
    /// 接收数据
    /// </summary>
    private void ReceiveMsg()
    {
        //异步加载
        m_Client.BeginReceive(m_ReceiveBuffer, 0, m_ReceiveBuffer.Length, SocketFlags.None, ReceiveCallBack, m_Client);
    }
    #endregion

    #region ReceiveCallBack 接收数据的回调
    private void ReceiveCallBack(IAsyncResult ar)
    {
        try
        {
            int len = m_Client.EndReceive(ar);
            if (len > 0)
            {
                //已经接收到数据
                m_ReceiveMS.Position = m_ReceiveMS.Length;

                //把指定长度的字节 写入数据流
                m_ReceiveMS.Write(m_ReceiveBuffer, 0, len);

                //如果缓存的数据流的长度>2 说明至少有个不完整的包过来了
                //为什么这里是2 因为我们客服端封包的时候 用的ushort的长度就是2

                if (m_ReceiveMS.Length > 2)
                {
                    //惊醒循环拆包
                    while (true)
                    {
                        //把数据流的指针位置放在0处
                        m_ReceiveMS.Position = 0;

                        //currMsgLenth=包体的长度
                        int currMsgLenth = m_ReceiveMS.ReadUshort();

                        // currFullMsgLenth=总包的长度=包头长度+包体长度
                        int currFullMsgLenth = 2 + currMsgLenth;

                        //如果数据流的长度>=整包的长度 说明至少收到了一个完整包
                        if (m_ReceiveMS.Length >= currFullMsgLenth)
                        {
                            //定义包体的byte[]数组
                            byte[] buffer = new byte[currMsgLenth];

                            //把数据流指针放到2的位置 也就是包体的位置
                            m_ReceiveMS.Position = 2;

                            //把包体读到byte[]数组
                            m_ReceiveMS.Read(buffer, 0, currMsgLenth);

                            lock (m_ReceiveQueue)
                            {
                                m_ReceiveQueue.Enqueue(buffer);
                            }

                            //=============处理剩余字节数组==============

                            //剩余字节长度
                            int remainLen = (int)m_ReceiveMS.Length - currFullMsgLenth;
                            if (remainLen > 0)
                            {
                                //把指针放在第一个包的尾部
                                m_ReceiveMS.Position = currFullMsgLenth;

                                //定于剩余字节数组
                                byte[] remainBuffer = new byte[remainLen];

                                //把数据流读到剩余字节的数组
                                m_ReceiveMS.Read(remainBuffer, 0, remainLen);

                                //清空数组
                                m_ReceiveMS.Position = 0;
                                m_ReceiveMS.SetLength(0);

                                //把剩余字节数组重新写进数据流
                                m_ReceiveMS.Write(remainBuffer, 0, remainBuffer.Length);

                                remainBuffer = null;
                            }
                            else
                            {
                                m_ReceiveMS.Position = 0;
                                m_ReceiveMS.SetLength(0);

                                break;
                            }
                        }
                        else
                        {
                            //还没有收到完整包
                            break;
                        }

                    }
                }
                //进行下一次收到数据包
                ReceiveMsg();
            }
            else
            {
                Debug.Log($"服务器{m_Client.RemoteEndPoint}断开连接");
            }
        }
        catch (Exception)
        {

            Debug.Log($"服务器{m_Client.RemoteEndPoint}断开连接");
        }

    }
    #endregion
}

