using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ServerTaskManager : Singleton<ServerTaskManager>
{
    /// <summary>
    /// 服务器任务管理
    /// </summary>
    /// <typeparam name="int"></typeparam>
    /// <typeparam name="TaskData"></typeparam>
    /// <returns></returns>

    Dictionary<int, TaskData> taskDict = new Dictionary<int, TaskData>();
    public TaskData GetTask(int taskId)
    {
        return taskDict[taskId];
    }
}
public class TaskData
{
    public int taskId;
    public int taskCurrCount;
}
