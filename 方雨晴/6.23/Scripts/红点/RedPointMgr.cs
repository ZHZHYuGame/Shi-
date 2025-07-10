using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// ��������
/// </summary>
public class RedPointMgr<T> : Singleton<RedPointMgr<T>>
{
    /// <summary>
    /// ������Ӱ���ϵ
    /// </summary>
    Dictionary<RedPoinType, RedPoinItem> redPointDict = new Dictionary<RedPoinType, RedPoinItem>();
    /// <summary>
    /// ������ˢ��ui
    /// </summary>
    Dictionary<RedPoinType, Action<T>> redPointToUIDict = new Dictionary<RedPoinType, Action<T>>();
    void Srart()
    {
        //ע����Ϸ���к���ϵ
        RedPoinToPoint(RedPoinType.Root, RedPoinType.MainIcon);
        RedPoinToPoint(RedPoinType.MainIcon, RedPoinType.Bag);
        RedPoinToPoint(RedPoinType.MainIcon, RedPoinType.Shop);
        RedPoinToPoint(RedPoinType.MainIcon, RedPoinType.Pet);

        //���﹦���º���ϵ
        RedPoinToPoint(RedPoinType.MainIcon, RedPoinType.Mount);
        RedPoinToPoint(RedPoinType.Mount, RedPoinType.Normal);
        RedPoinToPoint(RedPoinType.Mount, RedPoinType.Mount_Activity);
    }
    /// <summary>
    /// ע����ڵ��ϵ
    /// </summary>
    /// <param name="currPoint"></param>
    /// <param name="parenPoint"></param>
    private void RedPoinToPoint(RedPoinType currPoint, RedPoinType parenPoint)
    {
        //�Ƿ���ڵ�ǰ�ڵ�
        if (!redPointDict.ContainsKey(currPoint))
        {
            //��ǰ���ڵ�
            RedPoinItem cueeTtem = new RedPoinItem
            {
                type = currPoint
            };
        }
        //�жϵ�ǰ���ĸ��ڵ��Ƿ����
        if (redPointDict[currPoint].PoinItemList.Find(x => x.type == parenPoint) == null)
        {
            //�������ڵ�
            RedPoinItem parentItem = new RedPoinItem
            {
                type = parenPoint
            };
            //��Ӹ��ڵ㵽��ǰ�ĸ��ڵ���
            redPointDict[currPoint].PoinItemList.Add(parentItem);
        }
        //Ҫ�����ڵ�����ӵ�ǰ�ڵ㣨�ӽڵ㣩
        foreach (var item in redPointDict[currPoint].PoinItemList)
        {
            item.childrenItemList.Add(redPointDict[currPoint]);
        }

    }
    /// <summary>
    /// ע����������ui�Ľ�����������ˢ��
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
            //һ��������͵���ʾ״̬�仯����Ӱ����λ�õ�����
            redPointToUIDict[type] += act;
        }
    }
    /// <summary>
    /// ���ˢ�£�������֪ͨ���״̬�仯--���������Э��֪ͨ
    /// </summary>
    /// <param name="type"></param>
    /// <param name="state"></param>
    public void RedPointUpdata(RedPoinType type, bool state)
    {
        //�ı��Լ�Ӱ�����
        //���֮���ϵ��ȷ��Ӱ��Ľڵ��ϵ
        if (redPointDict.ContainsKey(type))
        {
            //��ǰ���ˢ��
            redPointDict[type].isShow = state;
            //֪ͨ��������״̬

            foreach (var par in redPointDict[type].PoinItemList)
            {
                bool anychildShow = false;
                //�����ӽڵ���б�
                foreach (var item in par.childrenItemList)
                {
                    //�ж���ʾ״̬
                    if (item.isShow)
                    {
                        anychildShow = true;
                        

                        break;
                    }
                    anychildShow = false;
                    redPointToUIDict[par.type]?.Invoke(default(T));
                    //������ڵ㷢�ֱ仯������֪ͨ���ڵ�ĸ��ڵ�
                    if (par.PoinItemList.Count > 0)
                    {
                        RedPointUpdata(par.type, anychildShow);
                    }
                }
            }
        }
        //���״̬Ӱ���UI״̬��ʾ
        if (redPointToUIDict.ContainsKey(type))
        {
            T stataAst = (T)(object)state;
            redPointToUIDict[type]?.Invoke(stataAst);
        }
    }
}