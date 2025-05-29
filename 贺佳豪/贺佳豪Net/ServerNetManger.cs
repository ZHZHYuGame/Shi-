using GameMessage;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Google.Protobuf;
using GameMessage;

/// <summary>
/// 模拟服务器
/// </summary>
public class ServerNetManger : SingLeton<ServerNetManger>
{
     
    public void Start()
    {
        
    }
    /// <summary>
    /// 收到客户端数据
    /// </summary>
    /// <param name="netId">行为Id</param>
    /// <param name="data">行为具体数据</param>
    public void ReceiveClientVo(int netId, byte[]data)
    {
        switch (netId)
        {
            case NetID.C2S_Task_CurrComplete:
                //客户端向服务器请求当前进行任务,计算结果
                //反序列化客户端数据  谷歌的PB协议规则的对应的协议反序列化操作
                C2S_Task_CurrComplete ct = C2S_Task_CurrComplete.Parser.ParseFrom(data);
                 
                C2S_Task_CurrCompleteHandle(ct);
                break;
            case NetID.C2S_Task_CompleteState:
                // 客户端向服务器请求当前任务进度,计算结果
                C2S_Task_CompleteState tc=C2S_Task_CompleteState.Parser.ParseFrom(data);
                C2S_Task_CompleteStateHandle(tc);
                break;
            default:
                break;
        }
    }
    /// <summary>
    /// 客户端向服务器请求当前进行任务,计算结果
    /// </summary>
    private void C2S_Task_CurrCompleteHandle(C2S_Task_CurrComplete cc)
    {
        //这个玩家的任务状态（TaskID）
        //1.玩家是否开启任务系统
        S2C_Task_CurrComplete tc = new S2C_Task_CurrComplete();
        if (ServerUsersData.Ins.GetUser(cc.PlayerID).currTaskId>0)
        {
            tc.TaskID = ServerUsersData.Ins.GetUser(cc.PlayerID).currTaskId;
            //查找支线任务
           List<TaskData>zxList=  ServerTaskManger.Ins.GetZXTask();
            //
            if(zxList!=null&&zxList.Count>0)
            {
                foreach (var task in zxList)
                {
                    ZXTask t1 = new ZXTask();
                    t1.TaskID = task.taskId;
                    tc.ZxTaskList.Add(t1);
                }
            }
        }
        else
        {
            //未开启
            tc.TaskID=0;      
        }
        SendClientVo(NetID.S2C_Task_CurrComplete, tc.ToByteArray());
        //2.主线任务必有（正常状态）

        //3.支线任务


    }
    /// <summary>
    /// 客户端向服务器请求当前任务进度,计算结果
    /// </summary>
    private void C2S_Task_CompleteStateHandle(C2S_Task_CompleteState ctc)
    {
        //1.玩家是否开启任务系统
        S2C_Task_CompleteState stc = new S2C_Task_CompleteState();
        if (ServerUsersData.Ins.GetUser(ctc.PlayerID).currTaskId > 0)
        {
            TaskData td=ServerTaskManger.Ins.GetTask(ctc.TaskID);
            if(td != null)
            {
                stc.R = Result.Cg;
                stc.CurrCount = td.taskCurrCount;
            }
            else
            {
                stc.R = Result.Sb1;
            }
        }
        SendClientVo(NetID.S2C_Task_CompleteState,stc.ToByteArray());
    }
    /// <summary>
    /// 发送数据到客户端
    /// </summary>
    /// <param name="netId">行为Id</param>
    /// <param name="data">行为具体数据</param>
    public void SendClientVo(int netId, byte[] data)
    {

    }

    
}
