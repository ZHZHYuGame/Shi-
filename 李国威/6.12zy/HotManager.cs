using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;
using Debug = UnityEngine.Debug;
using Directory = System.IO.Directory;
using File = System.IO.File;

/// <summary>
/// ��Դ�ȸ���
/// 1.�ȸ��·�������ַ url
/// 2.�����ַ���������ļ�
/// 3.�汾�������ļ�
/// 4.�뱾�ذ汾�������ļ��Աȣ��ڱ��ش����ļ�������£��粻�����ǵ�1�����أ�ȫ�����أ�������ԣ�
/// 5.��Դ�嵥�����ļ�
/// 6.�뱾����Դ�嵥�����ļ��Աȣ��ڱ��ش����ļ�������£��粻�����ǵ�1�����أ�ȫ�����أ�
/// 7.���ԱȵĲ�ͬ��Դ���뵽�����б���
/// 8.ͨ��UnityWebQuest���������Դ����
/// 9.���ظ��½�����������Ϸ
/// </summary>
public class HotManager : MonoBehaviour
{
    /// <summary>
    /// ��Դ������Title
    /// </summary>
    string urlPath = "127.0.0.1/ShiBa_Game";
    /// <summary>
    /// ���������°汾��
    /// </summary>
    string serverVersion = "";
    /// <summary>
    /// ������ ������Դ�嵥
    /// </summary>
    string serverABManifest = "";
    /// <summary>
    /// ��Դ�����б�
    /// </summary>
    Queue<AssetItem> assetLoadQue = new Queue<AssetItem>();
    /// <summary>
    /// ��������Դ�嵥item�ֵ�
    /// </summary>
    Dictionary<string, AssetItem> serverAssetDict = new Dictionary<string, AssetItem>();
    /// <summary>
    /// ������Դ�嵥�ֵ�
    /// </summary>
    Dictionary<string, AssetItem> localAssetDict = new Dictionary<string, AssetItem>();

    
    // Start is called before the first frame update
    void Start()
    {
       
        //�������İ汾���ļ�·��
        string path = $"{urlPath}/GameVersion.txt";
        LoadGameAsset(path, (byteData) =>
        {
            Debug.Log(22);
            //������Դ�������ϵ����°汾��
            serverVersion = UTF8Encoding.UTF8.GetString(byteData);
            //��¼����������ϸ�汾��
            VersionItem sVersionItem = new VersionItem(serverVersion);
            //���ذ汾����ʲô
            string localVerStr = Application.persistentDataPath + "/GameVersion.txt";           
            //�л��İ汾���ļ��Ƿ����
            if (File.Exists(localVerStr))
            {
                //�������  �ȸ���
                //������ϸ�汾��
                VersionItem lVersionItem = new VersionItem(File.ReadAllText(localVerStr));
                //�����汾�ŷ����仯
                if (sVersionItem.big>lVersionItem.big)
                {
                    //ǿ�����ظ��ǣ�Ӧ���̵���������
                }
                else
                {
                    //����а汾�����仯
                    if (sVersionItem.middle > lVersionItem.middle)
                    {
                        //
                    }
                    else
                    {
                        //���С�汾�ŷ����仯
                        if (sVersionItem.small > lVersionItem.small)
                        {
                            //�ȸ�����Ϸ��Դ
                            LoadHotGameAsset();
                        }
                    }
                }
            }
            //��һ�ΰ�װ��Ϸ�����ų��쳣�Ƴ����𻵣�
            else
            {
                //��һ����ȫ����
                LoadGameAllAsset();
            }
            
        });
    }

   
    /// <summary>
    /// ����������Դ����������Դ
    /// </summary>
    /// <exception cref="NotImplementedException"></exception>
    private void LoadGameAllAsset()
    {
        //��Դ�嵥�����ļ�
        string assetPath = $"{urlPath}/ABManifest.txt";
        LoadGameAsset(assetPath, (data) =>
        {
            //��Դ�嵥���ı��ַ���
            serverABManifest = UTF8Encoding.UTF8.GetString(data);
            //��ʽ�Ű�
            string[] aList = serverABManifest.Trim().Split(new string[] { "\r\n" },StringSplitOptions.None);
            string[] assetStrList;
            foreach (var asset in aList)
            {
                assetStrList = asset.Split("|");
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
        string assetPath = $"{urlPath}/ABManifest.txt";
        LoadGameAsset(assetPath, (data) =>
        {
            //��Դ�嵥���ı��ַ���
            serverABManifest = UTF8Encoding.UTF8.GetString(data);
            ABManifestToITemDict(serverABManifest, serverAssetDict);
            //�����嵥
            string localABManifest = Application.persistentDataPath + "/ABManifest.txt";
            ABManifestToITemDict(File.ReadAllText(localABManifest), localAssetDict);
            //�Աȷ������뱾�ص���Դ�嵥������������Ҫ���µ���Դ��������ض���
            CompareServerAndLocalToLoadQueue();
            //
            if(assetLoadQue.Count>0)
            DownLoadAsset(assetLoadQue.Dequeue());
        });
        
    }


    /// <summary>
    /// ͨ����Դ�嵥�ļ����ݽ���AssetItem����
    /// </summary>
    /// <param name="abStr"></param>
    /// <param name="dict"></param>
    void ABManifestToITemDict(string abStr, Dictionary<string,AssetItem> dict)
    {
       
        //��ʽ�Ű�
        string[] aList = abStr.Trim().Split(new string[] { "\r\n" }, StringSplitOptions.None);
        string[] assetStrList;
        foreach (var asset in aList)
        {
            assetStrList = asset.Split("|");
            AssetItem item = new AssetItem(assetStrList[0], assetStrList[1]);
            //��ԴItem����������ֵ����
            dict.Add(item.abName, item);
        }
    }
    /// <summary>
    /// �Աȷ������뱾�ص���Դ�嵥�б��ҳ���Щ��Ҫ���أ�������У���Щ��Ҫɾ��
    /// </summary>
    private void CompareServerAndLocalToLoadQueue()
    {
        foreach (var sAsset in serverAssetDict)
        {
            //��������嵥���з���������Դ
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
            //��������嵥���з���������Դ
            if (!serverAssetDict.ContainsKey(lAsset.Key))
            {
                //ɾ��������Դ

            }
        }
    }

    void LoadGameAsset(string path, Action<byte[]> complete)
    {
        StartCoroutine(DownLoadGameAsset(path,complete));
    }

    /// <summary>
    /// ͨ��Э��������������Դ�Ķ�̬���أ�ͨ��ÿ֡�������ֽ���������
    /// </summary>
    /// <returns></returns>
    IEnumerator DownLoadGameAsset(string path, Action<byte[]> complete)
    {
        UnityWebRequest web = UnityWebRequest.Get(path);
        UnityWebRequestAsyncOperation op = web.SendWebRequest();

        yield return new WaitForSeconds(0.05f);

        if (op.isDone)//������سɹ�
        {

            // web.downloadProgress; //������ʾ
            // web.downloadedBytes;//������
            complete?.Invoke(web.downloadHandler.data);
        }

    }

    /// <summary>
    /// ���ؾ����AB��(��ӡ����ǡ�ɾ��)
    /// </summary>
    /// <param name="ab"></param>
    public void DownLoadAsset(AssetItem ab)
    {
        //�����AB������·��
        string assetPath = $"{urlPath}/{ab.abName}";
        LoadGameAsset(assetPath, (data) =>
        {
            //ȷ��д�뵽�ͻ���Ӳ��λ��
            string locaAssetPath = Application.persistentDataPath + "/" + ab.abName;
            if (Directory.Exists(locaAssetPath))
            {
                File.Delete(locaAssetPath);
            }
            //д�����
            string assetDir = Path.GetDirectoryName(locaAssetPath);
            if (!Directory.Exists(assetDir))
            {
                Directory.CreateDirectory(assetDir);
            }
            //��Դд�뵽��ӦӲ��λ��
            File.WriteAllBytes(locaAssetPath,data);
            //������ض��л�����Դ
            if (assetLoadQue.Count > 0)
            {
                DownLoadAsset(assetLoadQue.Dequeue());
            }
            else
            {
                //��Դȫ���������
                //���½�����Ϸ����ֱ�ӽ�����Ϸ
                SaveVersion();
                SaveAssetList();
                Process.Start(Application.persistentDataPath);
            }
        });
    }

    /// <summary>
    /// ������·������汾���ļ�
    /// </summary>
    private void SaveVersion()
    {
        File.WriteAllText(Application.persistentDataPath+"/GameVersion.txt",serverVersion);
    }
    /// <summary>
    /// ���������Դ�嵥
    /// </summary>
    private void SaveAssetList()
    {
        File.WriteAllText(Application.persistentDataPath + "/ABManifest.txt", serverABManifest);
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
        this.big =int.Parse(verStrList[0]);
        this.middle = int.Parse(verStrList[1]);
        this.small = int.Parse(verStrList[2]);
    }
}