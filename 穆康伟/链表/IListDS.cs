using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IListDS<T>
{
    void Add(T data);
    void Insert(int index, T data);
    T GetAt(int index);
    void RemoveAt(int index);
    void RemoveAll();
    void PrintAll();
}
