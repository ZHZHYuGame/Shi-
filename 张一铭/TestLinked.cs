using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//����ڵ���  �洢���ݺ�ָ����һ���ڵ������
public class Node<T>
{
    public T Value { get; set; }   //�ڵ�洢��ֵ
    public Node<T> Next { get; set; }  //ָ����һ���ڵ������(��������)

    public Node(T value)
    {
        Value = value;
        Next = null;//�½ڵ�Ĭ��ָ��null
    }

}
//�Զ��嵥��������:����ڵ㼯��
public class CustomLinkedList<T>
{
    private Node<T> head;  //����ͷ�ڵ�(��һ���ڵ�)
    private Node<T> tail;  //����β�ڵ�(���һ���ڵ�)
    private int count;     //����ڵ�����

    public int Count=>count;    //��ȡ������(ֻ������)
    public bool IsEmpty=>count==0;  //�ж������Ƿ�Ϊ��

    //������β������½ڵ�
    public void Add(T value)
    {
        Node<T> newNode=new Node<T>(value);

        //�������Ϊ��,�½ڵ����ͷ�ڵ�Ҳ��β�ڵ�
        if(tail==null)
        {
            head=tail=newNode;
        }
        else
        {
            //����ǰβ�ڵ�� Next ָ���½ڵ�
            tail.Next=newNode;
            //����β�ڵ�Ϊ�½ڵ�
            tail = newNode;
        }
        count++;// �����ȼ�1
    }

    //���������Ƴ���һ��ƥ��ֵ�Ľڵ�
    public bool Remove(T value)
    {
        Node<T> previous = null;//��¼��ǰ�ڵ��ǰһ���ڵ�
        Node<T> current = head;//��ͷ����ʼ����

        while(current!=null)
        {
            //�ҵ�ƥ��ֵ�Ľڵ�
            if(current.Value.Equals(value))
            {
                //���ɾ������ͷ�ڵ�
                if(previous==null)
                {
                    head = current.Next;
                    //�������ֻ��һ���ڵ� ɾ����tailҲӦΪnull
                    if (head == null) tail = null;
                }
                else
                {
                    //��ǰһ���ڵ��Nextָ��ǰ�ڵ����һ���ڵ�
                    previous.Next=current.Next;
                    //���ɾ������β�ڵ㣬����tail
                    if (current.Next == null) tail = previous;
                }
                count--;//�����ȼ�һ
                return true;
            }
            //�ƶ�����һ���ڵ�
            previous = current;
            current=current.Next;
        }
        return false;//δ�ҵ�ƥ��ֵ�Ľڵ�
    }

    //��ӡ�����е����нڵ�ֵ
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
        //�����Զ�������(�洢����)
        CustomLinkedList<int> custom = new CustomLinkedList<int>();

        //���Ԫ��
        custom.Add(10);
        custom.Add(20);
        custom.Add(30);

        //��ӡ����
        Debug.Log("�Զ�����������:");
        custom.PrintAllNodes();

        //ɾ��Ԫ��
        custom.Remove(20);
        Debug.Log("ɾ��20�������:");
        custom.PrintAllNodes();

        //�����������
        Debug.Log("������:" + custom.Count);
        Debug.Log("��������Ƿ�Ϊ��:" + custom.IsEmpty);
    }

}
