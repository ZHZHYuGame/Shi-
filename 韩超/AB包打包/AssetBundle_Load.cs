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
        //��һ�� ����AB��
        AssetBundle ab = AssetBundle.LoadFromFile(Application.streamingAssetsPath + "/" + "iconcube");
        //����������
        //AssetBundle ab2 = AssetBundle.LoadFromFile(Application.streamingAssetsPath + "/" + "iconsprite");
        //�ڶ��� ����AB���е���Դ
        GameObject cube = ab.LoadAsset("Cube", typeof(GameObject)) as GameObject;
        //����������Ҫ֪ʶ��--�������� ��ȡ������Ϣ
        //��������
        AssetBundle abMain = AssetBundle.LoadFromFile(Application.streamingAssetsPath + "/" + "PC");
        //���������еĹ̶��ļ�
        AssetBundleManifest abMainfest = abMain.LoadAsset<AssetBundleManifest>("AssetBundleManifest");
        //�ӹ̶��ļ��� �õ�������Ϣ
        string[] str = abMainfest.GetAllDependencies("iconcube");
        //�õ���������������
        for (int i = 0; i < str.Length; i++)
        {
            AssetBundle.LoadFromFile(Application.streamingAssetsPath + "/" + str[i]);
        }
        //����
        //GameObject cube = ab.LoadAsset<GameObject>("cube");
        Instantiate(cube);
        
        //�첽����ͼƬ ----Э��
        //StartCoroutine(LoadSprite("iconsprite", "icon_74"));
    }

    IEnumerator LoadSprite(string ABname, string dename)
    {
        //����AB��
        AssetBundleCreateRequest res = AssetBundle.LoadFromFileAsync(Application.streamingAssetsPath + "/" + ABname);

        yield return res;
        //������Դ
        AssetBundleRequest abq = res.assetBundle.LoadAssetAsync(dename, typeof(Sprite));

        yield return abq;

        img.sprite = abq.asset as Sprite;
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //ж�����м��ص�AB�� ����Ϊture ���ͨ��AB�����ص���Դ��ж����
            AssetBundle.UnloadAllAssetBundles(false);
        }
    }
}
