using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;
using LitJson;
using System.Text;
using System;
public class ToolForUploadModel : MonoBehaviour
{
    public long AreaBoxId = 1510136076975128578;//包围盒ID
    string FilePath = Application.streamingAssetsPath;
     public static string URL = "http://192.168.11.230:8020";
   // public static string URL = "https://webar.hereto.cn:17443";
    string PostURL_uploadOnlyFile = URL + "/trade/fdsfile/uploadOnlyFile";//单个文件上传并保存到附件表
    string PostURL_saveModel = URL + "/trade/show-model/saveModel";//保存模型信息
    [Header("文件名字集合信息")]
    public List<string> FileNames = new List<string>();//文件名称
    [Header("文件FileID集合信息")]
    public List<string> fileIDs = new List<string>();//fileIDs名称
    [Header("文件UTM集合信息")]
    public List<UTMVertoc3> uTMVertoc3s = new List<UTMVertoc3>();
    [Header("组合成的上传保存的数据信息集合")]
    public List<SaveModelInfor> saveModelInfors = new List<SaveModelInfor>();
    
    // 初始化
    void Awake()
    {
        //  StartCoroutine(Upload1(PostURL_uploadOnlyFile, FilePath, "diaosu0"));
    }
    // Start is called before the first frame update
    void Start()
    {
      //  StartCoroutine(Upload1(PostURL_uploadOnlyFile, FilePath, ""));
      
       //  StartCoroutine(UploadsaveModel(PostURL_saveModel));

    }





    void UploadAllSaveModelInformation()
    {
        for (int i = 0; i < saveModelInfors.Count; i++)
        {
            StartCoroutine(UploadsaveModel(PostURL_saveModel, saveModelInfors[i].fileId, saveModelInfors[i].name, saveModelInfors[i].uTMx, saveModelInfors[i].uTMy, saveModelInfors[i].uTMz));
        }
    }


    IEnumerator UploadsaveModel(string url,string _fileID,string _Name,string utmx,string utmy,string utmz)
    {
        string bodyJsonString = "{\"areaBoxId\":1510136076975128578,\"delIds\":[],\"items\":[{\"area\":0,\"fileId\":\"Fl-999999999999999999\",\"floor\":0,\"id\":0,\"name\":\"Re_Name\",\"posX\":\"\",\"posY\":\"\",\"posZ\":\"\",\"rotX\":\"\",\"rotY\":\"\",\"rotZ\":\"\",\"tag\":\"\",\"utmX\":\"Re_UTMX\",\"utmY\":\"Re_UTMY\",\"utmZ\":\"Re_UTMZ\"}]}";
        bodyJsonString = bodyJsonString.Replace("Fl-999999999999999999",_fileID).Replace("Re_UTMX", utmx).Replace("Re_UTMY", utmy).Replace("Re_UTMZ", utmz).Replace("Re_Name", _Name);
        var request = new UnityWebRequest(url, "POST");
        byte[] bodyRaw = Encoding.UTF8.GetBytes(bodyJsonString);
        request.uploadHandler = (UploadHandler)new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");

        yield return request.SendWebRequest();

        Debug.Log("Status Code: " + request.responseCode);


        if (request.result == UnityWebRequest.Result.ProtocolError || request.result == UnityWebRequest.Result.ConnectionError)
        {
            Debug.LogError(request.error);

        }
        else
        {
            Debug.Log(request.downloadHandler.text);

        }

    }



