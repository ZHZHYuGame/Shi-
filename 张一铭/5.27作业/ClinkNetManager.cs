using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using UnityEngine;


/// <summary>
/// �ͻ���Socket���ӷ�����
/// 1.���Ӵ���
/// 2.�������ݴ���
/// 3.�������ݴ���
/// </summary>

public class NetManager : Singleton<NetManager>
{

    // ����һ��Socket�����������������������
    Socket socket;

    /// <summary>
    /// �ͻ��˴����������ݵ�Byte����
    /// </summary>

    // ����һ���ֽ����飬���ڴ洢���յ�������
    byte[] data = new byte[1024];
    // ����һ�����У����ڴ洢���յ�����Ϣ������
    Queue<byte[]> queue = new Queue<byte[]>();

    // ��ʼ���ͻ��˵ķ���
    public void Init()
    {
        // ����һ���µ�Socket����ʹ��IPv4��ַ�塢��ʽ�����TCPЭ��
        socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        // ��ʼ�첽���ӵ�ָ���ķ�����IP��ַ�Ͷ˿ڣ�Cooect����Ϊ�ص�����
        socket.BeginConnect("10.161.13.123", 6666, Cooect, null);
    }

    // �첽���ӵ��������Ļص�����
    private void Cooect(IAsyncResult ar)
    {
        try
        {
            // �����첽���Ӳ���
            socket.EndConnect(ar);
            // ��Unity�Ŀ���̨������ӳɹ�����Ϣ
            Debug.Log("���ӳɹ�");
            // �ͻ��˽��ܷ��������ݼ���
            // ��ʼ�첽���շ��������͵����ݣ�Receive����Ϊ�ص�����
            socket.BeginReceive(data, 0, data.Length, SocketFlags.None, Receive, null);
        }
        catch (Exception ex)
        {
            // �����쳣
            // �����쳣����Unity�Ŀ���̨���������Ϣ
            Debug.LogError(ex);
        }
    }

    /// <summary>
    /// �ͻ��˽��ܷ��������ݼ������������ݴ���(�̴߳���)
    /// </summary>
    /// <param name="ar"></param>

    // �첽���շ��������ݵĻص�����
    private void Receive(IAsyncResult ar)
    {
        try
        {
            // �����첽���ղ�������ȡ���յ������ݳ���
            int len = socket.EndReceive(ar);
            if (len > 0)
            {
                // ����һ���µ��ֽ����飬���ڴ洢���յ�������
                byte[] rdata = new byte[len];
                // �����յ������ݸ��Ƶ��µ��ֽ�������
                Buffer.BlockCopy(data, 0, rdata, 0, len);
                // ѭ��������յ������ݣ�ֱ�����ݳ���С�ڵ���4
                while (rdata.Length > 4)
                {
                    // �����ݵ�ǰ4���ֽ��л�ȡ��Ϣ��ĳ���
                    int bodylen = BitConverter.ToInt32(rdata, 0);
                    // ����һ���µ��ֽ����飬���ڴ洢��Ϣ������
                    byte[] bodydata = new byte[bodylen];
                    // ����Ϣ�����ݸ��Ƶ��µ��ֽ�������
                    Buffer.BlockCopy(rdata, 4, bodydata, 0, bodylen);
                    // ����Ϣ��������ӵ�������
                    queue.Enqueue(bodydata);

                    // ����ʣ�����ݵĳ���
                    int sylen = rdata.Length - 4 - bodylen;
                    // ����һ���µ��ֽ����飬���ڴ洢ʣ������
                    byte[] sydata = new byte[sylen];
                    // ��ʣ�����ݸ��Ƶ��µ��ֽ�������
                    Buffer.BlockCopy(rdata, 4 + bodylen, sydata, 0, sydata.Length);
                    // ����rdata���飬��������ʣ������
                    rdata = sydata;
                }
            }
            // �����첽���շ��������͵�����
            socket.BeginReceive(data, 0, data.Length, SocketFlags.None, Receive, null);
        }
        catch (Exception ex)
        {
            // �����쳣����Unity�Ŀ���̨���������Ϣ
            Debug.LogError(ex);
        }
    }

    // ��������������ݵķ���
    public void OnSendCli(int id, byte[] data)
    {
        // ������Ϣ����ܳ��ȣ�������ϢID����Ϣ���ݵĳ���
        int bodylen = 4 + data.Length;
        // ����һ���յ��ֽ�����
        byte[] enddata = new byte[0];
        // ����Ϣ�峤�ȡ���ϢID����Ϣ��������ƴ�ӳ�һ���µ��ֽ�����
        enddata = enddata.Concat(BitConverter.GetBytes(bodylen)).Concat(BitConverter.GetBytes(id)).Concat(data).ToArray();
        // ��ʼ�첽�������ݣ�Send����Ϊ�ص�����
        socket.BeginSend(enddata, 0, enddata.Length, SocketFlags.None, Send, null);
    }

    // �첽�������ݵĻص�����
    private void Send(IAsyncResult ar)
    {
        // �����첽���Ͳ�������ȡ���͵����ݳ���
        int len = socket.EndSend(ar);
    }

    // ������յ�����Ϣ�ķ�����ͨ����Update�����е���
    public void NetUpdata()
    {
        // ѭ����������е���Ϣ��ֱ������Ϊ��
        while (queue.Count > 0)
        {
            // �Ӷ�����ȡ��һ����Ϣ������
            byte[] data = queue.Dequeue();
            // ����Ϣ�����ݵ�ǰ4���ֽ��л�ȡ��ϢID
            int msgid = BitConverter.ToInt32(data, 0);
            // ����һ���µ��ֽ����飬���ڴ洢��Ϣ��������
            byte[] infodata = new byte[data.Length - 4];
            // ����Ϣ�������ݸ��Ƶ��µ��ֽ�������
            Buffer.BlockCopy(data, 4, infodata, 0, infodata.Length);
            // ����MsgManager��OnBroadcast�������㲥��Ϣ
            MsgManager<byte[]>.Ins.OnBroadcast(msgid, infodata);
        }
    }
}