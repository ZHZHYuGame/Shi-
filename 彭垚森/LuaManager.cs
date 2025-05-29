using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using XLua;

public class LuaManager : MonoBehaviour
{
    //Lua环境
    LuaEnv luaEnv;
    // 初始化方法
    Action LuaStart;
    //Updata方法
    Action LuaUpdata;
    // Start is called before the first frame update
    void Start()
    {
        luaEnv = new LuaEnv();
        // 自定义加载Lua文件，每次读取Lua脚本时调用
        luaEnv.AddLoader(CustomLoaderHandle);
        // 加载Lua主文件
        luaEnv.DoString("require 'LuaMain'");
        // Lua和C#关键方法同步绑定
        LuaStart = luaEnv.Global.GetInPath<Action>("LuaStart");
        LuaUpdata = luaEnv.Global.GetInPath<Action>("LuaUpdata");
        // 执行绑定的函数
        LuaStart?.Invoke();
    }
    /// <summary>
    /// 自定义加载Lua文件，每次读取Lua脚本时调用
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
        //绑定的Updata方法帧更新
        LuaUpdata?.Invoke();
    }
}
[LuaCallCSharp]
public class Test
{
    public static void TestFun1()
    {
        Debug.Log("添加一个Cube，并添加Player脚本");
        GameObject go = GameObject.Instantiate(Resources.Load<GameObject>("Cube"));
        go.GetComponent<MeshRenderer>().material.color = Color.green;
        go.name = "SS";
        go.AddComponent<Player>();
    }
    public static void TestFun2(int a, int b)
    {
        int c = a + b;
        Debug.Log(c);
    }
}
