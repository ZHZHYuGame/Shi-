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
        // ��ʼ������
        head = null;
        count = 0;

        // ʾ������
        AddFirst(10);
        AddLast(20);
        AddFirst(5);
        AddLast(30);

        PrintList();

        Remove(20);

        PrintList();
    }

    // ������ͷ����ӽڵ�
    public void AddFirst(int data)
    {
        LinkedListNode<int> newNode = new LinkedListNode<int>(data);
        newNode.Next = head;
        head = newNode;
        count++;
    }

    // ������β����ӽڵ�
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

    // ɾ��ָ��ֵ�Ľڵ�
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

    // ��ӡ��������
    public void PrintList()
    {
        string listString = "��������: ";
        LinkedListNode<int> current = head;
        while (current != null)
        {
            listString += current.Data + " -> ";
            current = current.Next;
        }
        listString += "null";
        Debug.Log(listString);
    }

    // ����Ƿ����ĳ��ֵ
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
