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
/// ��Դ�ȸ���
/// 1.��Դ��������ַ  url
/// 2.�����ַ���������ļ�?
/// 3.�汾�������ļ�
/// 4.�뱾�ذ汾�������ļ��Աȣ��ڱ��ش����ļ�������£��粻�����ǵ�?1�����أ�ȫ�����أ�������ԣ�?
/// 4.��Դ�嵥�����ļ�
/// 5.�뱾����Դ�嵥�����ļ��Ա�
/// 6.���ԱȵĲ�ͬ��Դ���뵽�����б���
/// 7.ͨ��UnityWebQuest���������Դ����?
/// 8.���ظ��½�����������Ϸ
/// </summary>
public class HotManager : MonoBehaviour
{
    /// <summary>
    /// ��Դ������Title
    /// </summary>
    string urlPath = "127.0.0.1/AFGame";
    /// <summary>
    /// ���������°汾��
    /// </summary>
    string serverVersion = "";
    /// <summary>
    /// ������������Դ�嵥
    /// </summary>
    string serverABManifest = "";
    /// <summary>
    /// ��Դ���ض���
    /// </summary>
    Queue<AssetItem> assetLoadQue = new Queue<AssetItem>();
    /// <summary>
    /// ��������Դ�嵥Item�ֵ�
    /// </summary>
    Dictionary<string, AssetItem> serverAssetDict = new Dictionary<string, AssetItem>();
    /// <summary>
    /// ������Դ�嵥Item�ֵ�
    /// </summary>
    Dictionary<string, AssetItem> localAssetDict = new Dictionary<string, AssetItem>();

    // Start is called before the first frame update
    void Start()
    {
        UnityEngine.Debug.Log("����start");
        //�������İ汾���ļ�·��
        string path = $"{urlPath}/GameVersion.txt";
        UnityEngine.Debug.Log(path);
        LoadGameAsset(path, (byteData) =>
        {
            //������Դ�������ϵ����°汾��
            serverVersion = UTF8Encoding.UTF8.GetString(byteData);
            //��¼��������ϸ�汾��
            VersionItem sVersionItem = new VersionItem(serverVersion);
            //���صİ汾����ʲô
            string localVerStr = Application.persistentDataPath + "/GameVersion.txt";
            //���صİ汾���ļ��Ƿ����?
            if (File.Exists(localVerStr))
            {
                UnityEngine.Debug.Log("�ļ�����");
                //�ȸ���
                //������ϸ�汾��
                VersionItem lVersionItem = new VersionItem(File.ReadAllText(localVerStr));
                //�����汾�ŷ����仯
                if (sVersionItem.big > lVersionItem.big)
                {
                    //ǿ�����ظ��ǡ�Ӧ���̵���������
                }
                else
                {
                    //����а汾�ŷ�����?
                    if (sVersionItem.middle > lVersionItem.middle)
                    {

                    }
                    else
                    {
                        //���С�汾�ŷ�����?
                        if (sVersionItem.small > lVersionItem.small)
                        {
                            //�ȸ���Ϸ��Դ
                            LoadHotGameAsset();
                            UnityEngine.Debug.Log("222");
                        }
                    }
                }
            }
            //��1�ΰ�װ��Ϸ�����ų��쳣�Ƴ����𻵣�
            else
            {
                //��1����ȫ����
                LoadGameAllAsset();
                UnityEngine.Debug.Log("111");
            }

        });
    }
    /// <summary>
    /// ����������Դ����������Դ
    /// </summary>
    private void LoadGameAllAsset()
    {
        //��Դ�嵥�����ļ�
        string assetPath = $"{urlPath}/AssetBundleManifest.txt";
        LoadGameAsset(assetPath, (data) =>
        {
            //��Դ�嵥�ı��ַ���
            serverABManifest = UTF8Encoding.UTF8.GetString(data);
            //��ʽ�Ű�
            string[] aList = serverABManifest.Trim().Split(new string[] { "\r\n" }, StringSplitOptions.None);
            string[] assetStrList;
            foreach (var asset in aList)
            {
                assetStrList = asset.Split('|');
                AssetItem item = new AssetItem(assetStrList[0], assetStrList[1]);
                //��Դ�������ض���
                assetLoadQue.Enqueue(item);
            }
            //��ʼ������Դ
            DownLoadAsset(assetLoadQue.Dequeue());
        });
    }
    /// <summary>
    /// ��Դ�ȸ���(��ͬ�汾)
    /// </summary>
    void LoadHotGameAsset()
    {
        //��Դ�嵥�����ļ�
        string assetPath = $"{urlPath}/AssetBundleManifest.txt";
        LoadGameAsset(assetPath, (data) =>
        {
            //��Դ�嵥�ı��ַ���
            serverABManifest = UTF8Encoding.UTF8.GetString(data);
            ABManifestToItemDict(serverABManifest, serverAssetDict);
            //�����嵥
            string localABManifest = Application.persistentDataPath + "/AssetBundleManifest.txt";
            ABManifestToItemDict(File.ReadAllText(localABManifest), localAssetDict);
            UnityEngine.Debug.Log("333");
            //�Աȷ������뱾�ص���Դ�嵥���ҳ�������Ҫ���µ���Դ���������ض���
            CompareServerAndLocalToLoadQueue();
            if (assetLoadQue.Count > 0)
            DownLoadAsset(assetLoadQue.Dequeue());
        });
    }
    /// <summary>
    /// ͨ����Դ�嵥�ļ����ݽ���AssetItem����
    /// </summary>
    /// <param name="abStr"></param>
    /// <param name="dict"></param>
    void ABManifestToItemDict(string abStr, Dictionary<string, AssetItem> dict)
    {
        //��ʽ�Ű�
        string[] aList = abStr.Trim().Split(new string[] { "\r\n" }, StringSplitOptions.None);
        string[] assetStrList;
        foreach (var asset in aList)
        {
            assetStrList = asset.Split('|');
            AssetItem item = new AssetItem(assetStrList[0], assetStrList[1]);
            //��ԴItem����������ֵ����
            dict.Add(item.abName, item);
        }
    }
    /// <summary>
    /// �Աȷ������뱾�ص���Դ�嵥�б����ҵ���Щ��Ҫ���أ�������У���Щ���?ɾ��
    /// </summary>
    void CompareServerAndLocalToLoadQueue()
    {
        foreach (var sAsset in serverAssetDict)
        {
            //��������嵥���з����������?
            if (localAssetDict.ContainsKey(sAsset.Key))
            {
                //���ڵ���Դ��ͬ���������²���
                if (sAsset.Value.abMD5 != localAssetDict[sAsset.Key].abMD5)
                {
                    //�������ض���
                    assetLoadQue.Enqueue(sAsset.Value);
                }
            }
            else
            {
                //�����ӵ���Դ������ҪMD5���ж�
                //�������ض���
                assetLoadQue.Enqueue(sAsset.Value);
            }
        }

        foreach (var lAsset in localAssetDict)
        {
            //��������嵥���з����������?
            if (!serverAssetDict.ContainsKey(lAsset.Key))
            {
               //ɾ��������Դ
            }
        }
    }

