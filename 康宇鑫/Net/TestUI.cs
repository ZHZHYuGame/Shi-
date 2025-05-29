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
        //打开任务面板
        btn.onClick.AddListener(BtnClickHandle);
    }
    /// <summary>
    /// 触发一个通过服务器变化的行为
    /// 1.规则
    /// </summary>
    /// <exception cref="NotImplementedException"></exception>
    private void BtnClickHandle()
    {
        //通过UI框架来Open任务面板

        //通过网络消息来获取任务面板的任务信息
        //序列化数据
        C2S_Task_CurrComplete ct = new C2S_Task_CurrComplete();
        ct.PlayerId = 1;
        NetManager.Ins.SendMessager(NetID.C2S_Task_CurrComplete,ct.ToByteArray());

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
