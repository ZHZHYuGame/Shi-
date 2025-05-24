using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LinkList<T>
{
    private Node<T> head;//链表头
    private int count;//链表长度
    //用于获取链表长度
    public int Count
    {
        get
        {
            return count;
        }
    }
    //判断链表是否为空
    public bool IsEmpty
    {
        get
        {
            return count == 0;
        }
    }

    //在链表头部添加节点
    #region 
    //AddFirst 方法用于在链表的头部添加一个新节点：
    //LinkedListNode<T> newNode = new LinkedListNode<T>(data); 创建一个新的节点对象，传入要存储的数据 data。
    //newNode.Next = head; 将新节点的 Next 引用指向原来的头节点（如果原来有头节点的话）。
    //head = newNode; 更新链表的头节点为新创建的节点。
    //count++; 增加链表中节点的数量。
    #endregion
    public void AddFirst(T data)
    {
        Node<T> newNode = new Node<T>(data);
        newNode.next = head;
        head = newNode;
        count++;
    }
    //在链表尾部添加节点
    #region
    //AddLast 方法用于在链表的尾部添加一个新节点：
    //首先检查链表是否为空，如果为空则调用 AddFirst 方法添加节点并返回。
    //创建一个新的节点对象 newNode。
    //使用一个临时变量 current 从链表的头节点开始遍历，通过 while (current.Next != null) 循环找到链表的最后一个节点。
    //将最后一个节点的 Next 引用指向新创建的节点。
    //增加链表中节点的数量。
    #endregion
    public void AddLast(T data)
    {
        if (IsEmpty)
        {
            AddFirst(data);
            return;
        }
        Node<T> newNode = new Node<T>(data);
        Node<T> current = head;
        //找到最后一个节点
        while (current.next != null)
        {
            current = current.next;
        }
        current.next = newNode;
        count++;
    }
    #region
    //RemoveFirst 方法用于从链表的头部移除一个节点：
    //首先检查链表是否为空，如果为空则返回 false 表示移除操作失败。
    //将头节点的引用更新为原来头节点的下一个节点，即 head = head.Next;。
    //减少链表中节点的数量。
    //返回 true 表示移除操作成功。
    #endregion
    //从头部移除链表
    public bool RemoveFirst()
    {
        if (IsEmpty)
        {
            return false;
        }
        head = head.next;
        count--;
        return true;
    }
    //从链表中查找节点
    public bool Contains(T data)
    {
        Node<T> tempNode = head;
        while (tempNode != null)
        {
            if (tempNode.data.Equals(data))
            {
                return true;
            }
            tempNode = tempNode.next;
        }
        return false;
    }
    //清空链表
    public void Clear()
    {
        head = null;
        count = 0;
    }
}
