using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Unity.VisualScripting;
using UnityEngine;
using XLua;
public class LuaManager : MonoBehaviour
{
    //XX xx;
    /// <summary>
    /// lua虚拟机
    /// </summary>
    LuaEnv luaEnv;

    Action LuaStart;
    Action LuaUpdata;
    // Start is called before the first frame update
    void Start()
    {
        //xx.Init();
        luaEnv = new LuaEnv();
        //加载编译Lua文件 每次读取Lua脚本的时候
        luaEnv.AddLoader(CustomLoaderHandle);
        //启动Lua主文件编译
        luaEnv.DoString("require 'LuaMain'");
        //Lua与C#关键函数同步绑定
        LuaStart = luaEnv.Global.GetInPath<Action>("LuaStart");
        LuaUpdata = luaEnv.Global.GetInPath<Action>("LuaUpdata");
        //执行处罚绑定的函数
        LuaStart?.Invoke();

    }
    /// <summary>
    /// 加载编译Lua文件 每次读取Lua脚本的时候 走这
    /// </summary>
    /// <param name="filepath"></param>
    /// <returns></returns>
    private byte[] CustomLoaderHandle(ref string filepath)
    {
        return File.ReadAllBytes($"{Application.dataPath}/Lua/Scripts/LuaCode/{filepath}.lua");
    }

    // Update is called once per frame
    void Update()
    {
        LuaUpdata?.Invoke();
    }


}
[LuaCallCSharp]

public class TestLua
{
    public static void TestFunCshap()
    {
        //Debug.Log("11111111111111111");
    }
    public void TestFun1()
    {
        //Debug.Log("2222222222222222");
    }
    //白名单：
    //开发过程中在参数上有一些类型变化 在这 添加识别
    //黑名单:排除去的参数
    public void TestFun(Action<string, string> act)
    {
        act?.Invoke("asaaa", "bbbbb");
    }
}

