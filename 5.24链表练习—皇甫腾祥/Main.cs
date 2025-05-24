using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class LinkedListNode<T>
{
    public T Data;
    public LinkedListNode<T> Next;

    public LinkedListNode(T data)
    {
        Data = data;
        Next = null;
    }
}
public class Main : MonoBehaviour
{
    [SerializeField] private LinkedListNode<int> head;
    private int count;

    public int Count => count;
    public bool IsEmpty => head == null;

    void Start()
    {
        // 初始化链表
        head = null;
        count = 0;

        // 示例操作
        AddFirst(10);
        AddLast(20);
        AddFirst(5);
        AddLast(30);

        PrintList();

        Remove(20);

        PrintList();
    }

    // 在链表头部添加节点
    public void AddFirst(int data)
    {
        LinkedListNode<int> newNode = new LinkedListNode<int>(data);
        newNode.Next = head;
        head = newNode;
        count++;
    }

    // 在链表尾部添加节点
    public void AddLast(int data)
    {
        LinkedListNode<int> newNode = new LinkedListNode<int>(data);

        if (IsEmpty)
        {
            head = newNode;
        }
        else
        {
            LinkedListNode<int> current = head;
            while (current.Next != null)
            {
                current = current.Next;
            }
            current.Next = newNode;
        }
        count++;
    }

    // 删除指定值的节点
    public bool Remove(int data)
    {
        if (IsEmpty) return false;

        if (head.Data.Equals(data))
        {
            head = head.Next;
            count--;
            return true;
        }

        LinkedListNode<int> current = head;
        while (current.Next != null)
        {
            if (current.Next.Data.Equals(data))
            {
                current.Next = current.Next.Next;
                count--;
                return true;
            }
            current = current.Next;
        }

        return false;
    }

    // 打印链表内容
    public void PrintList()
    {
        string listString = "链表内容: ";
        LinkedListNode<int> current = head;
        while (current != null)
        {
            listString += current.Data + " -> ";
            current = current.Next;
        }
        listString += "null";
        Debug.Log(listString);
    }

    // 检查是否包含某个值
    public bool Contains(int data)
    {
        LinkedListNode<int> current = head;
        while (current != null)
        {
            if (current.Data.Equals(data))
                return true;
            current = current.Next;
        }
        return false;
    }
}
