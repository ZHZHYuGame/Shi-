using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using XLua;
public class LuaManager : MonoBehaviour
{
    /// <summary>
    /// 虚拟机（用于cshap与lua进行连接）
    /// </summary>
    LuaEnv luaEnv;

    /// <summary>
    /// lua模拟生命周期函数
    /// </summary>
    Action luaStart,luaUpdate;
    // Start is called before the first frame update
    void Start()
    {
        //初始化虚拟机
        luaEnv = new LuaEnv();
        //读取lua文件
        luaEnv.AddLoader(CustomsLoaderHandle);
        //进行编译
        luaEnv.DoString("require 'LuaTest'");

        luaStart = luaEnv.Global.GetInPath<Action>("Start");
        luaUpdate = luaEnv.Global.GetInPath<Action>("Update");

        luaStart?.Invoke();
    }

    private byte[] CustomsLoaderHandle(ref string filepath)
    {
        return File.ReadAllBytes($"{Application.dataPath}/Scripts/Lua/{filepath}.lua");
    }

    // Update is called once per frame
    void Update()
    {
        luaUpdate?.Invoke();
    }
    
}

[LuaCallCSharp]
public class LuaTest
{
    public static void LuaStaticFunction()
    {
        Debug.Log("LuaStaticFunction");
    }

    public void LuaFuction()
    {
        Debug.Log("luaFuction");
    }
}
