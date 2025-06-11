using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;

public class AssetsBundleMgr : Editor
{
    [MenuItem("AssetsBundlePack/һ�����")]
    public static void AsstsBundlePack()
    {
        //sĿ¼
        string sPath=Application.streamingAssetsPath;

        //pĿ¼
        string pPath = Application.persistentDataPath;

        AssetBundleType();
        //1 ���·����streaming Ŀ¼�� 2.ѹ����ʽ�����ڿ��ѹ����֧�ֲ��ּ��أ� 3.Ŀ��ƽ̨��Window64λ��
        BuildPipeline.BuildAssetBundles(sPath, BuildAssetBundleOptions.ChunkBasedCompression, BuildTarget.StandaloneWindows64);
        //������ɺ�ͨ��ϵͳ���̴����Ŀ¼ �������ֶ��鿴���ɵ�AssetsBundle�ļ���
        Process.Start(sPath);
      

    }
    public static void AssetBundleType()
    {
        //�ҵ�ResourcesĿ¼�µ������ļ�
        string[] allFile = Directory.GetFiles($"{Application.dataPath}/Resources", "*.*", SearchOption.AllDirectories);
        allFile = allFile.Where(o=>Path.GetExtension(o).ToLower()!=".meta").ToArray();
        foreach (string file in allFile)
        {
            //��·���ĸ�ʽͳһΪ/
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
    /// sĿ¼
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
    /// PĿ¼
    /// </summary>

    [MenuItem("AssetsBundlePack/persistentDataPath")]
    public static void AssetBundlePPath()
    {
        
        Process.Start(Application.persistentDataPath);
    }
}
