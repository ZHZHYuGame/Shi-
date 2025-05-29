using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LinkList<T> : IListDS<T>
{
    private Node<T> head;//；链表头结点
    private int count;//链表中的元素数量
    //属性 获取链表的长度
    public int Count
    {
        get
        {
            return count;
        }
    }
    /// <summary>
    /// 构造函数
    /// </summary>
    public LinkList()
    {
        head = null;
        count = 0;
    }
    /// <summary>
    /// 添加节点
    /// </summary>
    /// <param name="data"></param> <summary>
    /// 
    /// </summary>
    /// <param name="data"></param>
    public void Add(T data)
    {
        Node<T> newNode = new Node<T>(data);
        if (head == null)
        {
            head = newNode;
        }
        else
        {
            Node<T> current = head;
            while (current.Next != null)
            {
                current = current.Next;
            }
            current.Next = newNode;
        }
        count++;
    }
    /// <summary>
    /// 获取节点
    /// </summary>
    /// <param name="index"></param>
    /// <returns></returns>
    public T GetAt(int index)
    {
        if (index < 0 || index >= count)
        {
            Debug.Log("超出索引");
        }
        Node<T> current = head;
        for (int i = 0; i < index; i++)
        {
            current = current.Next;
        }
        return current.Data;
    }
    /// <summary>
    /// 删除节点
    /// </summary>
    /// <param name="index"></param>
    public void RemoveAt(int index)
    {
        if (index < 0 || index >= count)
        {
            Debug.Log("超出索引");
        }
        if (index == 0)
        {
            head = head.Next;
        }
        else
        {
            Node<T> current = head;
            for (int i = 1; i <= index - 1; i++)
            {
                current = current.Next;
            }
            current.Next = current.Next.Next;
        }
        count--;
    }

    public void PrintAll()
    {
        Node<T> current = head;
        while (current != null)
        {
            Debug.Log(current.Data);
            current = current.Next;
        }
    }

    public void Insert(int index, T data)
    {
        Node<T> newNode = new Node<T>(data);
        if (index == 0)//插入头结点
        {
            newNode.Next = head;
            head = newNode;
        }
        else
        {
            Node<T> temp = head;
            for (int i = 1; i <= index - 1; i++)
            {
                temp = temp.Next;
            }
            Node<T> preNode = temp;
            Node<T> current = temp.Next;
            preNode.Next = newNode;
            newNode.Next = current;
        }

    }

    public void RemoveAll()
    {

    }
}