    int downNum, UpLoadAllFileNum, UpLoadAllFileNumAll = 0;
    float timejishi = 0f;
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.D))
        {
            downNum++;
            if (downNum == 1)
            {
                ReadFileName(FilePath + "/UnityToUTM.txt");
                Debug.Log("读取UTM名字信息");
            }
            if (downNum == 2)
            {
                UpLoadAllFile(PostURL_uploadOnlyFile, FilePath);
                Debug.Log("根据UTM信息上传全部文件");
            }
            if (downNum == 3)
            {
                ReadAllInforToSaveModelInfor();
                Debug.Log("保存所有信息到SaveModelInfor集合里面");
            }
            if (downNum == 4)
            {
                UploadAllSaveModelInformation();
                Debug.Log("批量上传到saveModel");
            }
         
        }

        if (UpLoadAllFileNum ==UpLoadAllFileNumAll&&UpLoadAllFileNumAll!=0)
        {
            Debug.Log("完成_根据UTM信息上传全部文件");
            UpLoadAllFileNum = UpLoadAllFileNumAll = 0;
        }
        else if (UpLoadAllFileNum < UpLoadAllFileNumAll && UpLoadAllFileNumAll != 0)
        {
            timejishi += Time.deltaTime;
            if (timejishi%2>0)
            {
                Debug.Log("上传中,请等待");
            }

           
        }
    }



    /// <summary>
    /// 根据目录列表全部上传到FastDFS
    /// </summary>
    /// <param name="PostUrl"></param>
    /// <param name="FilePath"></param>
    void UpLoadAllFile(string PostUrl, string FilePath )
    {

        for (int i = 0; i < FileNames.Count; i++)
        {
            UpLoadAllFileNumAll = FileNames.Count;
            StartCoroutine(Upload1(PostUrl, FilePath, FileNames[i]));
        }


    }

    /// <summary>
    /// 只传单个文件
    /// </summary>
    /// <param name="PostUrl"></param>
    /// <returns></returns>
    IEnumerator Upload1(string PostUrl, string FilePath, string FileName, string type = ".fbx")
    {
        Debug.Log(PostUrl + ": " + FilePath + "/" + FileName + type);
        byte[] FileByte = File.ReadAllBytes(FilePath + "/" + FileName + type);
        WWWForm form = new WWWForm();
        form.AddBinaryData("file", FileByte, FileName);
        UnityWebRequest Request = UnityWebRequest.Post(PostUrl, form);
        Request.SetRequestHeader("Blade-Auth", " ");
        Request.SetRequestHeader("Authorization", " ");
        Request.SetRequestHeader("Tenant-Id", " ");
        yield return Request.SendWebRequest();

        if (Request.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError(Request.error);
        }
        else
        {

            string receiveContent = Request.downloadHandler.text;
            //  Debug.Log("接收到数据!" + receiveContent);
            Debug.Log("接收到数据!");
            string _fileid = fileId(receiveContent);
            fileIDs.Add(_fileid);
            Debug.Log("添加到集合");
            
        }
        UpLoadAllFileNum++;
    }


    /// <summary>
    /// 解析转换成UTM的模型名字
    /// </summary>
    /// <param name="path"></param>
    void ReadFileName(string path)
    {
        JsonData jsondata = JsonMapper.ToObject(File.ReadAllText(path));
        for (int i = 0; i < jsondata[0].Count; i++)
        {

            FileNames.Add(jsondata[0][i]["name"].ToString());
            UTMVertoc3 _uTMVertoc3 = new UTMVertoc3();
            _uTMVertoc3 = StringParse(jsondata[0][i]["UTM"].ToString());
            uTMVertoc3s.Add(_uTMVertoc3);
        }
        Debug.Log("添加到名字列表_完成");
    }


    /// <summary>
    /// 解析出来FildID
    /// </summary>
    /// <param name="receiveJson"></param>
    /// <returns></returns>
    string fileId(string receiveJson)
    {
        return JsonMapper.ToObject(receiveJson)["data"]["fileId"].ToString();

    }
    /// <summary>
    /// fileID转Json
    /// </summary>
    /// <param name="name"></param>
    /// <param name="fileId"></param>
    /// <returns></returns>
    string ToJson(string name, string fileId)
    {
        string json = "{\"name\":\"Name\",\"fileId\":\"FileID\"}";
        json = json.Replace("Name", name).Replace("FileID", fileId);
        return json;
    }



    /// <summary>
    /// 保存名字和fileid到JSON本地
    /// </summary>
    void SaveNameAndFileIDEvent()
    {
        string saveString = "{\"NameAndFileID\":[";
        for (int i = 0; i < FileNames.Count; i++)
        {
            if (i == 0)
            {
                saveString += ToJson(FileNames[i], fileIDs[i]);
            }
            else
            {
                saveString += "," + ToJson(FileNames[i], fileIDs[i]);
            }
        }
        saveString += "]}";
        File.WriteAllText(FilePath + "/FilePath.txt", saveString);
        Debug.Log("保存完成,保存到了: " + FilePath + "/FilePath.txt");
    }


    /// <summary>
    /// 保存所有信息到SaveModelInfor集合里面
    /// </summary>
    void ReadAllInforToSaveModelInfor()
    {
        for (int i = 0; i < FileNames.Count; i++)
        {
            SaveModelInfor _saveModelInfor = new SaveModelInfor();
            _saveModelInfor.areaBoxId = AreaBoxId;

            _saveModelInfor.fileId = fileIDs[i];
            _saveModelInfor.name = FileNames[i];
            _saveModelInfor.uTMx = uTMVertoc3s[i].UTMx.ToString();
            _saveModelInfor.uTMy = uTMVertoc3s[i].UTMy.ToString();
            _saveModelInfor.uTMz = uTMVertoc3s[i].UTMz.ToString();
            saveModelInfors.Add(_saveModelInfor);
        }
    }



    UTMVertoc3 StringParse(string str)
    {
        str = str.Replace("(", "").Replace(")", "");
        string[] s = str.Split(',');
        UTMVertoc3 utm = new UTMVertoc3();
        utm.UTMx = double.Parse(s[0]);
        utm.UTMy = double.Parse(s[1]);
        utm.UTMz = double.Parse(s[2]);
        return utm;
    }




}






[System.Serializable]
public class SaveModelInfor
{
    public long areaBoxId;
    public string fileId;
    public int floor;
    public int id;
    public string name;
    public string posx;
    public string posy;
    public string posz;
    public string rotx;
    public string roty;
    public string rotz;
    public string tag;
    public string uTMx;
    public string uTMy;
    public string uTMz;

}

[System.Serializable]
public class UTMVertoc3
{
    public double UTMx;
    public double UTMy;
    public double UTMz;
}
