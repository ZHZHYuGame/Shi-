using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bag_View : UIBase
{
    public override void Close()
    {
        base.Close();
    }
    public override void Show()
    {
        base.Show();
    }
    public override void LoadFinish()
    {
        base.LoadFinish();
    }

    /// UI模块的红点的触发逻辑
    /// 1.外部通知红点状态
    /// 2.内部获取红点缓存（保证红点状态更新准确
    /// </summary>
    public override void RegRedPoint()
    {
        RedPointMgr<int>.ins.RegPointToUIHandle(RedPoinType.GoodUse, GoodUesHandle);
        RedPointMgr<string>.ins.RegPointToUIHandle(RedPoinType.GoodUse, GoodUesHandle);
    }

    private void GoodUesHandle(string obj)
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// 物品使用红点显示
    /// </summary>
    private void GoodUesHandle(int obj)
    {
        //物品使用的位置
        int usedID = obj;

    }


   
    public override void UnRegRedPoint()
    {

    }
    public override void AddEventListener()
    {
    }

    public override void UnAddEventListener()
    {

    }

   
}
