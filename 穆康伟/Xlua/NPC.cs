using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using XLua;
public class NPC : MonoBehaviour
{
    private Action action;
    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            action = LuaManager.instanace.luaEnv.Global.GetInPath<Action>("OnClickNPC");
            action?.Invoke();
        }
    }
}
