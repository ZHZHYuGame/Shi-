using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using XLua;



public class LuaManager : MonoBehaviour
{
    /// <summary>
    /// Lua�����
    /// </summary>
    LuaEnv luaEnv;
    //��ʼ����
    Action LuaStart;

    Action LuaUpdate;

    public LuaManager()
    {

    }

    // Start is called before the first frame update
    void Start()
    {
        luaEnv = new LuaEnv();
        //���ر���Lua�ļ���ÿ�ζ�ȡLua�ű���ʱ������
        luaEnv.AddLoader(CustomLoaderHandle);
        //����Lua���ļ�����
        luaEnv.DoString("require 'LuaMain'");
        //Lua��C#�ؼ�����ͬ����
        LuaStart = luaEnv.Global.GetInPath<Action>("LuaStart");
        LuaUpdate = luaEnv.Global.GetInPath<Action>("LuaUpdate");
        TestCall();
        //ִ�д����󶨵ĺ���
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
        Debug.Log("����");
    }

    public void TestFun1()
    {
        Debug.Log("2222222222");
    }
}

