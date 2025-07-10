using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// UI基类
/// 1.ui的基本显示操作
/// 2.UI的事件操作
/// 3.UI的红点操作
/// 4.Ui的加载处理
/// </summary>
public class UIBase : MonoBehaviour
{
 
    public virtual void Show() { }
   
    public virtual void Close() { }
    /// UI模块的红点的触发逻辑
    /// 1.外部通知红点状态
    /// 2.内部获取红点缓存（保证红点状态更新准确
    /// </summary>
    public virtual void RegRedPoint() { }
    public virtual void UnRegRedPoint() { }
    public virtual void AddEventListener() { }
    public virtual void UnAddEventListener() { }
    public virtual void LoadFinish() { }
}
