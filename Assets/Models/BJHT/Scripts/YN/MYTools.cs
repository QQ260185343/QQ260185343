using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;
using LitJson;
/// <summary>
/// 工具包
/// </summary>
public class MYTools : MonoBehaviour
{


    string posturl = "http://192.168.10.148/UpLoad/UnityUpload.php";
#if UNITY_ANDROID && !UNITY_EDITOR
      //android端的保存路径
        string Filepath = Application.persistentDataPath + @"/MYTool.txt";
#elif (UNITY_IOS || UNITY_IPHONE) && !UNITY_EDITOR
         string Filepath = Application.persistentDataPath + @"/MYTool.txt";
#else
    string Filepath = Application.streamingAssetsPath + @"/MYTool.txt";
#endif



    public void ChooseToServer()
    {
        StartCoroutine(PostData(posturl, Filepath, "测试.txt"));
    }
    // 初始化
    void Awake()
    {

    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {


    }
    Vector3 screenSpace;
    Vector3 offset;
    void OnMouseDown()
    {
        screenSpace = Camera.main.WorldToScreenPoint(transform.position);
        offset = transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenSpace.z));
    }

    void OnMouseDrag()
    {
        Vector3 curScreenSpace = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenSpace.z);
        Vector3 curPosition = Camera.main.ScreenToWorldPoint(curScreenSpace) + offset;
        transform.position = curPosition;
    }
    public void ButtonEvent()
    {
        SaveData(Readappxyz());
        // StartCoroutine(PostData(posturl, Filepath, "MYTool.txt"));
        StartCoroutine(GetText());
    }





    /// <summary>
    /// 加载一个服务器的资源
    /// </summary>
    /// <returns></returns>
    IEnumerator StartWEB()
    {
        var uwr = UnityWebRequestAssetBundle.GetAssetBundle("http://webar.hereto.cn/model/konglong.u3d");
        yield return uwr.SendWebRequest();

        // Get an asset from the bundle and instantiate it.
        AssetBundle bundle = DownloadHandlerAssetBundle.GetContent(uwr);
        var loadAsset = bundle.LoadAssetAsync<GameObject>("trex_v3");
        yield return loadAsset;
        GameObject prefab = loadAsset.asset as GameObject;
        Instantiate(prefab, new Vector3(2922f, 77f, -0.9f), Quaternion.identity);

    }

    /// <summary>
    /// 上传文件到服务器上
    /// </summary>
    /// <param 服务器上传地址.php="PostURL"></param>
    /// <param 本地文件存储地址加后缀="DataPath"></param>
    /// <param 服务器保存文件名称加后缀="FileName"></param>
    /// <returns></returns>
    IEnumerator PostData(string PostURL, string DataPath, string FileName)
    {
        Debug.Log("准备上传");
        byte[] FileByte = File.ReadAllBytes(DataPath);
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
                    Debug.Log(webRequest.error);
                }
                else
                {
                    Debug.Log("上传完成");
                    //string text = webRequest.downloadHandler.text;
                    //Debug.Log("服务器返回值" + text);//正确打印服务器返回值
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
    /// 读取场景所有的XYZ信息
    /// </summary>
    public string Readappxyz()
    {
        string Axyz = "";
        foreach (GameObject objj in Object.FindObjectsOfType(typeof(GameObject)))
        {
            Axyz += objj.transform.name + " P: " + objj.transform.position + " R:" + objj.transform.rotation.eulerAngles + "\n";
        }
        return Axyz;
    }

    /// <summary>
    /// 保存数据到指定目录(安卓在storage/sdcard0/Android/data/包名/files)
    /// </summary>
    /// <param name="Data"></param>
    public void SaveData(string Data)
    {

#if UNITY_ANDROID && !UNITY_EDITOR
      //android端的保存路径
        string filepath = Application.persistentDataPath + @"/MYTool.txt";
#elif (UNITY_IOS || UNITY_IPHONE) && !UNITY_EDITOR
         string filepath = Application.persistentDataPath + @"/MYTool.txt";
#else
        string filepath = Application.streamingAssetsPath + @"/MYTool.txt";
#endif


        if (!File.Exists(filepath))
        {
            File.CreateText(filepath);
            Debug.Log("create 文件 OK!");

            SaveData(Data);
        }
        else
        {
            File.WriteAllText(filepath, Data);
        }
    }


    public string ReadData()
    {
#if UNITY_ANDROID && !UNITY_EDITOR
      //android端的保存路径
        string filepath = Application.persistentDataPath + @"/MYTool.txt";
#elif (UNITY_IOS || UNITY_IPHONE) && !UNITY_EDITOR
         string filepath = Application.persistentDataPath + @"/MYTool.txt";
#else
        string filepath = Application.streamingAssetsPath + @"/MYTool.txt";
#endif
        if (!File.Exists(filepath))
        {
            return "没有数据";
        }
        else
        {
            return File.ReadAllText(filepath);
        }
    }








    /// <summary>
    /// 加载一个服务器的资源
    /// </summary>
    /// <returns></returns>
    IEnumerator GetText()
    {
        using (UnityWebRequest uwr = UnityWebRequest.Get("http://192.168.10.240/index.html"))
        {
            yield return uwr.SendWebRequest();

            if (uwr.result == UnityWebRequest.Result.ConnectionError || uwr.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.Log(uwr.error);
            }
            else
            {
                // Get downloaded asset bundle
                //   AssetBundle bundle = DownloadHandlerAssetBundle.GetContent(uwr);

            }
            Debug.LogError(uwr.downloadHandler.text);
            BeJsonFair(JsonMapper.ToObject(uwr.downloadHandler.text));
        }
    }






    public void BeJsonFair(JsonData data)//解析数据
    {

        ObjectPosition.x = float.Parse(data[0].ToString());
        ObjectPosition.y = float.Parse(data[1].ToString());
        ObjectPosition.z = float.Parse(data[2].ToString());
        ObjectRotation.x = float.Parse(data[3].ToString());
        ObjectRotation.y = float.Parse(data[4].ToString());
        ObjectRotation.z = float.Parse(data[5].ToString());
        Model1.transform.position = ObjectPosition;
        Model1.transform.rotation = Quaternion.Euler(ObjectRotation);
    }

    public GameObject Model1;

    public Vector3 ObjectPosition, ObjectRotation;

}

