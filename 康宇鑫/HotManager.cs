using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;
using UnityEngine.Networking;
using System.Threading;
using System.IO;
using System.Diagnostics;
/// <summary>
/// 资源热更新
/// 1.资源服务器地址  url
/// 2.请求地址下载索引文件
/// 3.版本号索引文件
/// 4.与本地版本号索引文件对比（在本地存在文件的情况下，如不存在是第1次下载，全部下载，后面忽略）
/// 4.资源清单索引文件
/// 5.与本地资源清单索引文件对比
/// 6.将对比的不同资源加入到下载列表中
/// 7.通过UnityWebQuest进行逐个资源下载
/// 8.下载更新结束，进入游戏
/// </summary>
public class HotManager : MonoBehaviour
{
    /// <summary>
    /// 资源服务器Title
    /// </summary>
    string urlPath = "127.0.0.1/ShiBa_Game";
    /// <summary>
    /// 服务器最新版本号
    /// </summary>
    string serverVersion = "";
    /// <summary>
    /// 服务器最新资源清单
    /// </summary>
    string serverABManifest = "";
    /// <summary>
    /// 资源下载队列
    /// </summary>
    Queue<AssetItem> assetLoadQue = new Queue<AssetItem>();
    /// <summary>
    /// 服务器资源清单Item字典
    /// </summary>
    Dictionary<string, AssetItem> serverAssetDict = new Dictionary<string, AssetItem>();
    /// <summary>
    /// 本地资源清单Item字典
    /// </summary>
    Dictionary<string, AssetItem> localAssetDict = new Dictionary<string, AssetItem>();

    // Start is called before the first frame update
    void Start()
    {
        //服务器的版本号文件路径
        string path = $"{urlPath}/GameVersion.txt";
        LoadGameAsset(path, (byteData) =>
        {
            //现在资源服务器上的最新版本号
            serverVersion = UTF8Encoding.UTF8.GetString(byteData);
            //记录服务器详细版本号
            VersionItem sVersionItem = new VersionItem(serverVersion);
            //本地的版本号是什么
            string localVerStr = Application.persistentDataPath + "/GameVersion.txt";
            //本地的版本号文件是否存在
            if (File.Exists(localVerStr))
            {
                //热更新
                //本地详细版本号
                VersionItem lVersionItem = new VersionItem(File.ReadAllText(localVerStr));
                //如果大版本号发生变化
                if (sVersionItem.big > lVersionItem.big)
                {
                    //强行下载覆盖、应用商店重新下载
                }
                else
                {
                    //如果中版本号发生变化
                    if (sVersionItem.middle > lVersionItem.middle)
                    {

                    }
                    else
                    {
                        //如果小版本号发生变化
                        if (sVersionItem.small > lVersionItem.small)
                        {
                            //热更游戏资源
                            LoadHotGameAsset();
                        }
                    }
                }
            }
            //第1次安装游戏（先排除异常移除、损坏）
            else
            {
                //第1次完全下载
                LoadGameAllAsset();
            }

        });
    }
    /// <summary>
    /// 下载所有资源服务器的资源
    /// </summary>
    private void LoadGameAllAsset()
    {
        //资源清单索引文件
        string assetPath = $"{urlPath}/ABManifest.txt";
        LoadGameAsset(assetPath, (data) =>
        {
            //资源清单文本字符串
            serverABManifest = UTF8Encoding.UTF8.GetString(data);
            //格式排版
            string[] aList = serverABManifest.Trim().Split(new string[] { "\r\n" }, StringSplitOptions.None);
            string[] assetStrList;
            foreach (var asset in aList)
            {
                assetStrList = asset.Split('|');
                AssetItem item = new AssetItem(assetStrList[0], assetStrList[1]);
                //资源加入下载队列
                assetLoadQue.Enqueue(item);
            }
            //开始下载资源
            DownLoadAsset(assetLoadQue.Dequeue());
        });
    }
    /// <summary>
    /// 资源热更新(不同版本)
    /// </summary>
    void LoadHotGameAsset()
    {
        //资源清单索引文件
        string assetPath = $"{urlPath}/ABManifest.txt";
        LoadGameAsset(assetPath, (data) =>
        {
            //资源清单文本字符串
            serverABManifest = UTF8Encoding.UTF8.GetString(data);
            ABManifestToItemDict(serverABManifest, serverAssetDict);
            //本地清单
            string localABManifest = Application.persistentDataPath + "/ABManifest.txt";
            ABManifestToItemDict(File.ReadAllText(localABManifest), localAssetDict);

            //对比服务器与本地的资源清单，找出所有需要更新的资源，加入下载队列
            CompareServerAndLocalToLoadQueue();
            if (assetLoadQue.Count > 0)
            DownLoadAsset(assetLoadQue.Dequeue());
        });
    }
    /// <summary>
    /// 通过资源清单文件数据进行AssetItem管理
    /// </summary>
    /// <param name="abStr"></param>
    /// <param name="dict"></param>
    void ABManifestToItemDict(string abStr, Dictionary<string, AssetItem> dict)
    {
        //格式排版
        string[] aList = abStr.Trim().Split(new string[] { "\r\n" }, StringSplitOptions.None);
        string[] assetStrList;
        foreach (var asset in aList)
        {
            assetStrList = asset.Split('|');
            AssetItem item = new AssetItem(assetStrList[0], assetStrList[1]);
            //资源Item存入服务器字典管理
            dict.Add(item.abName, item);
        }
    }
    /// <summary>
    /// 对比服务器与本地的资源清单列表，找到哪些需要下载，加入队列，哪些需要删除
    /// </summary>
    void CompareServerAndLocalToLoadQueue()
    {
        foreach (var sAsset in serverAssetDict)
        {
            //如果本地清单含有服务器的资源
            if (localAssetDict.ContainsKey(sAsset.Key))
            {
                //存在的资源不同，产生更新操作
                if (sAsset.Value.abMD5 != localAssetDict[sAsset.Key].abMD5)
                {
                    //加入下载队列
                    assetLoadQue.Enqueue(sAsset.Value);
                }
            }
            else
            {
                //新增加的资源，不需要MD5码判断
                //加入下载队列
                assetLoadQue.Enqueue(sAsset.Value);
            }
        }

        foreach (var lAsset in localAssetDict)
        {
            //如果本地清单含有服务器的资源
            if (!serverAssetDict.ContainsKey(lAsset.Key))
            {
               //删除本地资源
            }
        }
    }

