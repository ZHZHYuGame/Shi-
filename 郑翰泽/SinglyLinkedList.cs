using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace YoyoCode
{
    internal class SinglyLinkedList<T>
    {
        //��Ԫ���
        public SinglyLinkedListNode<T> First;
        //β���
        public SinglyLinkedListNode<T> Last;
        //�������
        public int NodeCount = 0;

        /// <summary>
        /// �Ƿ�Ϊ��
        /// </summary>
        /// <returns>bool�������Ƿ�Ϊ��</returns>
        public bool IsEmpty()
        {
            return NodeCount == 0 ? true : false;
        }

        /// <summary>
        /// �������
        /// </summary>
        public void Clear()
        {
            First = null;
            Last = null;
            NodeCount = 0;
        }

        /// <summary>
        /// ͷ�巨
        /// </summary>
        /// <param name="value">����ΪT��ֵ</param>
        public void AddFirst(T value)
        {
            SinglyLinkedListNode<T> node = new SinglyLinkedListNode<T>(value);
            if (IsEmpty())
            {
                First = node;
                Last = node;
            }
            else
            {
                node.Next = First;
                First = node;
            }
            NodeCount++;
        }

        /// <summary>
        /// β�巨
        /// </summary>
        /// <param name="value">����ΪT��ֵ</param>
        public void AddLast(T value)
        {
            SinglyLinkedListNode<T> node = new SinglyLinkedListNode<T>(value);
            if (IsEmpty())
            {
                First = node;
                Last = node;
            }
            else
            {
                Last.Next = node;
                Last = node;
            }
            NodeCount++;
        }
        /// <summary>
        /// �ڶ�Ӧ����������Ԫ��
        /// </summary>
        /// <param name="idx">��������0��ʼ</param>
        public bool AddAt(T value, int idx)
        {
            if (idx < 0 || idx > NodeCount)
            {
                return false;
            }
            if (idx == 0)
            {
                AddFirst(value);
                return true;
            }
            if (idx == NodeCount)
            {
                AddLast(value);
                return true;
            }
            //�������
            SinglyLinkedListNode<T> prev = First;
            SinglyLinkedListNode<T> cur = First.Next;
            SinglyLinkedListNode<T> node = new SinglyLinkedListNode<T>(value);
            for (int i = 1; i < idx; i++)
            {
                prev = prev.Next;
                cur = cur.Next;
            }
            prev.Next = node;
            node.Next = cur;
            NodeCount++;
            return true;
        }
        /// <summary>
        /// ɾ����һ��Ԫ��
        /// </summary>
        /// <returns>bool���Ƿ�ɾ���ɹ�</returns>
        public bool RemoveFirst()
        {
            if (IsEmpty())
            {
                return false;
            }
            //����ֻ��һ��Ԫ��
            if (NodeCount > 1)
            {
                First = First.Next;
            }
            else
            {
                First = null;
                Last = null;
            }
            NodeCount--;
            return true;
        }

        /// <summary>
        /// ɾ����Ӧ������Ԫ��
        /// </summary>
        /// <returns>bool���Ƿ�ɾ���ɹ�</returns>
        public bool RemoveAt(int idx)
        {
            if (IsEmpty())
            {
                return false;
            }
            if (idx == 0)
            {
                return RemoveFirst();
            }

            SinglyLinkedListNode<T> prev = First;
            SinglyLinkedListNode<T> cur = First.Next;
            for (int i = 1; i < idx; i++)
            {
                prev = prev.Next;
                cur = cur.Next;
            }
            if (idx == NodeCount - 1)
            {
                Last = prev;
                prev.Next = null;
            }
            else
            {
                prev.Next = cur.Next;
            }
            NodeCount--;
            return true;
        }
        /// <summary>
        /// ɾ�����һ��Ԫ��
        /// </summary>
        /// <returns>bool���Ƿ�ɾ���ɹ�</returns>
        public bool RemoveLast()
        {
            return RemoveAt(NodeCount - 1);
        }
    }

    /// <summary>
    /// ���������еĽ�����ݽṹ
    /// </summary>
    internal class SinglyLinkedListNode<T>
    {
        public T data;
        public SinglyLinkedListNode<T> Next;

        public SinglyLinkedListNode(T data)
        {
            this.data = data;
        }
    }
}
