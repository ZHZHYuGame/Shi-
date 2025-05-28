using GameMessage;
using Google.Protobuf;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// ģ�������
/// </summary>
public class ServerNetManager : Singleton<ServerNetManager>
{
    public void Start()
    {

    }
    /// <summary>
    /// �յ��ͻ�������
    /// </summary>
    /// <param name="netId">��ΪID</param>
    /// <param name="data">��Ϊ��������</param>
    public void ReceiveClientVo(int netId, byte[] data)
    {
        switch(netId)
        {
            case NetID.C2S_Task_CurrComplete:
                //�ͻ��������������ǰ�������񣬽������
                //�����л��ͻ�������,�ȸ��PBЭ�����Ķ�Ӧ��Э�鷴���л�����
                C2S_Task_CurrComplete ct = C2S_Task_CurrComplete.Parser.ParseFrom(data);
                C2S_Task_CurrCompleteHandle(ct);
                break;
            case NetID.C2S_Task_CompleteState:
                //�ͻ��������������ǰ������ȣ��������
                //�������л�����Ҫ���㷢��������ת����ԭʼ�Ķ�������
                C2S_Task_CompleteState tc=C2S_Task_CompleteState.Parser.ParseFrom(data);
                C2S_Task_CompleteStateHandle(tc);
                break;
            default:
                break;  
        }
    }
    /// <summary>
    /// �ͻ��������������ǰ�������񣬽������
    /// </summary>
    private void C2S_Task_CurrCompleteHandle(C2S_Task_CurrComplete cc)
    {
        //�����ҵ�����״̬��TaskId��
        //1.����Ƿ�������ϵͳ
        S2C_Task_CurrComplete tc = new S2C_Task_CurrComplete();
        if (ServerUserData.Ins.GetUser(cc.PlayerId).currTaskId>0)
        {
            //���������ͻ��˵��������л�
            tc.TaskId=ServerUserData.Ins.GetUser(cc.PlayerId).currTaskId;
            //����֧������
            List<TaskData> zxList= ServerTaskManager.Ins.GetZXTask();
            //
            if(zxList!=null&&zxList.Count>0)
            {
                foreach (var task in zxList)
                {
                    zxTask t1=new zxTask();
                    t1.TaskID = task.taskId;
                    tc.ZxTaskList.Add(t1);
                }
            }
        }
        else
        {
            //δ����
            tc.TaskId = 0;
        }
        SendClientVo(NetID.S2C_Task_CurrComplete, tc.ToByteArray());
        //2.����������У�����״̬��

        //3.֧������״̬
    }
    /// <summary>
    /// �ͻ��������������ǰ������ȣ��������
    /// </summary>
    private void C2S_Task_CompleteStateHandle(C2S_Task_CompleteState ctc)
    {
        //����Ƿ�������ϵͳ
        S2C_Task_CompleteState stc = new S2C_Task_CompleteState();
        if (ServerUserData.Ins.GetUser(ctc.PlayerId).currTaskId > 0)
        {
            //��ǰ�������
            TaskData td = ServerTaskManager.Ins.GetTask(ctc.TaskId);
            if (td!=null)
            {
                stc.R = Result.Cg;
                stc.CurrCount=td.taskCurrCount;
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
    /// <param name="netId">��ΪID</param>
    /// <param name="data">��Ϊ��������</param>
    public void SendClientVo(int netId, byte[] data)
    {

    }
}
