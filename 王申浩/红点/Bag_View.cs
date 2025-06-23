using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bag_View : UIBase
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public override void LoadFinish()
    {
        base.LoadFinish();
    }

    public override void Show()
    {
        base.Show();    
    }
    public override void Close()
    {
        base.Close();
    }
    /// <summary>
    /// UI模版的红点触发逻辑处理
    /// 1.外部通知红点状态
    /// 2.内部获取红点缓存（保证红点状态更新准确）
    /// </summary>
    public override void RegRedPoint()
    {
        RedPointMgr<int>.instance.RegPointToUIHandle(RedpointType.GoodUse, GoodUseHandle);
        RedPointMgr<string>.instance.RegPointToUIHandle(RedpointType.GoodUse, GoodUseHandle);
    }

    private void GoodUseHandle(string obj)
    {
        
    }

    /// <summary>
    /// 物品使用红点显示
    /// </summary>

    private void GoodUseHandle(int obj)
    {
        //物品使用位置
        int useid = int.Parse(obj.ToString());
    }

    public override void UnRegRedPoint()
    {
        base.UnRegRedPoint();
    }

    public override void AddEventListener()
    {
        base.AddEventListener();
    }

    public override void UnAddEventListener()
    {
        base.UnAddEventListener();
    }
}
