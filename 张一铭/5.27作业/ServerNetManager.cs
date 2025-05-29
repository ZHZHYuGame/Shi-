using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ServerWeek
{
    internal class NetManager : Singleton<NetManager>
    {
        // 定义一个Socket对象，用于监听客户端的连接请求
        Socket socket;
        // 定义一个列表，用于存储所有已连接的客户端对象
        public List<Client> allcli = new List<Client>();

        // 初始化服务器的方法
        public void Init()
        {
            // 创建一个新的Socket对象，使用IPv4地址族、流式传输和TCP协议
            socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            // 将Socket绑定到本地的任意IP地址和端口6666
            socket.Bind(new IPEndPoint(IPAddress.Any, 6666));
            // 开始监听客户端的连接请求，最大允许1000个挂起的连接
            socket.Listen(1000);

            // 在控制台输出服务器已开启的信息
            Console.WriteLine("服务器开启");
            // 开始异步接受客户端的连接请求，Accept方法为回调函数
            socket.BeginAccept(Accept, null);
        }

        // 异步接受客户端连接的回调函数
        private void Accept(IAsyncResult ar)
        {
            try
            {
                // 结束异步接受操作，获取新连接的客户端Socket对象
                Socket socket_cli = socket.EndAccept(ar);
                // 获取客户端的IP地址和端口信息
                IPEndPoint ipend = socket_cli.RemoteEndPoint as IPEndPoint;
                // 创建一个新的Client对象
                Client client = new Client();
                // 将客户端的Socket对象赋值给Client对象
                client.socket = socket_cli;
                // 在控制台输出客户端已连接的信息
                Console.WriteLine("用户{0}已连接", ipend.Port);
                // 继续异步接受其他客户端的连接请求
                socket.BeginAccept(Accept, null);
                // 开始异步接收客户端发送的数据，Receive方法为回调函数
                client.socket.BeginReceive(client.data, 0, client.data.Length, SocketFlags.None, Receive, client);
            }
            catch (Exception ex)
            {
                // 捕获异常并在控制台输出异常信息
                Console.WriteLine(ex);
            }
        }

        // 异步接收客户端数据的回调函数
        private void Receive(IAsyncResult ar)
        {
            try
            {
                // 获取传递给回调函数的Client对象
                Client client = ar.AsyncState as Client;
                // 结束异步接收操作，获取接收到的数据长度
                int len = client.socket.EndReceive(ar);
                if (len > 0)
                {
                    // 创建一个新的字节数组，用于存储接收到的数据
                    byte[] rdata = new byte[len];
                    // 将接收到的数据复制到新的字节数组中
                    Buffer.BlockCopy(client.data, 0, rdata, 0, len);
                    // 循环处理接收到的数据，直到数据长度小于等于4
                    while (rdata.Length > 4)
                    {
                        // 从数据的前4个字节中获取消息体的长度
                        int bodylen = BitConverter.ToInt32(rdata, 0);
                        // 创建一个新的字节数组，用于存储消息体数据
                        byte[] bodydata = new byte[bodylen];
                        // 将消息体数据复制到新的字节数组中
                        Buffer.BlockCopy(rdata, 4, bodydata, 0, bodylen);
                        // 从消息体数据的前4个字节中获取消息ID
                        int msgid = BitConverter.ToInt32(bodydata, 0);
                        // 创建一个新的字节数组，用于存储消息内容数据
                        byte[] infodata = new byte[bodydata.Length - 4];
                        // 将消息内容数据复制到新的字节数组中
                        Buffer.BlockCopy(bodydata, 4, infodata, 0, infodata.Length);

                        // 创建一个新的MsgData对象
                        MsgData msgData = new MsgData();
                        // 将客户端对象赋值给MsgData对象
                        msgData.client = client;
                        // 将消息内容数据赋值给MsgData对象
                        msgData.data = infodata;
                        // 调用MsgManager的OnBroadcast方法，广播消息
                        MsgManager<MsgData>.Ins.OnBroadcast(msgid, msgData);

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
                // 继续异步接收客户端发送的数据
                client.socket.BeginReceive(client.data, 0, client.data.Length, SocketFlags.None, Receive, client);
            }
            catch (Exception ex)
            {
                // 捕获异常并在控制台输出异常信息
                Console.WriteLine(ex);
            }
        }

        // 向指定客户端发送数据的方法
        public void OnSendCli(int id, byte[] data, Client client)
        {
            // 计算消息体的总长度，包括消息ID和消息内容的长度
            int bodylen = 4 + data.Length;
            // 创建一个空的字节数组
            byte[] enddata = new byte[0];
            // 将消息体长度、消息ID和消息内容数据拼接成一个新的字节数组
            enddata = enddata.Concat(BitConverter.GetBytes(bodylen)).Concat(BitConverter.GetBytes(id)).Concat(data).ToArray();
            // 开始异步发送数据，Send方法为回调函数
            client.socket.BeginSend(enddata, 0, enddata.Length, SocketFlags.None, Send, client);
        }

        // 异步发送数据的回调函数
        private void Send(IAsyncResult ar)
        {
            // 获取传递给回调函数的Client对象
            Client client = ar.AsyncState as Client;
            // 结束异步发送操作，获取发送的数据长度
            int len = client.socket.EndSend(ar);
        }
    }
}