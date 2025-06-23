using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedPointMgr<T> : Singleton<RedPointMgr<T>>
{
    // Start is called before the first frame update
    Dictionary<RedpointType,RedpointItem> redPointDict=new  Dictionary<RedpointType,RedpointItem>();
    Dictionary<RedpointType, Action<T>> redPointToUIDict = new Dictionary<RedpointType, Action<T>>();
    void Start()
    {
        
        //注册游戏的所有红点关系,前面的是后面的父级
        RedPointToPoint(RedpointType.Root, RedpointType.MainIcon);
        RedPointToPoint(RedpointType.MainIcon, RedpointType.Bag);
        RedPointToPoint(RedpointType.MainIcon, RedpointType.Shop);
        RedPointToPoint(RedpointType.MainIcon, RedpointType.Pet);
        RedPointToPoint(RedpointType.MainIcon, RedpointType.Mount);
        //坐骑功能下红点关系
        //正常坐骑
        RedPointToPoint(RedpointType.Mount, RedpointType.Normal);
        //活动坐骑
        RedPointToPoint(RedpointType.Mount, RedpointType.Mount_Activity);
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    /// <summary>
    /// 注册红点节点关系
    /// </summary>
    /// <param name="currPoint"></param>
    /// <param name="parentPoint"></param>
    public void RedPointToPoint(RedpointType currPoint,RedpointType parentPoint)
    {
        //是否存在当前红点节点
        if(!redPointDict.ContainsKey(currPoint))
        {
            //当前红点节点
            RedpointItem currItem = new RedpointItem()
            {
                type = currPoint
            };
        }
        //判断当前节点的父节点是否存在
        if (redPointDict[currPoint].parentItemList.Find(x=>x.type==parentPoint)==null)
        {
            //父级红点节点
            RedpointItem parenItem = new RedpointItem()
            {
                type = parentPoint
            };
            //添加父节点到当前节点的父节点里
            redPointDict[currPoint].parentItemList.Add(parenItem);
        }
        //要给父节点里添加当前节点（子节点）
        foreach(var pItem in redPointDict[currPoint].parentItemList)
        {
            pItem.childrenItemList.Add(redPointDict[currPoint]);
        }
      
    }
    /// <summary>
    /// 注册红点类型与UI的交互处理（红点的显示刷新）
    /// </summary>
    public void RegPointToUIHandle(RedpointType type,Action<T> act)
    {
        if(!redPointToUIDict.ContainsKey(type))
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
    /// 红点刷新（服务器通知红点状态变化---服务器红点协议通知）
    /// </summary>
    /// <param name="type"></param>
    /// <param name="state"></param>
    public void RedPointUpdata(RedpointType type,bool state)
    {
        //红点之间关系（确定影响的节点关系）
        if(redPointDict.ContainsKey(type))
        {
            //当前节点红点显示状态刷新
            redPointDict[type].isShow = state;
            //通知父级显隐状态（父级要遍历子节点的列表，判断子节点的显示状态，如有一个为显示状态，则父级节点红点为显示状态，如果全部节点全部为不显示状态，则父节点为不显示状态）
            foreach(var par in redPointDict[type].parentItemList)
            {
                //深层节点可能会有多少父级节点存在
                foreach(var chi in par.childrenItemList)
                {
                    //如果有一个子节点的显示状态为真，这个父节点的显示状态则为真
                    if(chi.isShow)
                    {
                        par.isShow = true;
                        redPointToUIDict[par.type]?.Invoke(default(T));
                        break;
                    }
                    par.isShow = false;
                    //只传递1次
                }
            }
        }
        //红点状态影响的UI状态显示
        if(redPointToUIDict.ContainsKey(type))
        {

        }
    }
    
}
