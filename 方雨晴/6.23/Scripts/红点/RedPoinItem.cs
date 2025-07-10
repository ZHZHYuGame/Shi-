using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// ���ڵ���
/// </summary>
public class RedPoinItem 
{
    /// <summary>
    /// ��ǰ�ڵ�����
    /// </summary>
    public RedPoinType type = RedPoinType.Bag;
    /// <summary>
    /// ����ڵ��б�����֪ͨ���ӽڵ���ʾ״̬,�Ƿ�Ӱ�츸�ڵ���ʾ
    /// </summary>
    public List<RedPoinItem> PoinItemList = new List<RedPoinItem>();
    /// <summary>
    /// ����ڵ��б�  ���»�ȡ�ӽڵ���ʾ״̬,���µ�ǰ�ڵ����ʾ
    /// </summary>
    public List<RedPoinItem> childrenItemList = new List<RedPoinItem>();
    /// <summary>
    /// ����㲻��ʾ������һ���õĵ�
    /// �ӽڵ��������еĺ����ʾ�����Ǻ������ʾ�����м��������Բ鿴�����
    /// </summary>
    public int childrenCount = 0;

    /// <summary>
    /// ��ǰ�ڵ��Ƿ���ʾ���
    /// </summary>
    public bool isShow = false;
    /// <summary>
    /// ���㵱ǰ�ڵ�������ӽڵ��ж���Ϊ��ʾ״̬
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
