using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEditor;
public class UnityToUTM : MonoBehaviour
{

    //[Header("Offset-UTM50_X")]
    //public static double UTMOffset_X = 447500.000;
    //[Header("Offset-UTM50_Y")]
    //public static double UTMOffset_Y = 4418800.000;
    [Header("Offset-UTM50_X")]
    public static double UTMOffset_X = 615000.000;//太原万达
    [Header("Offset-UTM50_Y")]
    public static double UTMOffset_Y = 4202000.000;//太原万达
    [Header("是否保存到指定位置")]
    public bool IsOutPutSaveToAssets;
    [Header("保存位置")]
    public string SavePath = "";
    [Header("要转换的模型")]
    public List<GameObject> gameObjects;
    List<string> gameObjectsInformations;
    // 初始化


    private void Start()
    {
        gameObjectsInformations = new List<string>();
        SavePath = Application.streamingAssetsPath + "/UnityToUTM.txt";
        if (gameObjects.Count > 0)
        {
            UnityToUTMEvent();
        }

    }
    // Update is called once per frame
    void UnityToUTMEvent()
    {
        for (int i = 0; i < gameObjects.Count; i++)
        {
            gameObjectsInformations.Add(ToJson(gameObjects[i].name, (UTMOffset_X - (double)gameObjects[i].transform.localPosition.x).ToString("#0.00000"), (UTMOffset_Y - (double)gameObjects[i].transform.localPosition.z).ToString("#0.00000"), gameObjects[i].transform.localPosition.y.ToString(), gameObjects[i].transform.localScale.x.ToString(), gameObjects[i].transform.localScale.y.ToString(), gameObjects[i].transform.localScale.z.ToString(), gameObjects[i].transform.localRotation.x.ToString(), gameObjects[i].transform.localRotation.y.ToString(), gameObjects[i].transform.localRotation.z.ToString()));
        }
        if (!IsOutPutSaveToAssets)
        {

        }
        else
        {
            string saveString = "{\"data\":[";
            for (int i = 0; i < gameObjectsInformations.Count; i++)
            {
                if (i == 0)
                {
                    saveString += gameObjectsInformations[i];
                }
                else
                {
                    saveString += "," + gameObjectsInformations[i];
                }


            }
            saveString += "]}";
            File.WriteAllText(SavePath, saveString);
            Debug.Log("保存完成,保存到了: " + SavePath);
        }
    }



    string ToJson(string name, string UTMx, string UTMy, string UTMz)
    {
        string json = "{\"name\":\"Name\",\"UTM\":\"(x,y,z)\"}";
        json = json.Replace("Name", name).Replace("x", UTMx).Replace("y", UTMy).Replace("z", UTMz);
        return json;
    }
    /// <summary>
    /// UTM,Scale,Rotation 都替换
    /// </summary>
    /// <param name="name"></param>
    /// <param name="UTMx"></param>
    /// <param name="UTMy"></param>
    /// <param name="UTMz"></param>
    /// <param name="Scax"></param>
    /// <param name="Scay"></param>
    /// <param name="Scaz"></param>
    /// <param name="Rotx"></param>
    /// <param name="Roty"></param>
    /// <param name="Rotz"></param>
    /// <returns></returns>
    string ToJson(string name, string UTMx, string UTMy, string UTMz, string Scax, string Scay, string Scaz, string Rotx, string Roty, string Rotz)
    {
        string json = "{\"name\":\"Re_Name\",\"UTM\":\"(Re_UTMx,Re_UTMy,Re_UTMz)\",\"Scale\":\"(Re_Scax,Re_Scay,Re_Scaz)\",\"Retation\":\"(Re_Rotx,Re_Roty,Re_Rotz)\"}";
        json = json.Replace("Re_Name", name).Replace("Re_UTMx", UTMx).Replace("Re_UTMy", UTMy).Replace("Re_UTMz", UTMz);
        json = json.Replace("Re_Scax", Scax).Replace("Re_Scay", Scay).Replace("Re_Scaz", Scaz);
        json = json.Replace("Re_Rotx", Rotx).Replace("Re_Roty", Roty).Replace("Re_Rotz", Rotz);
        return json;
    }

    /// <summary>
    /// UTM,转unity pos
    /// </summary>
    /// <param name="UTM"></param>
    /// <returns></returns>

    public static Vector3 UTMToUnity(Vector3 UTM)
    {
        return new Vector3((float)UTMOffset_X - UTM.x, UTM.z, (float)UTMOffset_Y - UTM.y);
    }
    /// <summary>
    /// unity Pos 转UTM
    /// </summary>
    /// <param name="unitypos"></param>
    /// <returns></returns>
    public static Vector3 UnityTOUTM(Vector3 unitypos)
    {
        return new Vector3((float)(UTMOffset_X - (double)unitypos.x), (float)(UTMOffset_Y - (double)unitypos.z), unitypos.y);
    }

    /// <summary>
    /// 判断楼层
    /// </summary>
    /// <param name="F_tring"></param>
    /// <returns></returns>
    public static string Floor(string F_tring)
    {
        if (F_tring.Contains("B1"))
        {
            return "B1";
        }
        else if (F_tring.Contains("B2"))
        {
            return "B2";
        }
        else if (F_tring.Contains("B3"))
        {
            return "B3";
        }
        else if (F_tring.Contains("B4"))
        {
            return "B4";
        }
        else if (F_tring.Contains("1F"))
        {
            return "1F";
        }
        else if (F_tring.Contains("2F"))
        {
            return "2F";
        }
        else if (F_tring.Contains("3F"))
        {
            return "3F";
        }
        else if (F_tring.Contains("4F"))
        {
            return "4F";
        }
        else if (F_tring.Contains("5F"))
        {
            return "5F";
        }
        else if (F_tring.Contains("6F"))
        {
            return "6F";
        }
        else if (F_tring.Contains("7F"))
        {
            return "7F";
        }
        else if (F_tring.Contains("8F"))
        {
            return "8F";
        }
        else
        {
            return "???";
        }
    }
}
