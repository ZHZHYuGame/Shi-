using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class TestAB : MonoBehaviour
{
    public List<Sprite> sprites = new List<Sprite>();
    public int index;
    public Image icon;
    public GameObject obj;
    void Start()
    {
        //第一步加载AB包
        AssetBundle AB = AssetBundle.LoadFromFile(Application.streamingAssetsPath + "/" + "model");

        AssetBundle mainAB = AssetBundle.LoadFromFile(Application.streamingAssetsPath + "/" + "PC");

        AssetBundleManifest abMainFest = mainAB.LoadAsset<AssetBundleManifest>("AssetBundleManifest");
        string[] str = abMainFest.GetAllDependencies("model");
        for (int i = 0; i < str.Length; i++)
        {
            AssetBundle.LoadFromFile(Application.streamingAssetsPath + "/" + str[i]);
        }
        //AssetBundle AB1 = AssetBundle.LoadFromFile(Application.streamingAssetsPath + "/" + "materia");
        //第二部 加载AB包中的资源
        //建议用泛型加载 或者是 type指定类型
        GameObject obj = AB.LoadAsset("Cube", typeof(GameObject)) as GameObject;
        Debug.Log(obj.name);
        Instantiate(obj);

        //AB包不能重复加载 否则报错
        // GameObject obj1 = AB.LoadAsset<GameObject>("Sphere");
        // Debug.Log(obj1.name);
        // Instantiate(obj1);
        // AB.Unload(false);
        //StartCoroutine(AsyncLoadRes("icon", sprites[index].name));

        //AssetDatabase.GetAssetPath("Resources/");


    }

    IEnumerator AsyncLoadRes(string abName, string resName)
    {
        AssetBundleCreateRequest assetBundleCreateRequest = AssetBundle.LoadFromFileAsync(Application.streamingAssetsPath + "/" + abName);

        while (index <= 105)
        {
            AssetBundleRequest sprite = assetBundleCreateRequest.assetBundle.LoadAssetAsync<Sprite>(sprites[index].name);
            icon.sprite = sprite.asset as Sprite;
            Debug.Log(index);
            index++;
            yield return new WaitForSeconds(2);
        }
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            AssetBundle.UnloadAllAssetBundles(false);
        }
    }
}
