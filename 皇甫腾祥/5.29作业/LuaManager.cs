using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using XLua;

public class LuaManager : MonoBehaviour
{
    LuaEnv luaEnv;


    Action LuaStart;

    Action LuaUpdate;

    public LuaManager()
    {

    }
    // Start is called before the first frame update
    void Start()
    {
        luaEnv = new LuaEnv();
        luaEnv.AddLoader(CustomLoaderHandle);
        luaEnv.DoString(" require 'LuaMain' ");
        LuaStart = luaEnv.Global.GetInPath<Action>("LuaStart");
        LuaUpdate = luaEnv.Global.GetInPath<Action>("LuaUpdate");
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
}
[LuaCallCSharp]
public class TestLua
{
    public static void TestFunCShap()
    {
        Debug.Log("11111111111111111111");
    }

    public void TestFun1()
    {
        Debug.Log("2222222222");
    }
}
