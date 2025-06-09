using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using JetBrains.Annotations;
using UnityEngine;
using XLua;

public class LuaManager : MonoBehaviour
{
    public static LuaManager instanace;
    void Awake()
    {
        instanace = this;
    }
    public LuaEnv luaEnv;
    private Action LuaStart;
    private Action LuaUpDate;

    void Start()
    {
        luaEnv = new LuaEnv();
        luaEnv.AddLoader(CustomLoaderHandle);
        luaEnv.DoString("require ('MainLua')");

        LuaStart = luaEnv.Global.GetInPath<Action>("Start");
        LuaUpDate = luaEnv.Global.GetInPath<Action>("UpDate");
        LuaStart?.Invoke();

    }
    void Update()
    {


        if (LuaUpDate != null)
        {
            LuaUpDate?.Invoke();
        }

    }


    private byte[] CustomLoaderHandle(ref string filepath)
    {
        return File.ReadAllBytes($"{Application.dataPath}/Scripts/LuaCode/{filepath}.lua");
    }
}
[LuaCallCSharp]
public class TestLua
{
    private static Action action;

    public static void CallCSharp(Ray ray)
    {
        Debug.Log("点击NPC");
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            if (hit.transform.tag == "NPC")
            {

                action = LuaManager.instanace.luaEnv.Global.GetInPath<Action>("OnClickNPC");
                action?.Invoke();
            }
        }

    }
}
