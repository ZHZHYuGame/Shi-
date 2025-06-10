using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MMOMemoryTest : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //1.链接服务器
        NetWorkSocket.Instance.Connect("172.27.210.3", 1001);

        //EventDispatcher.GetInstance().AddEventListener(,OnReceiveProtoCallBack)
        //GlobalInit.instance.onReceiveProto = OnReceiveProtoCallBack;
    }

    private void OnReceiveProtoCallBack(ushort protoCode, byte[] buffer)
    {
        Debug.Log(protoCode);
        if (protoCode == ProtoDef.Main)
        {
            MainProto mainProto = MainProto.GetProto(buffer);
            Debug.Log(mainProto.Id);
            Debug.Log(mainProto.Name);
            Debug.Log(mainProto.Type);
            Debug.Log(mainProto.Pirce);
            Debug.Log(mainProto.isSucces);
            Debug.Log(mainProto.ErrorCode);
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            TestProto proto = new TestProto
            {
                Id = 1,
                Name = "测试",
                Type = 0,
                Pirce = 99.5f
            };
            NetWorkSocket.Instance.SendMsg(proto.ToArray());
        }
    }


}
