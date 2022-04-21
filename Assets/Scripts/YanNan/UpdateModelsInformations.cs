using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;

public class UpdateModelsInformations : MonoBehaviour
{
    public List<GameObject> ModelgameObjects;
   // string posturl = "http://192.168.10.203/UpLoad/UnityUpload.php";
    string posturl = "http://webar.hereto.cn/upload/UpLoad/UnityUpload.php";

#if UNITY_ANDROID && !UNITY_EDITOR
      //android端的保存路径
        string Filepath = Application.persistentDataPath + @"/MYTool.txt";
#elif (UNITY_IOS || UNITY_IPHONE) && !UNITY_EDITOR
         string Filepath = Application.persistentDataPath + @"/MYTool.txt";
#else
    string Filepath = Application.streamingAssetsPath + @"/MYTool.txt";
#endif
    // 初始化
    void Awake()
    {
        for (int i = 0; i < gameObject.transform.childCount; i++)
        {
            ModelgameObjects.Add(gameObject.transform.GetChild(i).gameObject);
        }

    }

    private void Start()
        {

        // SaveData("测试:" + System.DateTime.Now);
        //  StartCoroutine(GetText());
        Debug.Log("这里加载模型");
    //    StartCoroutine(InstanceLoadFromAssetBundles("http://192.168.10.203/AssetBundles/StandaloneWindows/model.u3d", "model"));

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    /// <summary>
    /// 请求数据
    /// </summary>
    /// <returns></returns>
    IEnumerator GetText()
    {
        using (UnityWebRequest uwr = UnityWebRequest.Get("http://webar.hereto.cn/upload/UpLoad/upload/MYTool.txt"))
        {
            yield return uwr.SendWebRequest();

            if (uwr.result == UnityWebRequest.Result.ConnectionError || uwr.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.LogError(uwr.error);
            }
            else
            {
                // Get downloaded asset bundle
                //   AssetBundle bundle = DownloadHandlerAssetBundle.GetContent(uwr);

            }
            Debug.Log(uwr.downloadHandler.text);

        }
    }

    string AllModelgameObjectsInformations()
    {
        string infors = "{\"Data\":[";


        for (int i = 0; i < ModelgameObjects.Count; i++)
        {
            infors += PostStringJson(ModelgameObjects[i].name, ModelgameObjects[i].transform.position.ToString(), ModelgameObjects[i].transform.rotation.eulerAngles.ToString());
            if (ModelgameObjects.Count>0)
            {
                if (i != ModelgameObjects.Count - 1)
                {
                    infors = infors + ",";
                }
            }
          
            
        }
         infors = infors+ "]}";
        return infors;
    }
    public string PostStringJson(string Name, string Position, string Rotation)
    {
        string Json1 = "{\"name\":\"XX\",\"Position\":\"1381111111\",\"Rotation\":\"99:99\"}".Replace("XX", Name).Replace("1381111111", Position).Replace("99:99", Rotation);
        return Json1;
    }
    public void UpdateModelsInformationsToServer()
    {
        Debug.Log("上传数据");


        StartCoroutine(PostData(posturl, AllModelgameObjectsInformations(), "MYTool.txt"));
    }





 


    /// <summary>
    /// 上传文件到服务器上
    /// </summary>
    /// <param 服务器上传地址.php="PostURL"></param>
    /// <param 本地文件存储地址加后缀="DataPath"></param>
    /// <param 服务器保存文件名称加后缀="FileName"></param>
    /// <returns></returns>
    IEnumerator PostData(string PostURL, string Data, string FileName)
    {
        Debug.Log("准备上传");
        //  byte[] FileByte = File.ReadAllBytes(DataPath);
        Debug.Log(Data);
        byte[] FileByte = System.Text.Encoding.Default.GetBytes( Data);
        if (FileByte != null)
        {
            WWWForm form = new WWWForm();
            form.AddField("Name", FileName);  //Name是和服务器自检,文件保存名称和类型
            form.AddBinaryData("post", FileByte);       //这个post是和服务器自检的,不用修改        
            using (UnityWebRequest webRequest = UnityWebRequest.Post(PostURL, form)) //路径http://xxxxxxxx/UpLoad/UnityUpload.php,因为服务器里的接收在这个路径下
            {

                yield return webRequest.SendWebRequest();
                if (webRequest.result == UnityWebRequest.Result.ConnectionError || webRequest.result == UnityWebRequest.Result.ProtocolError)
                {
                    Debug.LogError(webRequest.error);
                }
                else
                {
                    Debug.Log("上传完成");
                    string text = webRequest.downloadHandler.text;
                    Debug.Log("服务器返回值" + text);//正确打印服务器返回值
                }
            }

        }
        else
        {
            yield return null;
            Debug.Log("FileByte is null");
        }


    }









    /// <summary>
    /// 保存数据到指定目录(安卓在storage/sdcard0/Android/data/包名/files)
    /// </summary>
    /// <param name="Data"></param>
    public void SaveData(string Data)
    {
        if (!File.Exists(Filepath))
        {
            File.CreateText(Filepath);
            Debug.Log("create 文件 OK!");

            //  SaveData(Data);
        }
        else
        {
            File.WriteAllText(Filepath, Data);
        }
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



    public void QuitEvent()
    {
        Application.Quit();
        Debug.Log("退出");
    }
}
