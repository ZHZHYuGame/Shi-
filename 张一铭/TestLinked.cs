using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//链表节点类  存储数据和指向下一个节点的引用
public class Node<T>
{
    public T Value { get; set; }   //节点存储的值
    public Node<T> Next { get; set; }  //指向下一个节点的引用(单向链表)

    public Node(T value)
    {
        Value = value;
        Next = null;//新节点默认指向null
    }

}
//自定义单项链表类:管理节点集合
public class CustomLinkedList<T>
{
    private Node<T> head;  //链表头节点(第一个节点)
    private Node<T> tail;  //链表尾节点(最后一个节点)
    private int count;     //链表节点数量

    public int Count=>count;    //获取链表长度(只读属性)
    public bool IsEmpty=>count==0;  //判断链表是否为空

    //在链表尾部添加新节点
    public void Add(T value)
    {
        Node<T> newNode=new Node<T>(value);

        //如果链表为空,新节点既是头节点也是尾节点
        if(tail==null)
        {
            head=tail=newNode;
        }
        else
        {
            //将当前尾节点的 Next 指向新节点
            tail.Next=newNode;
            //更新尾节点为新节点
            tail = newNode;
        }
        count++;// 链表长度加1
    }

    //从链表中移除第一个匹配值的节点
    public bool Remove(T value)
    {
        Node<T> previous = null;//记录当前节点的前一个节点
        Node<T> current = head;//从头部开始遍历

        while(current!=null)
        {
            //找到匹配值的节点
            if(current.Value.Equals(value))
            {
                //如果删除的是头节点
                if(previous==null)
                {
                    head = current.Next;
                    //如果链表只有一个节点 删除后tail也应为null
                    if (head == null) tail = null;
                }
                else
                {
                    //将前一个节点的Next指向当前节点的下一个节点
                    previous.Next=current.Next;
                    //如果删除的是尾节点，更新tail
                    if (current.Next == null) tail = previous;
                }
                count--;//链表长度减一
                return true;
            }
            //移动到下一个节点
            previous = current;
            current=current.Next;
        }
        return false;//未找到匹配值的节点
    }

    //打印链表中的所有节点值
    public void PrintAllNodes()
    {
        Node<T> current=head;
        while(current!=null)
        {
            Debug.Log(current.Value);
            current=current.Next;
        }

    }
}


public class TestLinked : MonoBehaviour
{
    void Start()
    {
        //创建自定义链表(存储整数)
        CustomLinkedList<int> custom = new CustomLinkedList<int>();

        //添加元素
        custom.Add(10);
        custom.Add(20);
        custom.Add(30);

        //打印链表
        Debug.Log("自定义链表内容:");
        custom.PrintAllNodes();

        //删除元素
        custom.Remove(20);
        Debug.Log("删除20后的链表:");
        custom.PrintAllNodes();

        //检查链表属性
        Debug.Log("链表长度:" + custom.Count);
        Debug.Log("检查链表是否为空:" + custom.IsEmpty);
    }

}
