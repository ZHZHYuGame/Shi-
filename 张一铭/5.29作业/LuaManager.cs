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
        TestCall();
        //执行触发绑定的函数
        LuaStart?.Invoke();

    }

    private byte[] CustomLoaderHandle(ref string filepath)
    {
        return File.ReadAllBytes($"{Application.dataPath}/Scripts/LuaCode/{filepath}.lua");
    }

    // Update is called once per frame
    void Update()
    {
        LuaUpdate?.Invoke();
    }
    public void TestCall()
    {
        //var addFunc = luaEnv.Global.Get<Func<float,float,>>("AddNum");
        //float num = addFunc(3, 4);
        //Debug.Log("3 + 4 = " + num);

        var sayHello = luaEnv.Global.Get<Func<string,string>>("SayHello");
        string greeting = sayHello("world");
        Debug.Log(greeting);
    }
}
[LuaCallCSharp]
public class TestLua
{
    public static void TestFunCShap()
    {
        Debug.Log("测试");
    }

    public void TestFun1()
    {
        Debug.Log("2222222222");
    }
}

