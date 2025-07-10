using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 红点管理器
/// </summary>
public class RedPointMgr<T> : Singleton<RedPointMgr<T>>
{
    /// <summary>
    /// 管理红点影响关系
    /// </summary>
    Dictionary<RedPoinType, RedPoinItem> redPointDict = new Dictionary<RedPoinType, RedPoinItem>();
    /// <summary>
    /// 管理红点刷新ui
    /// </summary>
    Dictionary<RedPoinType, Action<T>> redPointToUIDict = new Dictionary<RedPoinType, Action<T>>();
    void Srart()
    {
        //注册游戏所有红点关系
        RedPoinToPoint(RedPoinType.Root, RedPoinType.MainIcon);
        RedPoinToPoint(RedPoinType.MainIcon, RedPoinType.Bag);
        RedPoinToPoint(RedPoinType.MainIcon, RedPoinType.Shop);
        RedPoinToPoint(RedPoinType.MainIcon, RedPoinType.Pet);

        //坐骑功能下红点关系
        RedPoinToPoint(RedPoinType.MainIcon, RedPoinType.Mount);
        RedPoinToPoint(RedPoinType.Mount, RedPoinType.Normal);
        RedPoinToPoint(RedPoinType.Mount, RedPoinType.Mount_Activity);
    }
    /// <summary>
    /// 注册红点节点关系
    /// </summary>
    /// <param name="currPoint"></param>
    /// <param name="parenPoint"></param>
    private void RedPoinToPoint(RedPoinType currPoint, RedPoinType parenPoint)
    {
        //是否存在当前节点
        if (!redPointDict.ContainsKey(currPoint))
        {
            //当前红点节点
            RedPoinItem cueeTtem = new RedPoinItem
            {
                type = currPoint
            };
        }
        //判断当前红点的父节点是否存在
        if (redPointDict[currPoint].PoinItemList.Find(x => x.type == parenPoint) == null)
        {
            //父级红点节点
            RedPoinItem parentItem = new RedPoinItem
            {
                type = parenPoint
            };
            //添加父节点到当前的父节点里
            redPointDict[currPoint].PoinItemList.Add(parentItem);
        }
        //要给父节点里添加当前节点（子节点）
        foreach (var item in redPointDict[currPoint].PoinItemList)
        {
            item.childrenItemList.Add(redPointDict[currPoint]);
        }

    }
    /// <summary>
    /// 注册红点类型与ui的交互处理（红点的刷新
    /// </summary>
    /// <param name="type"></param>
    /// <param name="act"></param>
    public void RegPointToUIHandle(RedPoinType type, Action<T> act)
    {
        if (!redPointToUIDict.ContainsKey(type))
        {
            redPointToUIDict.Add(type, act);
        }
        else
        {
            //一个红点类型的显示状态变化可以影响多个位置的显隐
            redPointToUIDict[type] += act;
        }
    }
    /// <summary>
    /// 红点刷新（服务器通知红点状态变化--服务器红点协议通知
    /// </summary>
    /// <param name="type"></param>
    /// <param name="state"></param>
    public void RedPointUpdata(RedPoinType type, bool state)
    {
        //改变自己影响别人
        //红点之间关系（确定影响的节点关系
        if (redPointDict.ContainsKey(type))
        {
            //当前红点刷新
            redPointDict[type].isShow = state;
            //通知父类显隐状态

            foreach (var par in redPointDict[type].PoinItemList)
            {
                bool anychildShow = false;
                //遍历子节点的列表
                foreach (var item in par.childrenItemList)
                {
                    //判断显示状态
                    if (item.isShow)
                    {
                        anychildShow = true;
                        

                        break;
                    }
                    anychildShow = false;
                    redPointToUIDict[par.type]?.Invoke(default(T));
                    //如果父节点发现变化，继续通知父节点的父节点
                    if (par.PoinItemList.Count > 0)
                    {
                        RedPointUpdata(par.type, anychildShow);
                    }
                }
            }
        }
        //红点状态影响的UI状态显示
        if (redPointToUIDict.ContainsKey(type))
        {
            T stataAst = (T)(object)state;
            redPointToUIDict[type]?.Invoke(stataAst);
        }
    }
}