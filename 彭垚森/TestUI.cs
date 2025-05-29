using System;
using System.Collections;
using System.Collections.Generic;
using GameMessage;
using UnityEngine;
using UnityEngine.UI;
using Google.Protobuf;
public class TestUI : MonoBehaviour
{
    public Button btn;
    // Start is called before the first frame update
    void Start()
    {
        btn.onClick.AddListener(BtnClickHandle);
    }

    private void BtnClickHandle()
    {
        C2S_Task_CurrComplete ct = new C2S_Task_CurrComplete();
        ct.PlayerID = 1;
        NetManager.Ins.SendMessage(NetID.C2S_Task_CurrComplete, ct.ToByteArray());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
