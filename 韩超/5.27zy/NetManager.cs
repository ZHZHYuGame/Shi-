using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;
using UnityEngine;
/// <summary>
/// �ͻ���Socket���ӷ�����
/// 1.���Ӵ���
/// 2.�������ݴ���
/// 3.�������ݴ���
/// </summary>
public class NetManager : SingLeton<NetManager>
{
    /// <summary>
    /// �����׽���
    /// </summary>
    Socket st;
    /// <summary>
    /// �ͻ��˴����������ݵ�Byte����
    /// </summary>
    byte[] dataByte = new byte[1024];
    public void Start()
    {
        st = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        //�ͻ������ӷ�������
        st.BeginConnect("127.0.0.1", 1668, GameConnectHandle, null);
    }
    /// <summary>
    /// ���ӷ�����������
    /// </summary>
    /// <param name="ar"></param>
    private void GameConnectHandle(IAsyncResult ar)
    {
        try
        {
            //���ӳɹ�
            st.EndConnect(ar);
            //�ͻ��˽��շ��������ݼ���
            st.BeginReceive(dataByte, 0, dataByte.Length, SocketFlags.None, ReceiveHandle, null);
        }
        catch (Exception)
        {
            //�����쳣
        }
    }
    /// <summary>
    /// �ͻ��˽��շ��������ݼ���,�������ݴ���(�̴߳���)
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
    /// �ͻ��˷������ݵ�������
    /// </summary>
    /// <param name="netid">��Ϊ</param>
    /// <param name="data">������ʲô�仯</param>
    public void SendMessage(int netid,string data)
    {
        //��IDת����Byte
        byte[] idByte = BitConverter.GetBytes(netid);
        //���仯����ת����Byte
        byte[] dByte = Encoding.UTF8.GetBytes(data);
        //��������Ҫ���͸�������������Byte,�����ͻ��˴����������ݵ�Byte����
        Buffer.BlockCopy(dataByte,0,idByte,0,idByte.Length);
        Buffer.BlockCopy(dataByte,idByte.Length, dByte,0,dByte.Length);

        st.BeginSend(dataByte,0,dataByte.Length,SocketFlags.None,SenHandle,null);
    }
    /// <summary>
    /// �ͻ��˷������ݵ�������
    /// </summary>
    /// <param name="netid">��Ϊ</param>
    /// <param name="data">������ʲô�仯</param>
    public void SendMessage(int netid, byte[] data)
    {
        //��IDת����Byte
        byte[] idByte = BitConverter.GetBytes(netid);
        //��������Ҫ���͸�������������Byte,�����ͻ��˴����������ݵ�Byte����
        Buffer.BlockCopy(dataByte, 0, idByte, 0, idByte.Length);
        Buffer.BlockCopy(dataByte, idByte.Length, data, 0, data.Length);

        st.BeginSend(dataByte, 0, dataByte.Length, SocketFlags.None, SenHandle, null);
    }
    private void SenHandle(IAsyncResult ar)
    {

    }
}