    void LoadGameAsset(string path, Action<byte[]> complete)
    {
        StartCoroutine(DownLoadGameAsset(path, complete));
    }
    /// <summary>
    /// 通过协程来进行网上资源的动态下载，通过每帧来进行字节流的下载
    /// </summary>
    /// <param name="path"></param>
    /// <param name="complete"></param>
    /// <returns></returns>
    IEnumerator DownLoadGameAsset(string path, Action<byte[]> complete)
    {
        UnityWebRequest web = UnityWebRequest.Get(path);
        UnityWebRequestAsyncOperation op = web.SendWebRequest();

        yield return new WaitForSeconds(0.05f);

        if (op.isDone)
        {
            complete?.Invoke(web.downloadHandler.data);
        }
    }
    /// <summary>
    /// 下载具体的AB包（添加、覆盖、删除）
    /// </summary>
    /// <param name="ab"></param>
    public void DownLoadAsset(AssetItem ab)
    {
        //具体的AB包加载路径
        string assetPath = $"{urlPath}/{ab.abName}";
        LoadGameAsset(assetPath, (data) =>
        {
            //确定写入到客户端硬盘位置
            string localAssetPath = Application.persistentDataPath + "/" + ab.abName;
            if (Directory.Exists(localAssetPath))
            {
                File.Delete(localAssetPath);
            }
            //写入操作
            string assetDir = Path.GetDirectoryName(localAssetPath);
            if (!Directory.Exists(assetDir))
            {
                Directory.CreateDirectory(assetDir);
            }
            //资源写入到对应硬盘位置
            File.WriteAllBytes(localAssetPath, data);
            //如果下载队列还有资源
            if (assetLoadQue.Count > 0)
            {
                DownLoadAsset(assetLoadQue.Dequeue());
            }
            else
            {
                //资源全部下载完毕
                //重新进入游戏
                //直接进入游戏
                SaveVersion();
                SaveAssetList();
                Process.Start(Application.persistentDataPath);
            }
        });
    }
    /// <summary>
    /// 保存更新服务器版本号文件
    /// </summary>
    private void SaveVersion()
    {
        File.WriteAllText(Application.persistentDataPath + "/GameVersion.txt", serverVersion);
    }
    /// <summary>
    /// 保存更新服务器清单文件
    /// </summary>
    private void SaveAssetList()
    {
        File.WriteAllText(Application.persistentDataPath + "/ABManifest.txt", serverABManifest);
    }
}
/// <summary>
/// AB包数据类
/// </summary>
public class AssetItem
{
    public string abName;
    public string abMD5;

    public AssetItem(string abName, string abMD5)
    {
        this.abName = abName;
        this.abMD5 = abMD5;
    }
}
/// <summary>
/// 游戏版本号
/// </summary>
public class VersionItem
{
    public int big;
    public int middle;
    public int small;

    public VersionItem(string verStr)
    {
        string[] verStrList = verStr.Split('.');
        this.big = int.Parse(verStrList[0]);
        this.middle = int.Parse(verStrList[1]);
        this.small = int.Parse(verStrList[2]);
    }
}
