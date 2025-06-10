using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventDispatcher : Singleton<EventDispatcher>
{
    public delegate void OnActionHandler(byte[] buffer);

    private Dictionary<ushort, List<OnActionHandler>> dict = new Dictionary<ushort, List<OnActionHandler>>();

    /// <summary>
    /// 添加监听
    /// </summary>
    /// <param name="protoCode"></param>
    /// <param name="handler"></param>
    public void AddEventListener(ushort protoCode, OnActionHandler handler)
    {
        if (dict.ContainsKey(protoCode))
        {
            dict[protoCode].Add(handler);
        }
        else
        {
            List<OnActionHandler> lstHandler = new List<OnActionHandler>();
            lstHandler.Add(handler);
            dict[protoCode] = lstHandler;
        }
    }
    /// <summary>
    /// 移除监听
    /// </summary>
    /// <param name="protoCode"></param>
    /// <param name="handler"></param>
    public void RemoveEventListener(ushort protoCode, OnActionHandler handler)
    {
        if (dict.ContainsKey(protoCode))
        {
            List<OnActionHandler> lstHandler = dict[protoCode];
            lstHandler.Remove(handler);
            if (lstHandler.Count == 0)
            {
                dict.Remove(protoCode);
            }
        }
    }
    /// <summary>
    /// 派发
    /// </summary>
    /// <param name="protoCode"></param>
    /// <param name="param"></param> <summary>
    /// 
    /// </summary>
    /// <param name="protoCode"></param>
    /// <param name="param"></param>
    public void Dispath(ushort protoCode, byte[] param)
    {
        if (dict.ContainsKey(protoCode))
        {
            List<OnActionHandler> lstHandler = dict[protoCode];
            if (lstHandler != null && lstHandler.Count > 0)
            {
                for (int i = 0; i < lstHandler.Count; i++)
                {
                    if (lstHandler[i] != null)
                    {
                        lstHandler[i](param);
                    }
                }
            }

        }
    }
}
