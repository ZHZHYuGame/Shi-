using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Google.Protobuf;
using GameMessage;
using System;
/// <summary>
/// 服务器网络管理
/// </summary> <summary>
/// 
/// </summary>
public class ServerNetManager : Singleton<ServerNetManager>
{
    /// <summary>
    /// 接受客户端的请求
    /// </summary>
    /// <param name="netID"></param>
    /// <param name="data"></param>
    public void ReceiveClientVo(int netID, byte[] data)
    {
        switch (netID)
        {
            case NetID.C2S_Task_CurrComplete:
                //客户端向服务器请求当前进行任务，计算结果
                C2S_Task_CurrComplete c2s = C2S_Task_CurrComplete.Parser.ParseFrom(data);
                C2S_Task_CurrCompleteHandle(c2s);
                break;
            case NetID.C2S_Task_CompleteState:
                //客户端向服务器请求当前任务进度，计算结果
                C2S_Task_CompleteState s2c = C2S_Task_CompleteState.Parser.ParseFrom(data);
                C2S_Task_CompleteStateHandle(s2c);
                break;
            default:
                break;
        }
    }
    /// <summary>
    /// 处理客户端的当前任务申请 并处理
    /// </summary>
    /// <param name="cc"></param>
    private void C2S_Task_CurrCompleteHandle(C2S_Task_CurrComplete cc)
    {
        S2C_Task_CurrComplete s2c = new S2C_Task_CurrComplete();
        if (ServerUserData.GetInstance().GetUser(cc.PlayerID).currTaskId > 0)
        {
            s2c.TaskID = ServerUserData.GetInstance().GetUser(cc.PlayerID).currTaskId;
            //查找支线任务
        }
        else
        {
            s2c.TaskID = 0;
        }
        SendClientVo();
    }
    /// <summary>
    /// 处理客户端的任务进度申请 并处理
    /// </summary>
    /// <param name="cc"></param>
    private void C2S_Task_CompleteStateHandle(C2S_Task_CompleteState cc)
    {
        //这个玩家的任务状态（TaskID）
        //1.玩家是否开启任务系统
        S2C_Task_CompleteState s2c = new S2C_Task_CompleteState();
        if (ServerUserData.GetInstance().GetUser(cc.PlayerID).currTaskId > 0)
        {
            TaskData data = ServerTaskManager.GetInstance().GetTask(cc.TaskID);
            if (data != null)
            {
                //s2c.CurrCount = data.taskCurrCount;
            }
        }
        else
        {

        }
    }
    private void SendClientVo()
    {

    }

}
