using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using XLua;

public class LuaManager : MonoBehaviour
{
    /// <summary>
    /// Lua虚拟机
    /// </summary>
    LuaEnv luaEnv;
    //开始函数
    Action LuaStart;

    Action LuaUpdate;

    public LuaManager()
    {
    }

    // Start is called before the first frame update
    void Start()
    {

        luaEnv = new LuaEnv();
        //加载编译Lua文件，每次读取Lua脚本的时候，走这
        luaEnv.AddLoader(CustomLoaderHandle);
        //启动Lua主文件编译
        luaEnv.DoString("require 'LuaMain'");
        //Lua与C#关键函数同步绑定
        LuaStart = luaEnv.Global.GetInPath<Action>("LuaStart");
        LuaUpdate = luaEnv.Global.GetInPath<Action>("LuaUpdate");
        //执行触发绑定的函数
        LuaStart?.Invoke();
    }
    
    /// <summary>
    /// 加载编译Lua文件，每次读取Lua脚本的时候，走这
    /// </summary>
    /// <param name="filepath"></param>
    /// <returns></returns>
    private byte[] CustomLoaderHandle(ref string filepath)
    {
        return File.ReadAllBytes($"{Application.dataPath}/Scripts/LuaCode/{filepath}.lua");
    }
    
    // Update is called once per frame
    void Update()
    {
        LuaUpdate?.Invoke();
    }
}
