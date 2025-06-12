using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;

public class AssetsBundleMgr : Editor
{
    [MenuItem("AssetsBundlePack/一键打包")]
    public static void AsstsBundlePack()
    {
        //s目录
        string sPath=Application.streamingAssetsPath;

        //p目录
        string pPath = Application.persistentDataPath;

        AssetBundleType();
        //1 输出路径（streaming 目录） 2.压缩方式（基于块的压缩，支持部分加载） 3.目标平台（Window64位）
        BuildPipeline.BuildAssetBundles(sPath, BuildAssetBundleOptions.ChunkBasedCompression, BuildTarget.StandaloneWindows64);
        //构建完成后通过系统进程打开输出目录 （方便手动查看生成的AssetsBundle文件）
        Process.Start(sPath);
      

    }
    public static void AssetBundleType()
    {
        //找到Resources目录下的所有文件
        string[] allFile = Directory.GetFiles($"{Application.dataPath}/Resources", "*.*", SearchOption.AllDirectories);
        allFile = allFile.Where(o=>Path.GetExtension(o).ToLower()!=".meta").ToArray();
        foreach (string file in allFile)
        {
            //把路径的格式统一为/
            string fPath = file.Replace(@"\", "/");
            string fpath2 = fPath.Replace(Application.dataPath, "Assets");
            string fName=Path.GetFileNameWithoutExtension(file);
            string fExt=Path.GetExtension(file);    
            AssetImporter assetImporter=AssetImporter.GetAtPath(fpath2);
            if(fExt==".mat")
            {
                assetImporter.assetBundleName = "Material/" + fName;
            }
            else if (fExt == ".prefab")
            {
                assetImporter.assetBundleName = "Prefab/" + fName;
            }
            else if(fExt ==".png")
            {
                assetImporter.assetBundleName = "Texture/" + fName;
            }
        }
    }
    /// <summary>
    /// s目录
    /// </summary>
    [MenuItem("AssetsBundlePack/StreamingAssetsPath")]
    public static void AssetBundleSPath()
    {
        if(!Directory.Exists(Application.streamingAssetsPath))
        {
            Directory.CreateDirectory(Application.streamingAssetsPath);
        }
        Process.Start(Application.streamingAssetsPath);
    }
    /// <summary>
    /// P目录
    /// </summary>

    [MenuItem("AssetsBundlePack/persistentDataPath")]
    public static void AssetBundlePPath()
    {
        
        Process.Start(Application.persistentDataPath);
    }
}
