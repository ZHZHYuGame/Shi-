using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XLua;

[LuaCallCSharp]
public class MoveTrigger
{
    public static float BackX()
    {
        float h = Input.GetAxis("Horizontal");
        return h;
    }
    public static float BackY()
    {
        float v = Input.GetAxis("Vertical");
        return v;
    }
}
