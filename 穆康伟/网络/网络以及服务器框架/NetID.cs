using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Google.Protobuf;
using GameMessage;
using JetBrains.Annotations;


public class NetID
{
    /// <summary>
    /// 客户端向服务器请求当前进行任务
    /// </summary>
    public const int C2S_Task_CurrComplete = 1001;
    /// <summary>
    /// 服务器向客户端回馈当前进行任务
    /// </summary>
    public const int S2C_Task_CurrComplete = 1002;
    /// <summary>
    /// 客户端向服务器请求当前任务进度
    /// </summary>
    public const int C2S_Task_CompleteState = 1003;
    /// <summary>
    /// 服务器向客户端回馈当前任务进度
    /// </summary>
    public const int S2C_Task_CompleteState = 1004;


}