    void LoadGameAsset(string path, Action<byte[]> complete)
    {
        StartCoroutine(DownLoadGameAsset(path, complete));
    }
    /// <summary>
    /// ͨ��Э��������������Դ�Ķ�̬���أ�ͨ��ÿ֡�������ֽ���������
    /// </summary>
    /// <param name="path"></param>
    /// <param name="complete"></param>
    /// <returns></returns>
    IEnumerator DownLoadGameAsset(string path, Action<byte[]> complete)
    {
        UnityWebRequest web = UnityWebRequest.Get(path);
        UnityWebRequestAsyncOperation op = web.SendWebRequest();
        yield return new WaitForSeconds(1f);
        print("ִ��Э��");
        if (op.isDone)
        {
            complete?.Invoke(web.downloadHandler.data);
        }
    }
    /// <summary>
    /// ���ؾ����AB�������ӡ����ǡ�ɾ����
    /// </summary>
    /// <param name="ab"></param>
    public void DownLoadAsset(AssetItem ab)
    {
        //�����AB������·��
        string assetPath = $"{urlPath}/{ab.abName}";
        LoadGameAsset(assetPath, (data) =>
        {
            //ȷ��д�뵽�ͻ���Ӳ��λ��
            string localAssetPath = Application.persistentDataPath + "/" + ab.abName;
            if (Directory.Exists(localAssetPath))
            {
                File.Delete(localAssetPath);
            }
            //д�����?
            string assetDir = Path.GetDirectoryName(localAssetPath);
            if (!Directory.Exists(assetDir))
            {
                Directory.CreateDirectory(assetDir);
            }
            //��Դд�뵽��ӦӲ��λ��
            File.WriteAllBytes(localAssetPath, data);
            //������ض��л������?
            if (assetLoadQue.Count > 0)
            {
                DownLoadAsset(assetLoadQue.Dequeue());
            }
            else
            {
                //��Դȫ���������?
                //���½�����Ϸ
                //ֱ�ӽ�����Ϸ
                SaveVersion();
                SaveAssetList();
                Process.Start(Application.persistentDataPath);
            }
        });
    }
    /// <summary>
    /// ������·������汾���ļ�?
    /// </summary>
    private void SaveVersion()
    {
        File.WriteAllText(Application.persistentDataPath + "/GameVersion.txt", serverVersion);
    }
    /// <summary>
    /// ������·������嵥�ļ�?
    /// </summary>
    private void SaveAssetList()
    {
        File.WriteAllText(Application.persistentDataPath + "/AssetBundleManifest.txt", serverABManifest);
    }
}
/// <summary>
/// AB��������
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
/// ��Ϸ�汾��
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
