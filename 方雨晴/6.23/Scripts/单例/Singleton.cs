using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// µ¥Àý
/// </summary>
public class Singleton <T> where T :class ,new()
{
    private static T Value;
    public static T ins
    {
        get
        {
            if(Value==null)
            {
                Value = new T();
            }
            return Value;
        }
    }
}
