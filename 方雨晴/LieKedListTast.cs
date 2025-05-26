using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LieKedListTast : MonoBehaviour
{
    public LinkedList<int> lie = new LinkedList<int>();
    // Start is called before the first frame update
    void Start()
    {
        lie.AddFirst(10);//��ͷ�������
        lie.AddLast(20);//��β�������
        
        lie.AddAfter(lie.First, 5);//��ָ���ڵ����
        foreach (var item in lie)
        {
            Debug.Log(item);
        }
        //����
        LinkedListNode<int> node = lie.Find(10);
        Debug.Log(node.Value);
        LinkedListNode<int> linked = lie.FindLast(10);
        Debug.Log(linked.Value);
        //ɾ��
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
