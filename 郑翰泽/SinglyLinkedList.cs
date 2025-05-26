using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace YoyoCode
{
    internal class SinglyLinkedList<T>
    {
        //首元结点
        public SinglyLinkedListNode<T> First;
        //尾结点
        public SinglyLinkedListNode<T> Last;
        //结点数量
        public int NodeCount = 0;

        /// <summary>
        /// 是否为空
        /// </summary>
        /// <returns>bool，链表是否为空</returns>
        public bool IsEmpty()
        {
            return NodeCount == 0 ? true : false;
        }

        /// <summary>
        /// 清空链表
        /// </summary>
        public void Clear()
        {
            First = null;
            Last = null;
            NodeCount = 0;
        }

        /// <summary>
        /// 头插法
        /// </summary>
        /// <param name="value">类型为T的值</param>
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
        /// 尾插法
        /// </summary>
        /// <param name="value">类型为T的值</param>
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
        /// 在对应的索引插入元素
        /// </summary>
        /// <param name="idx">索引，从0开始</param>
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
            //其他情况
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
        /// 删除第一个元素
        /// </summary>
        /// <returns>bool，是否删除成功</returns>
        public bool RemoveFirst()
        {
            if (IsEmpty())
            {
                return false;
            }
            //链表只有一个元素
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
        /// 删除对应索引的元素
        /// </summary>
        /// <returns>bool，是否删除成功</returns>
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
        /// 删除最后一个元素
        /// </summary>
        /// <returns>bool，是否删除成功</returns>
        public bool RemoveLast()
        {
            return RemoveAt(NodeCount - 1);
        }
    }

    /// <summary>
    /// 单向链表中的结点数据结构
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
