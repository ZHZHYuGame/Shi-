using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// �û����ݹ�����
/// </summary>
public class ServerUserData : Singleton<ServerUserData>
{
    public Dictionary<int, UserData> userDict = new Dictionary<int, UserData>();
    /// <summary>
    /// ����һ���ͻ�����ҵ�����
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