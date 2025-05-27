using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ServerUsersData : Singleton<ServerUsersData>
{
    Dictionary<int, UserData> userDict = new Dictionary<int, UserData>();
    /// <summary>
    /// 返回一个客户端玩家的数据
    /// </summary>
    /// <param name="userId"></param>
    /// <returns></returns>
    public UserData GetUser(int userId)
    {
        return userDict[userId];
    }
}
public class UserData
{
    public int userId;
    public int currTaskId;
}
