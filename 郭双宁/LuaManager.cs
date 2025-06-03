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
        //ִ�д����󶨵ĺ���
        LuaStart?.Invoke();
    }
    
    /// <summary>
    /// ���ر���Lua�ļ���ÿ�ζ�ȡLua�ű���ʱ������
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
