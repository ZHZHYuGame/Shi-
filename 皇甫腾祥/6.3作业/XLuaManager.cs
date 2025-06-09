using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//引入虚拟机
using XLua;
using System.IO;
using System;
public class XLuaManager : MonoBehaviour
{
    /// <summary>
    /// 定义虚拟机对象
    /// </summary>
    private LuaEnv luaEnv;
    private Action luaStart;
    private Action luaUpdata;
    private void Awake()
    {
        //实例化虚拟机
        luaEnv = new LuaEnv();
        //添加自定义加载器
        luaEnv.AddLoader(CustomLoader);
        //加载Lua里面的第一个模块
        luaEnv.DoString("require 'Main'");
        luaStart = luaEnv.Global.Get<Action>("LuaStart");
        luaUpdata = luaEnv.Global.Get<Action>("LuaUpdate");
    }

    private byte[] CustomLoader(ref string filepath)
    {
        return File.ReadAllBytes(Application.dataPath+"/Lua/"+filepath+".lua");
    }

    // Start is called before the first frame update
    void Start()
    {
        luaStart?.Invoke();
    }

    // Update is called once per frame
    void Update()
    {
        luaUpdata?.Invoke();
    }
}
[LuaCallCSharp]
public static class RaycastHelper
{
    // 从屏幕点发射射线
    public static bool ScreenPointRaycast(Camera cam, Vector2 screenPos, out RaycastHit hit)
    {
        Ray ray = cam.ScreenPointToRay(screenPos);
        return Physics.Raycast(ray, out hit);
    }
}

