using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using LitJson;
public class FastDFS : MonoBehaviour
{
    public static string URL = "http://192.168.11.177:8020";
    string postUrl = URL + "/trade/fdsfile/uploadOnlyFile";//单个文件上传
    string postURLs = URL + "/fdsfile/uploadOnlyFiles";//多个文件上传
    string postAndRelationTableURL = URL + "/trade/fdsfile/uploadFile";//单个文件上传并关联关系表地址
    string postAndRelationTableURLs = URL + "/trade/fdsfile/uploadFiles";//多个文件上传并关联关系表地址
    string detailUsingPOSTURL = URL + "/trade/area-box/detail";




    void Start()
    {
        //  StartCoroutine(getWebImage("http://119.3.176.115/group1/M00/00/01/dwOwc2JHv26AEeKCAATA9zygKhs480.png?token=0d0a3547e6d9398e64d9932007811ffe&ts=1648869231"));
        //   StartCoroutine(Upload1(postUrl));
        //  StartCoroutine(Upload1(postURLs));
        // StartCoroutine(Upload1AndRelationTable(postAndRelationTableURL));
        //    StartCoroutine(Upload1AndRelationTables(postAndRelationTableURLs));
        StartCoroutine(detailUsingPOST(detailUsingPOSTURL));
    }







    //多个文件上传按照UTM坐标来做

    IEnumerator Upload1AndRelationTablesForWEBAR_UTM(string url, string file = "", string id = "", string fileld = "", string fileIds = "", string localFilePath = "", string regexType = "", string showThumbn = "", string sourceType = "", string taskId = "")
    {
        Debug.Log(url);
        byte[] FileByte = File.ReadAllBytes(Application.streamingAssetsPath + "/CG.png");
        WWWForm form = new WWWForm();

        form.AddField("id", "123");
        form.AddField("taskId", "1223234");
        form.AddBinaryData("files", FileByte, "CG1.png");
        form.AddBinaryData("files", FileByte, "CG2.png");
        UnityWebRequest Request = UnityWebRequest.Post(url, form);
        Request.SetRequestHeader("Blade-Auth", " ");
        Request.SetRequestHeader("Authorization", " ");
        Request.SetRequestHeader("Tenant-Id", " ");
        yield return Request.SendWebRequest();

        if (Request.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(Request.error);
        }
        else
        {
            string receiveContent = Request.downloadHandler.text;
            Debug.Log("Form upload complete!" + receiveContent);
        }
    }



















