using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIMgr : MonoBehaviour
{
    public Button aud_Btn;
    public AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        aud_Btn.onClick.AddListener(OnLoadMusic);
    }

    private void OnLoadMusic()
    {
        AssetBundle ab = AssetBundle.LoadFromFile(GetPath+"bgm.u3d");
        AudioClip clip = ab.LoadAsset<AudioClip>("bgm");
        audioSource.clip = clip;
        audioSource.Play();
        ab.Unload(false);
    }
    private string GetPath
    {
        get
        {
            return Application.streamingAssetsPath + "/AB/";
        }
       
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
