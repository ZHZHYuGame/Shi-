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
public class NetManager : Singleton<NetManager>
{
    /// <summary>
    /// ���� �׽���
    /// </summary>
    Socket st;
    /// <summary>
    /// �ͻ��˴����������ݵ�Byte����
    /// </summary>
    Byte[] dataByte=new byte[1024];
    public void Strat()
    {
        //AddressFamily.InterNetwork����ʾʹ�� IPv4 ��ַ��
        //SocketType.Stream����ʾ�������ӵĿɿ��ֽ����׽��֣�ͨ������ TCP Э��
        //ProtocolType.Tcp����ʾʹ�� TCP Э��
        st = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

        //�ͻ������ӷ�������
        //BeginConnect(�����첽����һ�����ӵ�Զ������������)
        //"127.0.0.1"����ʾ���ӵ�����������IP��ַ����������
        //1668����ʾҪ���ӵķ������˿ں�
        //GameConnectNetHandle���������ʱ���õĻص�����
        //null��û�д��ݶ����״̬����
        st.BeginConnect("127.0.0.1",1668,GameConnectNetHandle,null);
    }
    /// <summary>
    /// ���ӷ���������
    /// </summary>
    /// <param name="ar"></param>
    private void GameConnectNetHandle(IAsyncResult ar)
    {
        try
        {
            //���ӳɹ�
            st.EndConnect(ar);
            //�ͻ��˽��ܷ��������ݼ���
            //dataByte �ǽ������ݵĻ�����
            //0 �ǻ���������ʼƫ����
            //dataByte.Length �ǽ��յ�����ֽ���
            //SocketFlags.None ��ʾ��ʹ������ѡ��
            st.BeginReceive(dataByte,0,dataByte.Length,SocketFlags.None,ReceiveHandle,null);
        }
        catch(Exception)  
        {
            //�����쳣
        }
    }
    /// <summary>
    /// �ͻ������ӷ��������ݼ������������ݴ����̴߳���
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
    /// �ͻ��˷������ݵ�������
    /// </summary>
    /// <param name="netId">��Ϊ</param>
    /// <param name="data">������ʲô�仯</param>
    public void SendMessager(int netId,string data)
    {
        //��idת����Byte
        //BitConverter.GetBytes ���Խ�������������ת�����ֽ�����
        byte[] idByte = BitConverter.GetBytes(netId);

        //���仯����ת��Byte
        //Encoding.UTF8.GetBytes ���ڽ��ַ�������Ϊ�ֽ�����
        byte[] dByte = Encoding.UTF8.GetBytes(data);

        //��������Ҫ���͸�������������Byte�������ͻ��˴����������ݵ�Byte����
        //src��Դ����
        //srcOffset��Դ�����е���ʼλ�ã����ֽ�Ϊ��λ��
        //dst��Ŀ������
        //dstOffset��Ŀ�������е���ʼλ�ã����ֽ�Ϊ��λ��
        //count��Ҫ���Ƶ��ֽ���
        Buffer.BlockCopy(dataByte, 0, idByte, 0, idByte.Length);
        Buffer.BlockCopy(dataByte, idByte.Length, dByte, 0, dByte.Length);

        st.BeginSend(dataByte,0,dataByte.Length,SocketFlags.None,SendHandle,null);
    }
    /// <summary>
    /// �ͻ��˷������ݵ�������
    /// </summary>
    /// <param name="netId"></param>
    /// <param name="data"></param>
    public void SendMessager(int netId, byte[] data)
    {
        //��idת����Byte
        byte[] idByte = BitConverter.GetBytes(netId);

        //��������Ҫ���͸�������������Byte�������ͻ��˴����������ݵ�Byte����
        Buffer.BlockCopy(dataByte, 0, idByte, 0, idByte.Length);
        Buffer.BlockCopy(dataByte, idByte.Length, data, 0, data.Length);

        st.BeginSend(dataByte, 0, dataByte.Length, SocketFlags.None, SendHandle, null);
    }
    private void SendHandle(IAsyncResult ar)
    {
        
    }
}
