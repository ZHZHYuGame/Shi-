using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XLua;
[LuaCallCSharp]
public class LuaViewBehaviour : MonoBehaviour
{
    [CSharpCallLua]
    public delegate void delLuaAwake(GameObject obj);
    LuaViewBehaviour.delLuaAwake luaAwake;
    public delegate void delLuaStart();
    LuaViewBehaviour.delLuaStart luaStart;
    public delegate void delLuaUpDate();
    LuaViewBehaviour.delLuaUpDate luaUpDate;
    public delegate void delLuaOnDestory();
    LuaViewBehaviour.delLuaOnDestory luaOnDestory;

    private LuaTable scriptEnv;
    private LuaEnv luaEnv;
    void Awake()
    {
        luaEnv = LuaMgr.luaEnv;
        scriptEnv = luaEnv.NewTable();

        LuaTable meta = luaEnv.NewTable();
        meta.Set("__index", luaEnv.Global);
        scriptEnv.SetMetaTable(meta);
        meta.Dispose();

        string prefabName = name;
        if (prefabName.Contains("(Clone)"))
        {
            prefabName = prefabName.Split(new string[] { "(Clone)" }, StringSplitOptions.RemoveEmptyEntries)[0];
        }
        luaAwake = scriptEnv.GetInPath<LuaViewBehaviour.delLuaAwake>(prefabName + ".Awake");
        luaStart = scriptEnv.GetInPath<LuaViewBehaviour.delLuaStart>(prefabName + ".Start");
        luaUpDate = scriptEnv.GetInPath<LuaViewBehaviour.delLuaUpDate>(prefabName + ".Update");
        luaOnDestory = scriptEnv.GetInPath<LuaViewBehaviour.delLuaOnDestory>(prefabName + ".OnDestroy");
        scriptEnv.Set("self", this);
        if (luaAwake != null)
        {
            luaAwake(gameObject);
        }

    }
    void Start()
    {
        if (luaStart != null)
        {
            luaStart();
        }
    }
    void Update()
    {
        if (luaUpDate != null)
        {
            luaUpDate();
        }
    }
    void OnDestroy()
    {
        if (luaOnDestory != null)
        {
            luaOnDestory();
        }
        luaOnDestory = null;
        luaStart = null;
        luaUpDate = null;
        luaAwake = null;
    }
}
