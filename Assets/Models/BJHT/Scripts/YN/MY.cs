using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Xml;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class MY : MonoBehaviour
{


    string recvStr;
    string UDPClientIP;
    string editString = "hello wolrd";
    Socket socket;
    EndPoint serverEnd;
    IPEndPoint ipEnd;

    string sendStr;
    byte[] recvData;
    byte[] sendData;
    int recvLen = 0;
    Thread connectThread;


    public GameObject Cube;
    public Text Ttext;
    public Text Debtext;
    string abx = "";
    // 初始化
    void Awake()
    {

    }
    // Start is called before the first frame update
    void Start()
    {
      //  StartCoroutine(StartWEB());
        ScoketStart();
    }

    // Update is called once per frame
    void Update()
    {

    }

    /// <summary>
    /// 读取场景所有的XYZ信息
    /// </summary>
    public string readappxyz()
    {
        string Axyz = "";
        foreach (GameObject objj in UnityEngine.Object.FindObjectsOfType(typeof(GameObject)))
        {
            Axyz += objj.transform.name +"Position"+ objj.transform.position+"Rotation: " + objj.transform.rotation.eulerAngles + "\n";
        }
        return Axyz;
    }


    /// <summary>
    /// 获取所有场景数据信息
    /// </summary>
    public void showXYZ()
    {
        abx = "";
        readappxyz();
        Ttext.text = "位置X" + Cube.transform.position.ToString();

        SocketSend(abx);
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
    /// 读写到安卓持久化文件位置
    /// </summary>
/*
    public Text TextRead, TextWrite;

    public void SaveIp(string iP)
    {
        //android端的保存路径
        string filepath = Application.persistentDataPath + @"/IP.txt";
        if (!File.Exists(filepath))
        {
            XmlDocument xmlDoc = new XmlDocument();
            XmlNode docNode = xmlDoc.CreateXmlDeclaration("1.0", "UTF-8", null);
            //创建root节点，也就是最上一层节点
            XmlElement root = xmlDoc.CreateElement("IP");
            root.InnerText = iP;
            xmlDoc.AppendChild(root);
            xmlDoc.Save(filepath);
            Debug.Log("createIP OK!");
            TextWrite.text = "createIP OK!";
        }
        else
        {
            XmlDocument xmlDoc = new XmlDocument();
            XmlNode docNode = xmlDoc.CreateXmlDeclaration("1.0", "UTF-8", null);
            //根据路径将XML读取出来
            xmlDoc.Load(filepath);
            XmlNode root = xmlDoc.SelectSingleNode("IP");
            root.InnerText = iP;
            xmlDoc.Save(filepath);
            Debug.Log(iP);
            TextWrite.text = iP;
        }
    }

    string IPname = "";

    public void ReadIP()
    {
        string filepath = Application.persistentDataPath + @"/IP.txt";
        if (File.Exists(filepath))
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(filepath);
            XmlNode root = xmlDoc.SelectSingleNode("IP");

            IPname = root.InnerText.ToString().Trim();
            TextRead.text = IPname;
        }
        else
        {
            SaveIp(IPname);
            TextRead.text = "Save:" + IPname;
        }
    }


*/











    void ScoketStart()
    {
        UDPClientIP = "192.168.3.167";//服务端的IP.自己更改
        UDPClientIP = UDPClientIP.Trim();
        Debug.LogWarning(UDPClientIP);
        InitSocket(); //在这里初始化
    }

    void InitSocket()
    {
        ipEnd = new IPEndPoint(IPAddress.Parse(UDPClientIP), 8888);
        socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
        IPEndPoint sender = new IPEndPoint(IPAddress.Any, 0);
        serverEnd = (EndPoint)sender;
        print("等待连接");
        SocketSend("hello");
        print("连接");
        ////开启一个线程连接收
        //connectThread = new Thread(new ThreadStart(SocketReceive));
        //connectThread.Start();
    }
    void SocketSend(string sendStr)
    {
        //清空
        sendData = new byte[1048576];
        //数据转换
        sendData = Encoding.UTF8.GetBytes(sendStr);
        //发送给指定服务端
        socket.SendTo(sendData, sendData.Length, SocketFlags.None, ipEnd);
    }

    //服务器接收
    void SocketReceive()
    {
        while (true)
        {

            recvData = new byte[1048576];
            try
            {
                recvLen = socket.ReceiveFrom(recvData, ref serverEnd);
            }
            catch (Exception e)
            {
            }

            print("message from: " + serverEnd.ToString());
            if (recvLen > 0)
            {
                recvStr = Encoding.UTF8.GetString(recvData, 0, recvLen);
            }

            print(recvStr);
        }
    }

    //连接关闭
    void SocketQuit()
    {
        //关闭线程
        if (connectThread != null)
        {
            connectThread.Interrupt();
            connectThread.Abort();
        }
        //最后关闭socket
        if (socket != null)
            socket.Close();
    }
    void OnApplicationQuit()
    {
        SocketQuit();
    }




}
