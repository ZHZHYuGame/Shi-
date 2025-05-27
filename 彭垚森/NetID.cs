using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 网络ID协议号
/// </summary>
public class NetID
{
    //客户端向服务器请求当前进行任务
    public const int C2S_Task_CurrComplete = 1001;
    //服务器向客户端回馈当前进行任务
    public const int S2C_Task_CurrComplete = 1002;
    //客户端向服务器请求当前任务进度
    public const int C2S_Task_CompleteState = 1003;
    //服务器向客户端回馈当前任务进度
    public const int S2C_Task_CompleteState = 1004;
}
