using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Google;
using GameMessage;
using Google.Protobuf;

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
    /// <exception cref="NotImplementedException"></exception>
    private void BtnClickHandle()
    {
        //ͨ��UI�����Open�������

        //ͨ��������Ϣ����ȡ��������������Ϣ
        //���л�����
        C2S_Task_CurrComplete ct = new C2S_Task_CurrComplete();
        ct.PlayerId = 1;
        NetManager.Ins.SendMessager(NetID.C2S_Task_CurrComplete,ct.ToByteArray());

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
