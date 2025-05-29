using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node<T>
{
    private T data;
    private Node<T> next;

    public T Data { get => data; set => data = value; }
    public Node<T> Next { get => next; set => next = value; }

    public Node()
    {
        Data = default;
        Next = null;
    }
    public Node(T value)
    {
        Data = value;
        Next = null;
    }
    public Node(T value, Node<T> next)
    {
        this.Data = value;
        this.Next = next;
    }
    public Node(Node<T> next)
    {
        this.Next = next;
    }
}
