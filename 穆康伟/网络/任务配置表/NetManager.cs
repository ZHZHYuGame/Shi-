using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;
using UnityEngine;

public class NetManager : Singleton<NetManager>
{
    Socket socket;

    byte[] dataByte=new byte[1024];
    private void Start()
    {
        socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        socket.BeginConnect("127.0.0.1", 1668, GameConnectNetHandle, null);
    }

    private void GameConnectNetHandle(IAsyncResult ar)
    {
        try
        {
                socket.EndConnect(ar);
            socket.BeginReceive(dataByte,0,dataByte.Length,SocketFlags.None,ReceiveHandle,null);
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
    void SendMessage(int netID,string data)
    {
        byte[] idByte = BitConverter.GetBytes(netID);
        byte[] dByte = Encoding.UTF8.GetBytes(data);
        Buffer.BlockCopy(dataByte, 0, idByte, 0, idByte.Length);
        Buffer.BlockCopy(dataByte, idByte.Length, dByte, 0, dByte.Length);
        socket.BeginSend(dataByte, 0, dataByte.Length, SocketFlags.None, SenHandle, null);
    }

    private void SenHandle(IAsyncResult ar)
    {
        throw new NotImplementedException();
    }
}
