using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XLua;
public class PrefabCollision : MonoBehaviour
{
    private LuaTable luaTable;

    public void Init(LuaTable table)
    {
        luaTable = table;
        luaTable.Get("OnInit", out OnInitFunc);
        luaTable.Get("OnCollisionEnter", out OnCollisionEnterFunc);
        luaTable.Get("OnCollisionExit", out OnCollisionExitFunc);
        if (OnInitFunc != null) OnInitFunc.Call(luaTable, gameObject);
    }
    private LuaFunction OnInitFunc;
    private LuaFunction OnCollisionEnterFunc;
    private LuaFunction OnCollisionExitFunc;

    void OnCollisionEnter(Collision collision)
    {
        if (OnCollisionEnterFunc != null)
            OnCollisionEnterFunc.Call(luaTable, gameObject, collision.gameObject);
    }

    void OnCollisionExit(Collision collision)
    {
        if (OnCollisionExitFunc != null)
            OnCollisionExitFunc.Call(luaTable, gameObject, collision.gameObject);
    }

    void OnDestroy()
    {
        if (luaTable != null)
        {
            luaTable.Dispose();
            luaTable = null;
        }
    }
}
