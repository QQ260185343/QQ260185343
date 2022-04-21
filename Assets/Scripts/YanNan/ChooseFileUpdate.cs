using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Runtime.InteropServices;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.IO;
using System.Text;
using System.Data.Common;
using System.Security.Cryptography;
using UpLoad;

public class ChooseFileUpdate : MonoBehaviour
{
    public InputField inputFieldUrl, inputFieldName, OutInputFieldPath;
    public Text ServerURLText, DebText;
    //  string ServerURL = "http://webar.hereto.cn/upload/UpLoad/UnityUpload.php";
  //  string ServerURL = "http://82.157.140.117:80";
  //string ServerURL = "http://file.hereto.cn/WWW/UpLoad/UnityUpload.php";
      string ServerURL = "https://webar.hereto.cn/WWW/UpLoad/OtherUpload.php";
    public Button buttonChoose, buttonUpDate;
    // 初始化
    void Awake()
    {
        ServerURL = File.ReadAllText(Application.streamingAssetsPath + "/UpdatePath/PathURL.txt");
      //  string a = "M00/00/00/wKgzgFnkaXqAIfXyAAEoRmXZPp878.jpegHandFastDFSToken1508141521";
      // Debug.Log(MD5Encrypt(a));
        buttonChoose.onClick.AddListener(() => { ChooseButtonEvent(); });
        buttonUpDate.onClick.AddListener(() => { UpdateModelsInformationsToServer(); });
    }
    // Start is called before the first frame update
    void Start()
    {
      //  StartCoroutine(GetText());
        ServerURLText.text = "上传地址: " + ServerURL;
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void UpdateModelsInformationsToServer()
    {
        Debug.Log("上传数据");

        if (inputFieldUrl.text != "")
        {
            DebText.color = Color.green;
            DebText.text = "上传数据";

          //  StartCoroutine(PostData(ServerURL+"/"+ "M00/00/00/wKgzgFnkaXqAIfXyAAEoRmXZPp878.jpeg" + "?"+getToken("M00/00/00/wKgzgFnkaXqAIfXyAAEoRmXZPp878.jpeg"), inputFieldUrl.text, inputFieldName.text));
        //    StartCoroutine(UpLoad(ServerURL + "/" + "M00/00/00/wKgzgFnkaXqAIfXyAAEoRmXZPp878.jpeg" + "?" + getToken("M00/00/00/wKgzgFnkaXqAIfXyAAEoRmXZPp878.jpeg"), inputFieldUrl.text, inputFieldName.text));
             StartCoroutine(PostData(ServerURL, inputFieldUrl.text, inputFieldName.text));
        }
        else
        {
            DebText.color = Color.red;
            DebText.text = "数据为空";
        }

    }
  IEnumerator UpLoad(string url,string inputFieldUrl,string inputFieldName)
    {
        Debug.Log(url);
        yield return new WaitForSeconds(0.1f);
        //路径来自 "ftp://192.168.xxx.136/data/uploadFile/a.zip"
        UpLoadFiles.UploadFiles(url, inputFieldUrl, inputFieldName);

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
        Debug.Log("准备上传:"+ PostURL);
        byte[] FileByte = File.ReadAllBytes(Data);
        // Debug.Log(Data);
        //  byte[] FileByte = System.Text.Encoding.Default.GetBytes(Data);
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
                    DebText.text = webRequest.error;
                }
                else
                {
                    DebText.text = "上传完成";
                    Debug.Log("上传完成");
                    string text = webRequest.downloadHandler.text;

                    Debug.Log("服务器返回值" + text);//正确打印服务器返回值
                    OutInputFieldPath.text = ServerURL.Replace("UnityUpload.php", "upload/").Replace("Otherupload.php", "Otherupload/") + inputFieldName.text;
                }
            }

        }
        else
        {
            yield return null;
            Debug.Log("FileByte is null");
        }


    }
    string mainOpenDataPath = File.ReadAllText(Application.streamingAssetsPath + "/UpdatePath/mainOpenDataPath.txt");

    public void ChooseButtonEvent()
    {
        OpenFileName openFileName = new OpenFileName();
        openFileName.structSize = Marshal.SizeOf(openFileName);
        // openFileName.filter = "Excel文件(*.xlsx)\0*.xlsx";
        openFileName.file = new string(new char[256]);
        openFileName.maxFile = openFileName.file.Length;
        openFileName.fileTitle = new string(new char[64]);
        openFileName.maxFileTitle = openFileName.fileTitle.Length;
        //    openFileName.initialDir = Application.streamingAssetsPath.Replace('/', '\\');//默认路径
        openFileName.initialDir = mainOpenDataPath;//默认路径
        openFileName.title = "窗口标题";
        openFileName.flags = 0x00080000 | 0x00001000 | 0x00000800 | 0x00000008;

        if (LocalDialog.GetSaveFileName(openFileName))
        {
            Debug.Log(openFileName.file);
            inputFieldUrl.text = openFileName.file.ToString();
            mainOpenDataPath = openFileName.file;
            File.WriteAllText(Application.streamingAssetsPath+ "/UpdatePath/mainOpenDataPath.txt", openFileName.file);
            Debug.Log(openFileName.fileTitle);
            inputFieldName.text = openFileName.fileTitle.ToString();
        }


    }

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

    /// 获取当前本地时间戳
    /// </summary>
    /// <returns></returns>      
    public static long GetCurrentTimeUnix()
    {
        var timeSpan = (DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0));
        return (long)timeSpan.TotalSeconds;

    }

    /**
     * 获取访问服务器的token，拼接到地址后面
     *
     * @param filepath 文件路径 group1/M00/00/00/wKgzgFnkTPyAIAUGAAEoRmXZPp876.jpeg
     * @param httpSecretKey 密钥
     * @return 返回token，如： token=078d370098b03e9020b82c829c205e1f&ts=1508141521
     */
    public static String getToken(String filepath,string httpScretKey= "FASTDFS1234567890")
    {
        // unix seconds
        int ts =(int) GetCurrentTimeUnix();
        // token
        String token = "null";
        try
        {

          //  token = filepath+ ts+ httpSecretKey;
          //  token = ProtoCommon(filepath)+ ts;
          token = MD5Encrypt(filepath+ts+ httpScretKey);
        }
        //catch (UnsupportedEncodingException e)
        //{
        //    e.printStackTrace();
        //}
        //catch (NoSuchAlgorithmException e)
        //{
        //    e.printStackTrace();
        //}
        catch (DbException e)
        {
            e.ToString();
        }

        StringBuilder sb = new StringBuilder();
        sb.Append("token=").Append(token);
        sb.Append("&ts=").Append(ts);
        Debug.Log(sb.ToString());
        return sb.ToString();
    }
  //static  string ProtoCommon(string Data)
  //  {
  //      return MD5Encrypt(Data);
  //  }
    public static string MD5Encrypt(string str)
    {
        MD5CryptoServiceProvider md5Hasher = new MD5CryptoServiceProvider();
        byte[] hashedDataBytes;
        hashedDataBytes = md5Hasher.ComputeHash(Encoding.UTF8.GetBytes(str));
        StringBuilder tmp = new StringBuilder();
        foreach (byte i in hashedDataBytes)
        {
           tmp.Append(i.ToString("x2"));
        }
        return tmp.ToString();
    }
}

