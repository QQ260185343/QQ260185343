using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetScenesShowLog : MonoBehaviour
{

    public List<GameObject> DebObjects;
    public List<GameObject> BaiMos;
    public Button buttonShowBaiMo;
    // 初始化
    void Awake()
    {
        
    }
    // Start is called before the first frame update
    void Start()
    {
        if (ScenesSetingDates.IsShowLog)
        {
            for (int i = 0; i < DebObjects.Count; i++)
            {
                DebObjects[i].SetActive(true);
            }
        }
        else
        {
            for (int i = 0; i < DebObjects.Count; i++)
            {
                DebObjects[i].SetActive(false);
            }
        }
    }

    public void ShowBaiMo()
    {
        if (BaiMos[0].activeSelf ==true)
        {
            for (int i = 0; i < BaiMos.Count; i++)
            {
                BaiMos[i].SetActive(false);
            }
            buttonShowBaiMo.GetComponentInChildren<Text>().text = "不显示白模";
        }
        else
        {
            for (int i = 0; i < BaiMos.Count; i++)
            {
                BaiMos[i].SetActive(true);

            }
            buttonShowBaiMo.GetComponentInChildren<Text>().text = "显示白模";
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
