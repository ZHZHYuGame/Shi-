using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMgr : MonoBehaviour
{
    void Awake()
    {
        gameObject.AddComponent<LuaMgr>();
        var luaHelper = LuaHelper.Instance;
    }
    void Start()
    {
        LuaMgr.instance.DoString("require('Main')");
    }
}
