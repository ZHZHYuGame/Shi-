using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedpointItem
{
    /// <summary>
    /// 当前节点类型
    /// </summary>
    public RedpointType type = RedpointType.Bag;
    /// <summary>
    /// 父类节点列表(向上通知，节点显示状态，是否影响父节点显示)
    /// </summary>
    public List<RedpointItem> parentItemList=new List<RedpointItem>(); 
    /// <summary>
    /// 子类节点列表(向下获取子节点的显示状态，更新当前节点的显示)
    /// </summary>
    public List<RedpointItem> childrenItemList=new List<RedpointItem>();
    /// <summary>
    /// 子节点数量（有的红点显示方案是红点内显示下面有几个红点可以查看点击）
    /// </summary>
    public int childrenCount = 0;
    /// <summary>
    /// 当前节点是否显示红点
    /// </summary>
    public bool isShow = false;
    /// <summary>
    /// 计算当前节点的所有子节点有多少为显示状态
    /// </summary>
    public void ChildrenCountHandle()
    {
        childrenCount = 0;
        foreach(var c in childrenItemList)
        {
            if(c.isShow)
                childrenCount++;
        }
    }

}
