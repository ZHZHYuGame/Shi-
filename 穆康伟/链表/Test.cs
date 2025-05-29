using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        LinkList<int> ints = new LinkList<int>();
        ints.Add(10);
        ints.Add(20);
        ints.Add(30);
        ints.Add(300);
        // Debug.Log(ints.GetAt(2));
        ints.RemoveAt(3);
        //ints.Insert(4, 11);
        ints.PrintAll();
    }
}
