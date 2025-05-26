using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main:MonoBehaviour
{
    //��׼���˫������(LinkedList<T>)
    LinkedList<int> Ints = new LinkedList<int>();
    // Start is called before the first frame update
    void Start()
    {

        Ints.AddFirst(1);//ͷ������
        Ints.AddLast(5);//β������
        Ints.AddLast(9);//β������
        Ints.AddAfter(Ints.Last, 3);//��ָ���ڵ����
        Ints.AddAfter(Ints.Find(5), 2);

        foreach (var item in Ints)
        {
            Debug.Log(item);//�洢
        }

       
        //���Һ�ɾ��
        LinkedListNode<int> node = Ints.Find(5);//���ҽڵ�
        Ints.Remove(node);//ɾ���ڵ�

        foreach (var item in Ints)
        {
            Debug.Log(item);
        }

        //�������
        Debug.Log("�������");
        node = Ints.First;
        while (node!=null)
        {
            Debug.Log(node.Value);
            node = node.Next;
        }

        //�������
        Debug.Log("�������");  
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
