using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    void Start()
    {
        LinkList<int> datas = new LinkList<int>();

        datas.AddFirst(10);
        datas.AddLast(20);
        datas.AddLast(30);
        datas.AddLast(40);
        datas.AddLast(50);

        Debug.Log(datas.Count);
        Debug.Log(datas.Contains(30));

        datas.RemoveFirst();
        Debug.Log(datas.Contains(10));

        datas.Clear();
        Debug.Log(datas.Count);
    }
}
