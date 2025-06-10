using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;
public class ABTool
{
    [MenuItem("2210/ABTool",false,100)]
    static void MenuItem()
    {
        Debug.Log("你好");
    }
    [MenuItem("2210/Path", false, 111)]
    static void MenuItem1()
    {
        Object[] objects = Selection.GetFiltered<Object>(SelectionMode.DeepAssets); 
        for (int i = 0; i < objects.Length; i++)
        { 
            string path=AssetDatabase.GetAssetPath(objects[i]);
            Debug.Log($"名字={objects[i].name},路径={path}");
        }
    }



    [MenuItem("2210/手动打包", false, 111)]
    static void MenuItem2()
    {
        BuildPipeline.BuildAssetBundles(GetOutPath("AB"),BuildAssetBundleOptions.ChunkBasedCompression,BuildTarget.StandaloneWindows);
        AssetDatabase.Refresh();
    }


    [MenuItem("2210/自动打包", false, 111)]
    static void MenuItem3()
    {
        Object[] objects = Selection.GetFiltered<Object>(SelectionMode.DeepAssets);
        for (int i = 0; i < objects.Length; i++)
        {
            string path = AssetDatabase.GetAssetPath(objects[i]);
            AssetImporter assetImporter= AssetImporter.GetAtPath(path);
            assetImporter.assetBundleName = objects[i].name;
            assetImporter.assetBundleVariant ="u3d";

            Debug.Log($"名字={objects[i].name},路径={path}");
        }

        BuildPipeline.BuildAssetBundles(GetOutPath("AB"), BuildAssetBundleOptions.ChunkBasedCompression, BuildTarget.StandaloneWindows);
        //ClearFlog();
        AssetDatabase.Refresh();
    }

    static void ClearFlog()
    {
        
            Object[] objects = Selection.GetFiltered<Object>(SelectionMode.DeepAssets);
            for (int i = 0; i < objects.Length; i++)
            {
                string path = AssetDatabase.GetAssetPath(objects[i]);
                AssetImporter assetImporter = AssetImporter.GetAtPath(path);
                assetImporter.assetBundleName ="";
               
            }
        AssetDatabase.RemoveUnusedAssetBundleNames();
    }


    static string GetOutPath(string subFolder)
    {
        string path = Application.streamingAssetsPath + "/" + subFolder + "/";
        if(!Directory.Exists(path))
        {
            Directory.CreateDirectory(path);
        }
        return path;
    }
}
