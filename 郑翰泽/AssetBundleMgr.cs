using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Security.Cryptography;
using System;

public class AssetBundleMgr : MonoBehaviour
{
    [MenuItem("AssetBundlePack/һ�����")]
    public static void AssetBundlePackage()
    {
        RemoveAssetBundle();
        //SĿ¼
        string sPath = Application.streamingAssetsPath;
        //PĿ¼���ɶ���д��
        string pPath = Application.persistentDataPath;

        //���ͷ���
        AssetBundleType();

        BuildPipeline.BuildAssetBundles(sPath, BuildAssetBundleOptions.ChunkBasedCompression, BuildTarget.StandaloneWindows64);

        Process.Start(sPath);
    }


    public static void AssetBundleType()
    {
        //�ҵ�ResourcesĿ¼�µ�������Դ�ļ�
        string[] allFiles = Directory.GetFiles($"{Application.dataPath}/Resources", "*.*", SearchOption.AllDirectories);

        allFiles = allFiles.Where((o => Path.GetExtension(o).ToLower() != ".meta")).ToArray();
        StringBuilder sb = new StringBuilder();
        foreach (var file in allFiles)
        {
            //ͳһ��ʽ��\���/��
            string fPath = file.Replace(@"\", "/");
            string fPath1 = fPath.Replace(Application.dataPath, "Assets");//�޸�·��
            string fName = Path.GetFileNameWithoutExtension(file);//����ļ���
            string fExten = Path.GetExtension(file);//��ú�׺
            AssetImporter ai = AssetImporter.GetAtPath(fPath1);
            if (fExten == ".mat")//�����׺��.mat
            {
                ai.assetBundleName = "Material/" + fName;
            }
            else if (fExten == ".prefab")
            {
                ai.assetBundleName = "Prefab/" + fName;
            }

            ai.assetBundleVariant = "ud";
            //MD��
            string md5 = GetMD5(file);

            sb.Append(fPath1 + "|" + md5 + "\r\n");
        }

        SaveAssetBundleMainifest(sb.ToString());
    }

    /// <summary>
    /// ת��һ����Դ��MD5��
    /// </summary>
    /// <param name="path"></param>
    /// <returns></returns>
    private static string GetMD5(string path)
    {
        byte[] data = File.ReadAllBytes(path);
        MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
        byte[] mData = md5.ComputeHash(data);
        return BitConverter.ToString(mData).Replace("-", "");
    }

    static void SaveAssetBundleMainifest(string desc)
    {
        File.WriteAllText(Application.streamingAssetsPath + "/ABManifest.txt", desc);
    }

    /// <summary>
    /// SĿ¼��Դֻ��
    /// SĿ¼����Դ���Դ�����Build�����Ϸ����
    /// </summary>
    [MenuItem("AssetBundlePack/streamingAssetsPath")]
    public static void AssetBundleSPath()
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
    /// PĿ¼��SĿ¼�������Ϸ��Դ��һ���֣�
    /// </summary>
    [MenuItem("AssetBundlePack/persistentDataPath")]
    public static void AssetBundlePPath()
    {
        Process.Start(Application.persistentDataPath);
    }


    public static void RemoveAssetBundle()
    {
        string sPath = Application.streamingAssetsPath;
        string[] allFiles = Directory.GetFiles(sPath, "*.*", SearchOption.AllDirectories);
        foreach (var file in allFiles)
        {
            File.Delete(file);
        }
    }
}