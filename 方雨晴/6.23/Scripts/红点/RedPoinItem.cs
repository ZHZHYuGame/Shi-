using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 红点节点类
/// </summary>
public class RedPoinItem 
{
    /// <summary>
    /// 当前节点类型
    /// </summary>
    public RedPoinType type = RedPoinType.Bag;
    /// <summary>
    /// 父类节点列表（向上通知，子节点显示状态,是否影响父节点显示
    /// </summary>
    public List<RedPoinItem> PoinItemList = new List<RedPoinItem>();
    /// <summary>
    /// 子类节点列表  向下获取子节点显示状态,更新当前节点的显示
    /// </summary>
    public List<RedPoinItem> childrenItemList = new List<RedPoinItem>();
    /// <summary>
    /// 如果你不显示数量不一定用的到
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
    public void ChinldrenConutHandle()
    {
        childrenCount = 0;
        foreach (var item in childrenItemList)
        {
            if(item.isShow)
            {
                childrenCount++;
            }
        }
    }
}
