using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;
using XLua;
/// <summary>
/// Lua环境管理器
/// </summary> <summary>
/// 
/// </summary>
public class LuaMgr : MonoBehaviour
{
    // Start is called before the first frame update
    /// <summary>
    /// 全局的xlua引擎
    /// </summary> <summary>
    public static LuaMgr instance;
    public static LuaEnv luaEnv;
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

        //1.实例化xLua引擎
        luaEnv = new LuaEnv();

        //2.设置xlua的脚本路径
        string scriptPath = Application.dataPath + "/DownLoad/xLuaLogic/";

        // 确保路径使用正确的分隔符
        scriptPath = scriptPath.Replace('\\', '/');
        // 获取目录部分
        string directory = System.IO.Path.GetDirectoryName(scriptPath).Replace('\\', '/');
        UnityEngine.Debug.Log(directory);
        // 获取文件名（不带扩展名）
        string fileName = System.IO.Path.GetFileNameWithoutExtension(scriptPath);
        UnityEngine.Debug.Log($"package.path = package.path .. ';{directory}/?.lua'");
        // 设置 Lua 的包路径（如果需要）
        luaEnv.DoString($"package.path = package.path .. ';{directory}/?.lua'");

    }
    /// <summary>
    /// 执行xlua脚本
    /// </summary>
    /// <param name="str"></param> <summary>
    /// 
    /// </summary>
    /// <param name="str"></param>
    public void DoString(string str)
    {
        luaEnv.DoString(str);
        // Button btn = GetComponent<UnityEngine.UI.Button>();
        // btn.onClick.AddListener(null);
        //transform.FindChild
        //transform
    }
}
