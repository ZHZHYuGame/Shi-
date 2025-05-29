using GameMessage;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Google.Protobuf;
using GameMessage;

/// <summary>
/// ģ�������
/// </summary>
public class ServerNetManger : SingLeton<ServerNetManger>
{
     
    public void Start()
    {
        
    }
    /// <summary>
    /// �յ��ͻ�������
    /// </summary>
    /// <param name="netId">��ΪId</param>
    /// <param name="data">��Ϊ��������</param>
    public void ReceiveClientVo(int netId, byte[]data)
    {
        switch (netId)
        {
            case NetID.C2S_Task_CurrComplete:
                //�ͻ��������������ǰ��������,������
                //�����л��ͻ�������  �ȸ��PBЭ�����Ķ�Ӧ��Э�鷴���л�����
                C2S_Task_CurrComplete ct = C2S_Task_CurrComplete.Parser.ParseFrom(data);
                 
                C2S_Task_CurrCompleteHandle(ct);
                break;
            case NetID.C2S_Task_CompleteState:
                // �ͻ��������������ǰ�������,������
                C2S_Task_CompleteState tc=C2S_Task_CompleteState.Parser.ParseFrom(data);
                C2S_Task_CompleteStateHandle(tc);
                break;
            default:
                break;
        }
    }
    /// <summary>
    /// �ͻ��������������ǰ��������,������
    /// </summary>
    private void C2S_Task_CurrCompleteHandle(C2S_Task_CurrComplete cc)
    {
        //�����ҵ�����״̬��TaskID��
        //1.����Ƿ�������ϵͳ
        S2C_Task_CurrComplete tc = new S2C_Task_CurrComplete();
        if (ServerUsersData.Ins.GetUser(cc.PlayerID).currTaskId>0)
        {
            tc.TaskID = ServerUsersData.Ins.GetUser(cc.PlayerID).currTaskId;
            //����֧������
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
            //δ����
            tc.TaskID=0;      
        }
        SendClientVo(NetID.S2C_Task_CurrComplete, tc.ToByteArray());
        //2.����������У�����״̬��

        //3.֧������


    }
    /// <summary>
    /// �ͻ��������������ǰ�������,������
    /// </summary>
    private void C2S_Task_CompleteStateHandle(C2S_Task_CompleteState ctc)
    {
        //1.����Ƿ�������ϵͳ
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
    /// �������ݵ��ͻ���
    /// </summary>
    /// <param name="netId">��ΪId</param>
    /// <param name="data">��Ϊ��������</param>
    public void SendClientVo(int netId, byte[] data)
    {

    }

    
}
