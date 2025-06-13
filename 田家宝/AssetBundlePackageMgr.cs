using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using UnityEditor;
using UnityEngine;

public class AssetBundlePackageMgr : Editor
{
    [MenuItem("AssetBundlePackage/һ�����")]
    public static void AssetBundlePackage()
    {
        //SĿ¼
        string sPath = Application.streamingAssetsPath;
        //PĿ¼
        string pPath = Application.persistentDataPath;
        AssetBundleType();
        BuildPipeline.BuildAssetBundles("", BuildAssetBundleOptions.ChunkBasedCompression, BuildTarget.StandaloneWindows64);
        Process.Start(sPath);
    }

    private static void AssetBundleType()
    {
        //�ҵ�ResourcesĿ¼�����е���Դ�ļ�
        string[] allFiles = Directory.GetFiles($"{Application.dataPath}/Resources", "*.*", SearchOption.AllDirectories);
        foreach (var file in allFiles)
        {
            string fPath= file.Replace(@"\", "/");
            string fPath1 = fPath.Replace(Application.dataPath, "Assets");
            AssetImporter ai= AssetImporter.GetAtPath(fPath);
        }
    }

    /// <summary>
    /// SĿ¼��Դֻ��
    /// SĿ¼����Դ���Դ洢��Build�����Ϸ����
    /// SĿ¼
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
    /// PĿ¼�ɶ���д
    /// PĿ¼�����е���Ϸ��Դ
    /// PĿ¼��SĿ¼�洢����Ϸ��Դ��һС���֣�
    /// </summary>
    [MenuItem("AssetBundlePack/persistentDataPath")]
    public static void AssetBundlePPath()
    {
        Process.Start(Application.persistentDataPath);
    }
}
