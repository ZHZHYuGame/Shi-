using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class LinkMgr : MonoBehaviour
{
    LinkedList<int> links = new LinkedList<int>();

    void Start()
    {
        links.AddFirst(1);
        links.AddLast(5);
        links.AddAfter(links.Last, 4);
        foreach (int item in links)
        {
            print(item);
        }
        LinkedListNode<int> node =new LinkedListNode<int>(5);
        links.Remove(node.Data);
        foreach (int item in links)
        {
            print(item);
        }
    }
}
