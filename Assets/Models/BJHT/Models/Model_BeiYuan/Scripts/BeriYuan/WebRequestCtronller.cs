using LitJson;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
public class WebRequestCtronller : MonoBehaviour
{
    public InputField Name_inputField, Phone_inputField;
    public SetUserInformesPanel SetUserInformesPanel;
    string UserName = "";
    string UserPhone = "";
    string ShowJsonData = "";
    string NowTime;

    /// <summary>
    /// 信息编辑完成
    /// </summary>
    public void Name_inputFieldEdiorOver()
    {
        UserName = Name_inputField.text;
    }

    /// <summary>
    /// 名字编辑完成
    /// </summary>
    public void Phone_inputFieldEdiorOver()
    {
        UserPhone = Phone_inputField.text;
    }
    /// <summary>
    /// 上传输入信息
    /// </summary>
    public void ClickUpPostUserInfor()
    {
        if (UserName != "" && UserPhone != "")
        {
            Debug.Log("暂时没上传" + PostStringJson(UserName, UserPhone));
            //  StartCoroutine(Post(@"http://192.168.10.33:8083/api/guest", PostStringJson(UserName, UserPhone)));
            SaveData(PostStringJson(UserName, UserPhone,NowTime));
            Name_inputField.text = "";
            Phone_inputField.text = "";
            ShowJsonData = "{\"all\":["+ ReadData()+ "]}";
             Debug.LogError(ShowJsonData);
            UpdateXXPs(JsonMapper.ToObject(ShowJsonData));
            gameObject.SetActive(false);
        }


    }
    public string PostStringJson(string userName, string userPhone)
    {
        string Json1 = "{\"name\":\"XX\",\"phone\":\"1381111111\"}".Replace("XX", userName).Replace("1381111111", userPhone);
        return Json1;
    }
    public string PostStringJson(string userName, string userPhone,string TimeNow)
    {
        string Json1 = "{\"name\":\"XX\",\"phone\":\"1381111111\",\"TimeNow\":\"99:99\"}".Replace("XX", userName).Replace("1381111111", userPhone).Replace("99:99", TimeNow);
        return Json1;
    }
    // 初始化
    void Awake()
    {
        SetUserInformesPanel = GameObject.Find("QiandaoInformationPanel").GetComponent<SetUserInformesPanel>();
    }
    // Start is called before the first frame update
    void Start()
    {
        //  ShowJsonData = "{\"all\":[" + ReadData() + "]}";
        //  Debug.LogError(ShowJsonData);
        string TData = "{\"name\":\"张先生\",\"phone\":\"13812345678\",\"TimeNow\":\"02:12\"}";
        SaveDataStary(TData);
    }

    public void SaveDataStary(string Data)
    {

#if UNITY_ANDROID && !UNITY_EDITOR
      //android端的保存路径
        string filepath = Application.persistentDataPath + @"/UserPostData.txt";
#elif (UNITY_IOS || UNITY_IPHONE) && !UNITY_EDITOR
         string filepath = Application.persistentDataPath + @"/UserPostData.txt";
#else
        string filepath = Application.streamingAssetsPath + @"/UserPostData.txt";
#endif


        if (!File.Exists(filepath))
        {
            File.CreateText(filepath);
            Debug.Log("create 文件 OK!");
            StartCoroutine(WriterWatier(filepath, Data));
             
          
           
        }
        else
        {

            

            //  File.WriteAllText(filepath, Data);
        }
    }

    IEnumerator WriterWatier(string filepath, string Data)
    {
        yield return new WaitForSeconds(0.2f);
        File.WriteAllText(filepath, Data);
    }
    public void Update()
    {
        NowTime = DateTime.Now.Hour.ToString() + ":" + DateTime.Now.Minute.ToString();
    }

    IEnumerator Post(string url, string bodyJsonString)
    {
        Debug.LogError("开始");
       
        var request = new UnityWebRequest(url, "POST");
      //  request.chunkedTransfer = false;
       // request.useHttpContinue = false;
        byte[] bodyRaw = Encoding.UTF8.GetBytes(bodyJsonString);
        request.uploadHandler = (UploadHandler)new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");
      
        yield return request.SendWebRequest();

        Debug.LogError("Status Code: " + request.responseCode);


        if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.LogError(request.error);

        }
        else
        {
            Debug.LogError(request.downloadHandler.text);

        }
        Debug.LogError("结束");

        gameObject.SetActive(false);

    }



    /// <summary>
    /// 请求数据
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

        }
    }




    /// <summary>
    /// 保存数据到指定目录(安卓在storage/sdcard0/Android/data/包名/files)
    /// </summary>
    /// <param name="Data"></param>
    public void SaveData(string Data)
    {

#if UNITY_ANDROID && !UNITY_EDITOR
      //android端的保存路径
        string filepath = Application.persistentDataPath + @"/UserPostData.txt";
#elif (UNITY_IOS || UNITY_IPHONE) && !UNITY_EDITOR
         string filepath = Application.persistentDataPath + @"/UserPostData.txt";
#else
        string filepath = Application.streamingAssetsPath + @"/UserPostData.txt";
#endif


        if (!File.Exists(filepath))
        {
            File.CreateText(filepath);
            Debug.Log("create 文件 OK!");

            File.WriteAllText(filepath, Data);
        }
        else
        {

            File.WriteAllText(filepath, Data+","+"\n"+ ReadData());

          //  File.WriteAllText(filepath, Data);
        }
    }



    public string ReadData()
    {
#if UNITY_ANDROID && !UNITY_EDITOR
      //android端的保存路径
        string filepath = Application.persistentDataPath + @"/UserPostData.txt";
#elif (UNITY_IOS || UNITY_IPHONE) && !UNITY_EDITOR
         string filepath = Application.persistentDataPath + @"/UserPostData.txt";
#else
        string filepath = Application.streamingAssetsPath + @"/UserPostData.txt";
#endif
        if (!File.Exists(filepath))
        {
            return "{}";
        }
        else
        {
            return File.ReadAllText(filepath);
        }
    }


  
    public void UpdateXXPs(JsonData data)
    {

        if (data[0].Count<12)
        {
            for (int i = 0; i < data[0].Count; i++)
            {
                SetUserInformesPanel.XXPs[i].GetComponent<UINameXXP>().NameText.text = data[0][i]["name"].ToString();
                SetUserInformesPanel.XXPs[i].GetComponent<UINameXXP>().PhoneText.text = data[0][i]["phone"].ToString();
                SetUserInformesPanel.XXPs[i].GetComponent<UINameXXP>().TimeText.text = data[0][i]["TimeNow"].ToString();
            }
            SetUserInformesPanel.textNum.text = "12";
        }
        else
        {
            SetUserInformesPanel.textNum.text = data[0].Count.ToString();
            for (int i = 0; i < SetUserInformesPanel.XXPs.Count; i++)
            {
                SetUserInformesPanel.XXPs[i].GetComponent<UINameXXP>().NameText.text = data[0][i]["name"].ToString();
                SetUserInformesPanel.XXPs[i].GetComponent<UINameXXP>().PhoneText.text = data[0][i]["phone"].ToString();
                SetUserInformesPanel.XXPs[i].GetComponent<UINameXXP>().TimeText.text = data[0][i]["TimeNow"].ToString();
            }
        }
        
    }
}
