using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using UnityEditor;
using UnityEngine;

public class AssetBundlePackageMgr : Editor
{
    [MenuItem("AssetBundlePackage/一键打包")]
    public static void AssetBundlePackage()
    {
        //S目录
        string sPath = Application.streamingAssetsPath;
        //P目录
        string pPath = Application.persistentDataPath;
        AssetBundleType();
        BuildPipeline.BuildAssetBundles("", BuildAssetBundleOptions.ChunkBasedCompression, BuildTarget.StandaloneWindows64);
        Process.Start(sPath);
    }

    private static void AssetBundleType()
    {
        //找到Resources目录下所有的资源文件
        string[] allFiles = Directory.GetFiles($"{Application.dataPath}/Resources", "*.*", SearchOption.AllDirectories);
        foreach (var file in allFiles)
        {
            string fPath= file.Replace(@"\", "/");
            string fPath1 = fPath.Replace(Application.dataPath, "Assets");
            AssetImporter ai= AssetImporter.GetAtPath(fPath);
        }
    }

    /// <summary>
    /// S目录资源只读
    /// S目录的资源可以存储在Build后的游戏包中
    /// S目录
    /// </summary>
    [MenuItem("AssetBundlePack/streamingAssetsPath")]
    public static void AssetBundleSPath()
    {
        if(!Directory.Exists(Application.streamingAssetsPath))
        {
            Directory.CreateDirectory(Application.streamingAssetsPath);
        }
        Process.Start(Application.streamingAssetsPath);
    }
    /// <summary>
    /// P目录可读可写
    /// P目录放所有的游戏资源
    /// P目录放S目录存储的游戏资源（一小部分）
    /// </summary>
    [MenuItem("AssetBundlePack/persistentDataPath")]
    public static void AssetBundlePPath()
    {
        Process.Start(Application.persistentDataPath);
    }
}
