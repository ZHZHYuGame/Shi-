using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// UI的基类
/// 1.UI的基本显示操作
/// 2.UI的事件操作
/// 3.UI的红点操作（未知）
/// </summary>
public class UIBase : MonoBehaviour
{
    public virtual void Show() { }
    public virtual void Close() { }

    public virtual void RegRedPoint() { }

    public virtual void UnRegRedPoint() { }

    public virtual void AddEventListener() { }

    public virtual void UnAddEventListener() { }

    public virtual void LoadFinish() { }
}
