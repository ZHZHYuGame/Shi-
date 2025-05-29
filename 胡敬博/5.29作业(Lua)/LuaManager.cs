using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using XLua;
public class LuaManager : MonoBehaviour
{
    /// <summary>
    /// �����������cshap��lua�������ӣ�
    /// </summary>
    LuaEnv luaEnv;

    /// <summary>
    /// luaģ���������ں���
    /// </summary>
    Action luaStart,luaUpdate;
    // Start is called before the first frame update
    void Start()
    {
        //��ʼ�������
        luaEnv = new LuaEnv();
        //��ȡlua�ļ�
        luaEnv.AddLoader(CustomsLoaderHandle);
        //���б���
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
