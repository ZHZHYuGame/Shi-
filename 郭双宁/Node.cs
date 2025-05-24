using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node<T>
{
    //本节点包含的数据
    public T data
    {
        get;
        set;
    }
    //下一个节点的引用
    public Node<T> next
    {
        get;
        set;
    }
    //构造函数
    public Node(T data)
    {
        this.data = data;
        this.next = null;
    }
}
