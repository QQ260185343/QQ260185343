using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Experimental.AssetBundlePatching;
using UnityEngine.Networking;
using UnityEditor;
using System.Text;
using UnityEngine.UI;
/// <summary>
/// 加载服务器资源并且显示在场景_测试使用
/// </summary>
public class UnityWebRequestLoadForAssetBundleExamples : MonoBehaviour
{


    string U = "http://localhost/Upload/upload/AssetBundles/StandaloneWindows/model.u3d";
    string loadAssetURL = @"http://192.168.10.203/AssetBundles/Android/model.u3d";
    string loadAssetURL1 = @"http://webar.hereto.cn/upload/UpLoad/upload/model.u3d";
    string loadDateUrl = "http://webar.hereto.cn/upload/UpLoad/upload/MYTool.txt";
    // 初始化
    string ABPath = @"D:\phpstudy_pro\WWW\UpLoad\upload\AssetBundles\StandaloneWindows\realmodels";
    void Awake()
    {

   }
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(InstanceLoadFromMemoryEncry(ABPath));
    }

    // Update is called once per frame
    void Update()
    {

    }
    /// <summary>
    /// 加载一个服务器的资源
    /// </summary>
    /// <returns></returns>
    IEnumerator InstanceLoadFromAssetBundles(string URL, string ModelName)
    {
        var uwr = UnityWebRequestAssetBundle.GetAssetBundle(URL);
        yield return uwr.SendWebRequest();

        // Get an asset from the bundle and instantiate it.
        AssetBundle bundle = DownloadHandlerAssetBundle.GetContent(uwr);
        var loadAsset = bundle.LoadAssetAsync<GameObject>(ModelName);
        yield return loadAsset;
        GameObject prefab = loadAsset.asset as GameObject;
        Instantiate(prefab, new Vector3(0f, 0f, 0f), Quaternion.identity);

    }


   
    /// <summary>
    /// 通过文件流加载一个加密资源
    /// </summary>
    /// <returns></returns>
    IEnumerator InstanceLoadFromMemoryEncry( string Path)
    {
        yield return null; 
        EncryStream fileStream = new EncryStream(Path, FileMode.Open, FileAccess.Read, FileShare.None, 1024 * 4, false);
        var myLoadedAssetBundle = AssetBundle.LoadFromStream(fileStream);
        GameObject prefab = myLoadedAssetBundle.LoadAsset<GameObject>(System.IO.Path.GetFileName(Path));
        Instantiate(prefab, new Vector3(0f, 0f, 0f), Quaternion.identity);
        myLoadedAssetBundle.Unload(false);
    }

    /// <summary>
    /// 从本地加载一个资源
    /// </summary>
    /// <param name="ModelName"></param>
    /// <returns></returns>
    IEnumerator InstanceLoadFromMemory(string Path)
    {
        yield return null;       
        var myLoadedAssetBundle = AssetBundle.LoadFromFile(Path);
        GameObject prefab = myLoadedAssetBundle.LoadAsset<GameObject>(System.IO.Path.GetFileName(Path));
        Instantiate(prefab, new Vector3(0f, 0f, 0f), Quaternion.identity);
        myLoadedAssetBundle.Unload(false);
    }
}






