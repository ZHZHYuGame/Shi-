using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AssetBundle_Load : MonoBehaviour
{
    public Image img;
    // Start is called before the first frame update
    void Start()
    {
        //第一步 加载AB包
        AssetBundle ab = AssetBundle.LoadFromFile(Application.streamingAssetsPath + "/" + "iconcube");
        //加载依赖包
        //AssetBundle ab2 = AssetBundle.LoadFromFile(Application.streamingAssetsPath + "/" + "iconsprite");
        //第二步 加载AB包中的资源
        GameObject cube = ab.LoadAsset("Cube", typeof(GameObject)) as GameObject;
        //依赖包的重要知识点--利用主包 获取依赖信息
        //加载主包
        AssetBundle abMain = AssetBundle.LoadFromFile(Application.streamingAssetsPath + "/" + "PC");
        //加载主包中的固定文件
        AssetBundleManifest abMainfest = abMain.LoadAsset<AssetBundleManifest>("AssetBundleManifest");
        //从固定文件中 得到依赖信息
        string[] str = abMainfest.GetAllDependencies("iconcube");
        //得到了依赖包的名字
        for (int i = 0; i < str.Length; i++)
        {
            AssetBundle.LoadFromFile(Application.streamingAssetsPath + "/" + str[i]);
        }
        //泛型
        //GameObject cube = ab.LoadAsset<GameObject>("cube");
        Instantiate(cube);
        
        //异步加载图片 ----协程
        //StartCoroutine(LoadSprite("iconsprite", "icon_74"));
    }

    IEnumerator LoadSprite(string ABname, string dename)
    {
        //加载AB包
        AssetBundleCreateRequest res = AssetBundle.LoadFromFileAsync(Application.streamingAssetsPath + "/" + ABname);

        yield return res;
        //加载资源
        AssetBundleRequest abq = res.assetBundle.LoadAssetAsync(dename, typeof(Sprite));

        yield return abq;

        img.sprite = abq.asset as Sprite;
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //卸载所有加载的AB包 参数为ture 会把通过AB包加载的资源都卸载了
            AssetBundle.UnloadAllAssetBundles(false);
        }
    }
}
