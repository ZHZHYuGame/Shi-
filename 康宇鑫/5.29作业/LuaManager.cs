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
    /// Lua�����
    /// </summary>
    LuaEnv luaEnv;
    /// <summary>
    /// ��ʼ����
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
        //���ر���Lua�ļ���ÿ�ζ�Lua�ű���ʱ������
        luaEnv.AddLoader(CustomLoaderHandle);
        //����Luaס�ļ�����
        luaEnv.DoString("require'LuaMain'");
        //Lua��C#�ؼ�����ͬ����
        LuaStart=luaEnv.Global.GetInPath<Action>("LuaStart");
        LuaUptade = luaEnv.Global.GetInPath<Action>("LuaUptade");
        //ִ�д����󶨵ĺ���
        LuaStart?.Invoke();
       
    }
    /// <summary>
    /// ���ر���Lua�ļ���ÿ�ζ�Lua�ű���ʱ������
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