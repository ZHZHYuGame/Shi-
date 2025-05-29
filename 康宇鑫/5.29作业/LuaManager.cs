using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Tutorial;
using UnityEngine;
using XLua;
public class LuaManager : MonoBehaviour
{
    /// <summary>
    /// Lua虚拟机
    /// </summary>
    LuaEnv luaEnv;
    /// <summary>
    /// 开始函数
    /// </summary>
    Action LuaStart;
    Action LuaUptade;
    public LuaManager()
    {

    }
    // Start is called before the first frame update
    void Start()
    {
        luaEnv = new LuaEnv();
        //加载编译Lua文件，每次读Lua脚本的时候，走这
        luaEnv.AddLoader(CustomLoaderHandle);
        //启动Lua住文件编译
        luaEnv.DoString("require'LuaMain'");
        //Lua与C#关键函数同步绑定
        LuaStart=luaEnv.Global.GetInPath<Action>("LuaStart");
        LuaUptade = luaEnv.Global.GetInPath<Action>("LuaUptade");
        //执行触发绑定的函数
        LuaStart?.Invoke();
       
    }
    /// <summary>
    /// 加载编译Lua文件，每次读Lua脚本的时候，走这
    /// </summary>
    /// <param name="filepath"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    private byte[] CustomLoaderHandle(ref string filepath)
    {
        return File.ReadAllBytes($"{Application.dataPath}/Script/LuaCode/{filepath}.lua");
    }

    // Update is called once per frame
    void Update()
    {
        LuaUptade?.Invoke();
    }
}
[LuaCallCSharp]
public class TestLua
{
    public static void TestFunCShap()
    {
        Debug.Log(111111111);
    }
    public void TestFunCShap1()
    {
        Debug.Log(111111111);
    }
}