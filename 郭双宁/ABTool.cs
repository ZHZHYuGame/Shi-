using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ABTool : Singleton<ABTool>
{
    private AssetBundle mainAB = null;
    private AssetBundleManifest ABMainFest = null;

    private Dictionary<string, AssetBundle> allABDic = new Dictionary<string, AssetBundle>();
    private string PathUrl
    {
        get
        {
            return Application.streamingAssetsPath + "/";
        }
    }
    private string ABMainName
    {
        get
        {
#if UNITY_IOS
return "IOS";
#elif unity_ANDROID
return "Android";
#else
            return "PC";
#endif
        }
    }

    //同步加载
    public void LoadRes(string abName, string resName)
    {
        //记载AB包
        if (mainAB == null)
        {
            mainAB = AssetBundle.LoadFromFile(PathUrl + ABMainName);
            ABMainFest = mainAB.LoadAsset<AssetBundleManifest>("AssetBundleManifest");
        }
        //获取依赖包信息
        string[] str = ABMainFest.GetAllDependencies(abName);
        for (int i = 0; i < str.Length; i++)
        {
            if (!allABDic.ContainsKey(str[i]))
            {
                AssetBundle ab = AssetBundle.LoadFromFile(PathUrl + str[i]);
                allABDic.Add(str[i], ab);
            }
        }

    }
}

