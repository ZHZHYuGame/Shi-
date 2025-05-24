using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main:MonoBehaviour
{
    //标准库的双线链表(LinkedList<T>)
    LinkedList<int> Ints = new LinkedList<int>();
    // Start is called before the first frame update
    void Start()
    {

        Ints.AddFirst(1);//头部插入
        Ints.AddLast(5);//尾部插入
        Ints.AddLast(9);//尾部插入
        Ints.AddAfter(Ints.Last, 3);//在指定节点插入
        Ints.AddAfter(Ints.Find(5), 2);

        foreach (var item in Ints)
        {
            Debug.Log(item);//存储
        }

       
        //查找和删除
        LinkedListNode<int> node = Ints.Find(5);//查找节点
        Ints.Remove(node);//删除节点

        foreach (var item in Ints)
        {
            Debug.Log(item);
        }

        //正向遍历
        Debug.Log("正向遍历");
        node = Ints.First;
        while (node!=null)
        {
            Debug.Log(node.Value);
            node = node.Next;
        }

        //反向遍历
        Debug.Log("反向遍历");  
        node = Ints.Last;
        while (node != null)
        {
            Debug.Log(node.Value);
            node = node.Previous;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
