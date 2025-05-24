using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LinkList<T>
{
    private Node<T> head;//����ͷ
    private int count;//������
    //���ڻ�ȡ������
    public int Count
    {
        get
        {
            return count;
        }
    }
    //�ж������Ƿ�Ϊ��
    public bool IsEmpty
    {
        get
        {
            return count == 0;
        }
    }

    //������ͷ����ӽڵ�
    #region 
    //AddFirst ���������������ͷ�����һ���½ڵ㣺
    //LinkedListNode<T> newNode = new LinkedListNode<T>(data); ����һ���µĽڵ���󣬴���Ҫ�洢������ data��
    //newNode.Next = head; ���½ڵ�� Next ����ָ��ԭ����ͷ�ڵ㣨���ԭ����ͷ�ڵ�Ļ�����
    //head = newNode; ���������ͷ�ڵ�Ϊ�´����Ľڵ㡣
    //count++; ���������нڵ��������
    #endregion
    public void AddFirst(T data)
    {
        Node<T> newNode = new Node<T>(data);
        newNode.next = head;
        head = newNode;
        count++;
    }
    //������β����ӽڵ�
    #region
    //AddLast ���������������β�����һ���½ڵ㣺
    //���ȼ�������Ƿ�Ϊ�գ����Ϊ������� AddFirst ������ӽڵ㲢���ء�
    //����һ���µĽڵ���� newNode��
    //ʹ��һ����ʱ���� current �������ͷ�ڵ㿪ʼ������ͨ�� while (current.Next != null) ѭ���ҵ���������һ���ڵ㡣
    //�����һ���ڵ�� Next ����ָ���´����Ľڵ㡣
    //���������нڵ��������
    #endregion
    public void AddLast(T data)
    {
        if (IsEmpty)
        {
            AddFirst(data);
            return;
        }
        Node<T> newNode = new Node<T>(data);
        Node<T> current = head;
        //�ҵ����һ���ڵ�
        while (current.next != null)
        {
            current = current.next;
        }
        current.next = newNode;
        count++;
    }
    #region
    //RemoveFirst �������ڴ������ͷ���Ƴ�һ���ڵ㣺
    //���ȼ�������Ƿ�Ϊ�գ����Ϊ���򷵻� false ��ʾ�Ƴ�����ʧ�ܡ�
    //��ͷ�ڵ�����ø���Ϊԭ��ͷ�ڵ����һ���ڵ㣬�� head = head.Next;��
    //���������нڵ��������
    //���� true ��ʾ�Ƴ������ɹ���
    #endregion
    //��ͷ���Ƴ�����
    public bool RemoveFirst()
    {
        if (IsEmpty)
        {
            return false;
        }
        head = head.next;
        count--;
        return true;
    }
    //�������в��ҽڵ�
    public bool Contains(T data)
    {
        Node<T> tempNode = head;
        while (tempNode != null)
        {
            if (tempNode.data.Equals(data))
            {
                return true;
            }
            tempNode = tempNode.next;
        }
        return false;
    }
    //�������
    public void Clear()
    {
        head = null;
        count = 0;
    }
}
