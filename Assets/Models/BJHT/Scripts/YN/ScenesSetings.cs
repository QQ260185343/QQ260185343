using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
public static class ScenesSetingDates 
{
    /// <summary>
    /// 是否显示LOG
    /// </summary>
    public static bool IsShowLog = true;



}



public class ScenesSetings : MonoBehaviour
{

    public Button buttonSetlog;

    private void Awake()
    {

#if UNITY_ANDROID && !UNITY_EDITOR
      //android端的保存路径
        string filepath = Application.persistentDataPath + @"/ScenesSeting.txt";
#elif (UNITY_IOS || UNITY_IPHONE) && !UNITY_EDITOR
         string filepath = Application.persistentDataPath + @"/ScenesSeting.txt";
#else
        string filepath = Application.streamingAssetsPath + @"/ScenesSeting.txt";
#endif


        if (!File.Exists(filepath))
        {
            File.CreateText(filepath);
            Debug.Log("create 文件 OK!");

            StartCoroutine(waitWrite(filepath, "true"));
        }
        buttonSetlog.onClick.AddListener(() => { SetShowLogEvent(); });
        ScenesSetingDates.IsShowLog = bool.Parse(ReadData()) ;
        if (ScenesSetingDates.IsShowLog)
        {
            buttonSetlog.GetComponentInChildren<Text>().text = "当前状态:显示调试信息";
            
        }
        else
        {
            buttonSetlog.GetComponentInChildren<Text>().text = "当前状态:不显示调试信息";
           
        }
    }
    public void SetShowLogEvent()
    {
        ScenesSetingDates.IsShowLog = !ScenesSetingDates.IsShowLog;
        if (ScenesSetingDates.IsShowLog)
        {
            buttonSetlog.GetComponentInChildren<Text>().text = "当前状态:显示调试信息";
            SaveData("true");
        }
        else
        {
            buttonSetlog.GetComponentInChildren<Text>().text = "当前状态:不显示调试信息";
            SaveData("false");
        }
    }
    public void SaveData(string Data)
    {

#if UNITY_ANDROID && !UNITY_EDITOR
      //android端的保存路径
        string filepath = Application.persistentDataPath + @"/ScenesSeting.txt";
#elif (UNITY_IOS || UNITY_IPHONE) && !UNITY_EDITOR
         string filepath = Application.persistentDataPath + @"/ScenesSeting.txt";
#else
        string filepath = Application.streamingAssetsPath + @"/ScenesSeting.txt";
#endif


        if (!File.Exists(filepath))
        {
            File.CreateText(filepath);
            Debug.Log("create 文件 OK!");

            StartCoroutine(waitWrite(filepath, Data));
        }
        else
        {

            File.WriteAllText(filepath, Data);

            //  File.WriteAllText(filepath, Data);
        }
    }

   IEnumerator  waitWrite(string filepath,string Data)
    {
        yield return new WaitForSeconds(0.2f);
        File.WriteAllText(filepath, Data);
    }

    public string ReadData()
    {
#if UNITY_ANDROID && !UNITY_EDITOR
      //android端的保存路径
        string filepath = Application.persistentDataPath + @"/ScenesSeting.txt";
#elif (UNITY_IOS || UNITY_IPHONE) && !UNITY_EDITOR
         string filepath = Application.persistentDataPath + @"/ScenesSeting.txt";
#else
        string filepath = Application.streamingAssetsPath + @"/ScenesSeting.txt";
#endif
        if (!File.Exists(filepath))
        {
            return "true";
        }
        else
        {
            return File.ReadAllText(filepath);
        }
    }

}