[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
public class OpenFileName
{
    public int structSize = 0;
    public IntPtr dlgOwner = IntPtr.Zero;
    public IntPtr instance = IntPtr.Zero;
    public String filter = null;
    public String customFilter = null;
    public int maxCustFilter = 0;
    public int filterIndex = 0;
    public String file = null;
    public int maxFile = 0;
    public String fileTitle = null;
    public int maxFileTitle = 0;
    public String initialDir = null;
    public String title = null;
    public int flags = 0;
    public short fileOffset = 0;
    public short fileExtension = 0;
    public String defExt = null;
    public IntPtr custData = IntPtr.Zero;
    public IntPtr hook = IntPtr.Zero;
    public String templateName = null;
    public IntPtr reservedPtr = IntPtr.Zero;
    public int reservedInt = 0;
    public int flagsEx = 0;

}
public class LocalDialog
{
    //链接指定系统函数       打开文件对话框
    [DllImport("Comdlg32.dll", SetLastError = true, ThrowOnUnmappableChar = true, CharSet = CharSet.Auto)]
    public static extern bool GetOpenFileName([In, Out] OpenFileName ofn);
    public static bool GetOFN([In, Out] OpenFileName ofn)
    {
        return GetOpenFileName(ofn);
    }

    //链接指定系统函数        另存为对话框
    [DllImport("Comdlg32.dll", SetLastError = true, ThrowOnUnmappableChar = true, CharSet = CharSet.Auto)]
    public static extern bool GetSaveFileName([In, Out] OpenFileName ofn);
    public static bool GetSFN([In, Out] OpenFileName ofn)
    {
        return GetSaveFileName(ofn);
    }



}
