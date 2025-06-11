using BehaviorDesigner.Runtime.Tasks.Unity.UnityNavMeshAgent;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

public class CreateAssetBundle : Editor
{
    [MenuItem("AB包工具/BuildAB(Windows)")]
    public static void BuildAseetBuildWindow()
    {
        string outPath = Application.dataPath + "/ArtRes/AB/PC";

        BuildPipeline.BuildAssetBundles(outPath, BuildAssetBundleOptions.ChunkBasedCompression, BuildTarget.StandaloneWindows64);

        //AssetImporter importer = 

        DirectoryInfo direcotryInfo = Directory.CreateDirectory(outPath);

        FileInfo[] files = direcotryInfo.GetFiles();

        foreach (FileInfo file in files)
        {
            Debug.Log(file.Name);
        }
    }
    [MenuItem("AB包工具/BuildAB(Android)")]
    public static void BuildAseetBuildAndroid()
    {
        string outPath = Application.dataPath + "/ArtRes/AB/Android";

        BuildPipeline.BuildAssetBundles(outPath, BuildAssetBundleOptions.ChunkBasedCompression, BuildTarget.Android);

        DirectoryInfo direcotryInfo = Directory.CreateDirectory(outPath);

        FileInfo[] files = direcotryInfo.GetFiles();

        foreach (FileInfo file in files)
        {
            Debug.Log(file.Name);
        }
    }

    [MenuItem("AB包工具/根据对象类型分类打包")]

    public static void BuildAssetAB()
    {
        string targetPath = Application.dataPath + "/ArtRes/Res";

        if(!File.Exists(targetPath))
        {
            Directory.CreateDirectory(targetPath);
        }
        
        DirectoryInfo directoryInfo = Directory.GetParent(targetPath);

        FileInfo[] files = directoryInfo.GetFiles();

        foreach (var asset in files)
        {
            AssetImporter importer = AssetImporter.GetAtPath(asset.FullName);

            string bundleName = GetAssetBundleNameByType(asset.FullName, importer);

            if(!string.IsNullOrEmpty(bundleName))
            {
                importer.assetBundleName = bundleName;
            }
        }

        string outPath = Application.dataPath + "/ArtRes/AB/PC";
        BuildPipeline.BuildAssetBundles(outPath, BuildAssetBundleOptions.ChunkBasedCompression, BuildTarget.Android);
        Debug.Log("生成成功");
    }

    /// <summary>
    /// 根据文件后缀判断ab包名称
    /// </summary>
    /// <param name="path"></param>
    /// <param name="importer"></param>
    /// <returns></returns>
    private static string GetAssetBundleNameByType(string path,AssetImporter importer)
    {
        string extension = Path.GetExtension(path).ToLower();

        if(extension == ".jpg" || extension == ".png" || extension == ".webp")
        {
            return "Texture";
        }else if(extension == ".prefab")
        {
            return "Prefab";
        }else if(extension ==".mat")
        {
            return "Material";
        }
        return null;
    }
}
