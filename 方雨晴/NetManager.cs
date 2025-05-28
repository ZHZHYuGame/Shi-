using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using Unity.VisualScripting;
using UnityEngine;

public class NetManager : Singleton<NetManager>
{
    /// <summary>
    /// ͨ����"�׽���"������IP��ַ�Ͷ˿�
    /// �����׽���
    /// </summary>
    Socket st;
    //Socket�ǳ������ڵ绰��������һ���绰��Ϊ��:�绰��ͨ��˫���൱���໥ͨ�ŵ�2������
    //�绰�������ip��ַ���κ��û���ͨ��֮ǰ������Ҫռ��һ���绰����
    //�൱������һ��socket;ͬʱҪ֪���Է��ĺ��룬�൱�ڶԷ���һ���̶���socket��
    //Ȼ����Է����ź��У��൱�ڷ�����������
    //�Է������ڳ������У�����绰��Ͳ��˫���Ϳ�����ʽͨ�����൱�����ӳɹ���
    //˫��ͨ���Ĺ��̣���һ����绰�������źźͶԷ��ӵ绰�������źŵĹ��̣�
    //�൱����socket�������ݺʹ�socket�������ݡ�ͨ��������һ������绰���൱�ڹر�socket���������ӡ�

    /// <summary>
    /// �ͻ��˴����������ݵ�byte����
    /// </summary>
    byte[] dataByte=new byte[1024];
    public void Start()
    {
        //�̶���д��
        st = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        //�ͻ������ӷ���������
        //������Id��ַ �˿ں� ���� null
        st.BeginConnect("127.0.0.1", 1668, GameConectNetHangle, null);
    }
    /// <summary>
    /// ���ӷ�����������
    /// </summary>
    /// <param name="ar"></param>
    private void GameConectNetHangle(IAsyncResult ar)
    {
        try
        {
            //���ӳɹ�
            st.EndConnect(ar);
            //�ͻ��˽��շ�����
            st.BeginReceive(dataByte, 0, dataByte.Length, SocketFlags.None, ReceivHangdle, null);
        }
        catch(Exception)
        {
            //�����쳣
            throw;
        }
    }
    /// <summary>
    /// �ͻ��˽��ܷ��������ݼ������������ݴ����߳ɴ���
    /// </summary>
    /// <param name="ar"></param>
    /// <exception cref="NotImplementedException"></exception>
    private void ReceivHangdle(IAsyncResult ar)
    {
        throw new NotImplementedException();
    }
}
