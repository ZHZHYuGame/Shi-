using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ServerTaskManager : Singleton<ServerTaskManager>
{
    public Dictionary<int, TaskData> taskDict = new Dictionary<int, TaskData>();
    public TaskData GetTask(int taskId)
    {
        return taskDict[taskId];
    }
    public List<TaskData> GetZXTask()
    {
        List<TaskData> list = new List<TaskData>();
        return list;
    }
}
public class TaskData {
    public int taskId;
    public int taskCurrCount;
}
