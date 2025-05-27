using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ServerTaskManger : SingLeton<ServerTaskManger>
{
     public Dictionary<int ,TaskData>taskDic = new Dictionary<int ,TaskData>();
    public TaskData GetTask(int taskId)
    {
        return taskDic[taskId];
    }
    public List<TaskData> GetZXTask()
    {
        List<TaskData> taskList = new List<TaskData>();
        return taskList;
    }
}
public class TaskData
{
    public int taskId;
    public int taskCurrCount;
}