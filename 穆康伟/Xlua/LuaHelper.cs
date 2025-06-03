using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using XLua;
[LuaCallCSharp]
public class LuaHelper : MonoBehaviour
{


    private static LuaHelper _instance;

    public static LuaHelper Instance
    {

        get
        {

            if (_instance == null)
            {
                // 在场景中查找已存在的实例
                _instance = FindObjectOfType<LuaHelper>();
                if (_instance == null)
                {
                    // 创建新的 GameObject 并挂载脚本
                    GameObject obj = new GameObject("LuaHelper");
                    _instance = obj.AddComponent<LuaHelper>();
                    DontDestroyOnLoad(obj); // 可选：防止切换场景时销毁
                }
            }
            return _instance;
        }
    }
    void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        }
        else
        {
            Destroy(gameObject); // 避免重复实例化
        }
    }
    private GameObject image;
    public void OpenUI(xluaCustomExport.OnCreat OnCreat = null)
    {
        image = GameObject.Instantiate(Resources.Load<GameObject>("UIRootView"), GameObject.FindAnyObjectByType<Canvas>().transform);
        if (OnCreat != null)
        {
            image.GetOrAddComponent<LuaViewBehaviour>();
            OnCreat(image);
        }
    }
}
