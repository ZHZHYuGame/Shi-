using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Diagnostics;
using System.IO;
using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

public class AssetBundlePackageMgr : Editor
{
    [MenuItem("AssetBundlePack/一键打包")]
    public static void AssetBundlePackage()
    {
        RemoveAssetBundle();

        //S目录
        string sPath = Application.streamingAssetsPath;
        //P目录
        string pPath = Application.persistentDataPath;

        AssetBundleType();

        BuildPipeline.BuildAssetBundles(sPath, BuildAssetBundleOptions.ChunkBasedCompression, BuildTarget.StandaloneWindows64);

        Process.Start(sPath);
    }

    public static void AssetBundleType()
    {
        //找到Resources目录下的所有的资源文件
        string[] allFiles = Directory.GetFiles($"{Application.dataPath}/Resources", "*.*", SearchOption.AllDirectories);

        allFiles=allFiles.Where((o=>Path.GetExtension(o).ToLower()!=".meta")).ToArray();
        StringBuilder sb = new StringBuilder();

        foreach (var file in allFiles)
        {
            //把路径的格式统一 \变成/
            string fPath = file.Replace(@"\", "/");
            string fPath1 = fPath.Replace(Application.dataPath, "Assets");
            string fName = Path.GetFileNameWithoutExtension(file);
            string fExten = Path.GetExtension(file);
            AssetImporter ai = AssetImporter.GetAtPath(fPath1);
            if (fExten == ".mat")
            {
                ai.assetBundleName = "Material/" + fName;
            }
            else if (fExten == ".prefab")
            {
                ai.assetBundleName = "Prefab/" + fName;
            }
            ai.assetBundleVariant = "ud";

            //MD5码
            string md5 = GetMD5(file);

            
            sb.Append(fPath1 + "|" + md5+"\r\n");
        }
        SaveAssetBundleManifest(sb.ToString());
    }

    /// <summary>
    /// S目录资源只读
    /// S目录的资源可以存贮在Build后的游戏包中
    /// </summary>
    [MenuItem("AssetBundlePack/streamingAssetsPath")]
    public static void AssetBundleSpath()
    {
        if (!Directory.Exists(Application.streamingAssetsPath))
        {
            Directory.CreateDirectory(Application.streamingAssetsPath);
        }
        Process.Start(Application.streamingAssetsPath);
    }
    /// <summary>
    /// P目录可读可写
    /// P目录放所有的游戏资源
    /// P目录放S目录存贮的游戏资源（一小部分）
    /// </summary>
    [MenuItem("AssetBundlePack/persistentDataPath")]
    public static void AssetBundlePpath()
    {
        Process.Start(Application.persistentDataPath);
    }

    public static void RemoveAssetBundle()
    {
        string sPath = Application.streamingAssetsPath;

        string[] allFiles = Directory.GetFiles(sPath, "*.*", SearchOption.AllDirectories);

        foreach (var f in allFiles)
        {
            File.Delete(f);   
        }
    }
    /// <summary>
    /// 转化一个资源的MD5码
    /// </summary>
    /// <param name="path"></param>
    /// <returns></returns>
    public static string GetMD5(string path)
    {
        byte[] data = File.ReadAllBytes(path);
        MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
        byte[] mData = md5.ComputeHash(data);
        return BitConverter.ToString(mData).Replace("-", "");
    }
    static void SaveAssetBundleManifest(string desc)
    {
        File.WriteAllText(Application.streamingAssetsPath + "/ABManifest.txt", desc);
    }
}
