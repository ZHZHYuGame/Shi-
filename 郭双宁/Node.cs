using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node<T>
{
    //���ڵ����������
    public T data
    {
        get;
        set;
    }
    //��һ���ڵ������
    public Node<T> next
    {
        get;
        set;
    }
    //���캯��
    public Node(T data)
    {
        this.data = data;
        this.next = null;
    }
}
