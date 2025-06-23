
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton<T> where T:new ()
{
   public static T instance;

    private static T instanceInstance
    {
        get
        {
            if(instance == null)
            {
                instance = new T();
            }
            return instance;
        }
    }
}
