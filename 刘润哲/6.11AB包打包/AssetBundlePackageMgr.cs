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
    [MenuItem("AssetBundlePack/һ�����")]
    public static void AssetBundlePackage()
    {
        RemoveAssetBundle();

        //SĿ¼
        string sPath = Application.streamingAssetsPath;
        //PĿ¼
        string pPath = Application.persistentDataPath;

        AssetBundleType();

        BuildPipeline.BuildAssetBundles(sPath, BuildAssetBundleOptions.ChunkBasedCompression, BuildTarget.StandaloneWindows64);

        Process.Start(sPath);
    }

    public static void AssetBundleType()
    {
        //�ҵ�ResourcesĿ¼�µ����е���Դ�ļ�
        string[] allFiles = Directory.GetFiles($"{Application.dataPath}/Resources", "*.*", SearchOption.AllDirectories);

        allFiles=allFiles.Where((o=>Path.GetExtension(o).ToLower()!=".meta")).ToArray();
        StringBuilder sb = new StringBuilder();

        foreach (var file in allFiles)
        {
            //��·���ĸ�ʽͳһ \���/
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

            //MD5��
            string md5 = GetMD5(file);

            
            sb.Append(fPath1 + "|" + md5+"\r\n");
        }
        SaveAssetBundleManifest(sb.ToString());
    }

    /// <summary>
    /// SĿ¼��Դֻ��
    /// SĿ¼����Դ���Դ�����Build�����Ϸ����
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
    /// PĿ¼�ɶ���д
    /// PĿ¼�����е���Ϸ��Դ
    /// PĿ¼��SĿ¼��������Ϸ��Դ��һС���֣�
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
    /// ת��һ����Դ��MD5��
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
