using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Google.Protobuf;
using GameMessage;
public class TestUI : MonoBehaviour
{
    Button btn;

    static TestUI _instance;
    // Start is called before the first frame update
    void Start()
    {
        //���������
        btn.onClick.AddListener(BtnClickHandle);
    }
    /// <summary>
    /// ����һ��ͨ���������仯����Ϊ 
    /// 1.���� 
    /// </summary>
    private void BtnClickHandle()
    {
        //ͨ��UI�����Open�������

        //ͨ��������Ϣ����ȡ��������������Ϣ
        //���л���������
        C2S_Task_CurrComplete ct = new C2S_Task_CurrComplete();
        ct.PlayerID = 1;
        NetManager.ins.SendMessage(NetID.C2S_Task_CurrComplete,ct.ToByteArray());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