    /// <summary>
    /// //多个文件上传并关联关系表
    /// </summary>
    /// <param name="url"></param>
    /// <param name="file"></param>
    /// <param name="id"></param>
    /// <param name="fileld"></param>
    /// <param name="fileIds"></param>
    /// <param name="localFilePath"></param>
    /// <param name="regexType"></param>
    /// <param name="showThumbn"></param>
    /// <param name="sourceType"></param>
    /// <param name="taskId"></param>
    /// <returns></returns>
    IEnumerator Upload1AndRelationTables(string url, string file = "", string id = "", string fileld = "", string fileIds = "", string localFilePath = "", string regexType = "", string showThumbn = "", string sourceType = "", string taskId = "")
    {
        Debug.Log(url);
        byte[] FileByte = File.ReadAllBytes(Application.streamingAssetsPath + "/CG.png");
        WWWForm form = new WWWForm();

        form.AddField("id", "123");
        form.AddField("taskId", "1223234");
        form.AddBinaryData("files", FileByte, "CG1.png");
        form.AddBinaryData("files", FileByte, "CG2.png");
        UnityWebRequest Request = UnityWebRequest.Post(url, form);
        Request.SetRequestHeader("Blade-Auth", " ");
        Request.SetRequestHeader("Authorization", " ");
        Request.SetRequestHeader("Tenant-Id", " ");
        yield return Request.SendWebRequest();

        if (Request.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(Request.error);
        }
        else
        {
            string receiveContent = Request.downloadHandler.text;
            Debug.Log("Form upload complete!" + receiveContent);
        }
    }
    /// <summary>
    /// 单个文件上传并关联关系表
    /// </summary>
    /// <param name="url"></param>
    /// <param name="file"></param>
    /// <param name="id"></param>
    /// <param name="fileld"></param>
    /// <param name="fileIds"></param>
    /// <param name="localFilePath"></param>
    /// <param name="regexType"></param>
    /// <param name="showThumbn"></param>
    /// <param name="sourceType"></param>
    /// <param name="taskId"></param>
    /// <returns></returns>
    IEnumerator Upload1AndRelationTable(string url, string file = "", string id = "", string fileld = "", string fileIds = "", string localFilePath = "", string regexType = "", string showThumbn = "", string sourceType = "", string taskId = "")
    {
        Debug.Log(url);
        byte[] FileByte = File.ReadAllBytes(Application.streamingAssetsPath + "/CG.png");
        WWWForm form = new WWWForm();

        form.AddField("id", "123");
        form.AddField("taskId", "1223234");
        form.AddBinaryData("file", FileByte, "CG1.png");
        UnityWebRequest Request = UnityWebRequest.Post(url, form);
        Request.SetRequestHeader("Blade-Auth", " ");
        Request.SetRequestHeader("Authorization", " ");
        Request.SetRequestHeader("Tenant-Id", " ");
        yield return Request.SendWebRequest();

        if (Request.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(Request.error);
        }
        else
        {
            string receiveContent = Request.downloadHandler.text;
            Debug.Log("Form upload complete!" + receiveContent);
        }
    }
    /// <summary>
    /// 只传单个文件
    /// </summary>
    /// <param name="url"></param>
    /// <returns></returns>
    IEnumerator Upload1(string url)
    {
        Debug.Log(url);
        byte[] FileByte = File.ReadAllBytes(Application.streamingAssetsPath + "/CG.png");
        WWWForm form = new WWWForm();
        form.AddBinaryData("file", FileByte, "CG1.png");
        UnityWebRequest Request = UnityWebRequest.Post(url, form);
        Request.SetRequestHeader("Blade-Auth", " ");
        Request.SetRequestHeader("Authorization", " ");
        Request.SetRequestHeader("Tenant-Id", " ");
        yield return Request.SendWebRequest();

        if (Request.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(Request.error);
        }
        else
        {
            string receiveContent = Request.downloadHandler.text;
            Debug.Log("Form upload complete!" + receiveContent);
        }
    }
    /// <summary>
    /// 传多个文件
    /// </summary>
    /// <param name="url"></param>
    /// <returns></returns>
    IEnumerator Uploads(string url)
    {
        Debug.Log(url);
        byte[] FileByte = File.ReadAllBytes(Application.streamingAssetsPath + "/CG.png");
        WWWForm form = new WWWForm();
        form.AddBinaryData("files", FileByte, "CG.png");
        form.AddBinaryData("files", FileByte, "CG1.png");
        UnityWebRequest request = UnityWebRequest.Post(url, form);
        request.SetRequestHeader("Blade-Auth", " ");
        request.SetRequestHeader("Authorization", " ");
        request.SetRequestHeader("Tenant-Id", " ");
        yield return request.SendWebRequest();

        if (request.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(request.error);
        }
        else
        {
            string receiveContent = request.downloadHandler.text;
            Debug.Log("Form upload complete!" + receiveContent);
        }
    }

    /// <summary>
    /// 区域包围盒接口详情Post
    /// </summary>
    /// <param name="url"></param>
    /// <returns></returns>
    IEnumerator detailUsingPOST(string url)
    {
        Debug.Log(url);

        WWWForm form = new WWWForm();
        form.AddField("id", "123456");
        UnityWebRequest Request = UnityWebRequest.Post(url, form);
        Request.SetRequestHeader("Blade-Auth", " ");
        Request.SetRequestHeader("Authorization", " ");
        Request.SetRequestHeader("Tenant-Id", " ");
        yield return Request.SendWebRequest();

        if (Request.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(Request.error);
        }
        else
        {
            string receiveContent = Request.downloadHandler.text;
            Debug.Log("Form upload complete!" + receiveContent);
        }
    }



    IEnumerator Post(string url)
    {

        byte[] FileByte = File.ReadAllBytes(Application.streamingAssetsPath + "/CG.png");
        UnityWebRequest request = UnityWebRequest.Post(url, "");

        request.SetRequestHeader("Blade-Auth", "Basic ");
        request.SetRequestHeader("Authorization", "Basic ");
        request.SetRequestHeader("Tenant-Id", "Basic ");
        request.SetRequestHeader("Authorization", "Basic c2FiZXI6c2FiZXJfc2VjcmV0");
        yield return request.SendWebRequest();
        if (request.result == UnityWebRequest.Result.ProtocolError || request.result == UnityWebRequest.Result.ConnectionError)
        {
            Debug.LogError(request.error);
        }
        else
        {
            string receiveContent = request.downloadHandler.text;
            Debug.Log(receiveContent);
        }
    }


    public Image image;
    IEnumerator getWebImage(string PicURL)
    {
        yield return null;
        UnityWebRequest uwr = UnityWebRequestTexture.GetTexture(PicURL);
        yield return uwr.SendWebRequest();
        if (uwr.result == UnityWebRequest.Result.ConnectionError || uwr.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.LogError(uwr.error);
        }
        else
        {
            image.sprite = Sprite.Create(DownloadHandlerTexture.GetContent(uwr), new Rect(0, 0, DownloadHandlerTexture.GetContent(uwr).width, DownloadHandlerTexture.GetContent(uwr).height), Vector2.zero); ;
            //  Debug.Log("OK");
        }
    }



}
