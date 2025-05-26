using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LieKedListTast : MonoBehaviour
{
    public LinkedList<int> lie = new LinkedList<int>();
    // Start is called before the first frame update
    void Start()
    {
        lie.AddFirst(10);//在头添加数据
        lie.AddLast(20);//在尾添加数据
        
        lie.AddAfter(lie.First, 5);//在指定节点插入
        foreach (var item in lie)
        {
            Debug.Log(item);
        }
        //查找
        LinkedListNode<int> node = lie.Find(10);
        Debug.Log(node.Value);
        LinkedListNode<int> linked = lie.FindLast(10);
        Debug.Log(linked.Value);
        //删除
        lie.Remove(node);
        foreach (var item in lie)
        {
            Debug.Log(item);
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
