using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ServerTeskManager : SingLeton<ServerTeskManager>
{
    public Dictionary<int,TaskData> taskDic= new Dictionary<int,TaskData>();

    public TaskData GetTask(int taskid)
    {
         return taskDic[taskid];
    }
    public List<TaskData> GetZXTask()
    {
        List<TaskData> list= new List<TaskData>();
        return list;
    }
}
public class TaskData
{
    public int taskId;
    public int taskCurrCount;
}
