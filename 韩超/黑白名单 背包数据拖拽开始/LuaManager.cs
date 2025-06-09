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
    /// lua�����
    /// </summary>
    LuaEnv luaEnv;

    Action LuaStart;
    Action LuaUpdata;
    // Start is called before the first frame update
    void Start()
    {
        //xx.Init();
        luaEnv = new LuaEnv();
        //���ر���Lua�ļ� ÿ�ζ�ȡLua�ű���ʱ��
        luaEnv.AddLoader(CustomLoaderHandle);
        //����Lua���ļ�����
        luaEnv.DoString("require 'LuaMain'");
        //Lua��C#�ؼ�����ͬ����
        LuaStart = luaEnv.Global.GetInPath<Action>("LuaStart");
        LuaUpdata = luaEnv.Global.GetInPath<Action>("LuaUpdata");
        //ִ�д����󶨵ĺ���
        LuaStart?.Invoke();

    }
    /// <summary>
    /// ���ر���Lua�ļ� ÿ�ζ�ȡLua�ű���ʱ�� ����
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
    //��������
    //�����������ڲ�������һЩ���ͱ仯 ���� ���ʶ��
    //������:�ų�ȥ�Ĳ���
    public void TestFun(Action<string, string> act)
    {
        act?.Invoke("asaaa", "bbbbb");
    }
}

