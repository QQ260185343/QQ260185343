using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelCtronllers_3_2 : MonoBehaviour
{
    public List<GameObject> ImageLists;

    public int showNum = 0;


    [Header("状态图片")]
    public Image ZT_imange;
    public Sprite ZT_sprite0, ZT_sprite1;
    void Awake()
    {

        setAllText0();
        ZT_imange.sprite = ZT_sprite0;

    }
    // Start is called before the first frame update
    void Start()
    {

    }


    // Update is called once per frame
    void Update()
    {
        UseTimeChangeNum();
        setAlllist();
    }



    void UseTimeChangeNum()
    {

        if ((DateTime.Now > DateTime.Parse("13:30")) && (DateTime.Now < DateTime.Parse("14:00")))
        {
            showNum = 0;
        }
        else
        if ((DateTime.Now > DateTime.Parse("14:00")) && (DateTime.Now < DateTime.Parse("14:15")))
        {
            showNum = 1;
        }
        else
        if ((DateTime.Now > DateTime.Parse("14:15")) && (DateTime.Now < DateTime.Parse("14:30")))
        {
            showNum = 2;
        }
        else
        if ((DateTime.Now > DateTime.Parse("14:30")) && (DateTime.Now < DateTime.Parse("14:45")))
        {
            showNum = 3;
        }
        else
        if ((DateTime.Now > DateTime.Parse("14:45")) && (DateTime.Now < DateTime.Parse("15:00")))
        {
            showNum = 4;
        }
        else
        if ((DateTime.Now > DateTime.Parse("15:00")) && (DateTime.Now < DateTime.Parse("15:15")))
        {
            showNum = 5;
        }
        else
        if ((DateTime.Now > DateTime.Parse("15:15")) && (DateTime.Now < DateTime.Parse("15:30")))
        {
            showNum = 6;
        }
        else
        if ((DateTime.Now > DateTime.Parse("15:30")) && (DateTime.Now < DateTime.Parse("15:45")))
        {
            showNum = 7;
        }
       
        else
        {
            showNum = -1;
            ZT_imange.sprite = ZT_sprite1;
        }




    }

    public void setAlllist()
    {
        if (showNum >= 0)
        {
            for (int i = 0; i < ImageLists.Count; i++)
            {
                if (i != showNum)
                {
                    ImageLists[i].GetComponent<ImageCtronller>().IsOnLight = false;
                    ImageLists[i].GetComponent<ImageCtronller>().IsToClick = true;
                }
                else
                {
                    ImageLists[i].GetComponent<ImageCtronller>().IsOnLight = true;
                    ImageLists[i].GetComponent<ImageCtronller>().IsToClick = true;
                }
            }
            ZT_imange.sprite = ZT_sprite0;
        }
        else
        {
            for (int i = 0; i < ImageLists.Count; i++)
            {

                ImageLists[i].GetComponent<ImageCtronller>().IsOnLight = false;
                ImageLists[i].GetComponent<ImageCtronller>().IsToClick = true;

            }
        }

    }

    public void setAllText0()
    {
        ImageLists[0].GetComponent<ImageCtronller>().texttime.text = "13:00-14:00";
        ImageLists[0].GetComponent<ImageCtronller>().textXX.text = "签到";
        ImageLists[1].GetComponent<ImageCtronller>().texttime.text = "14:00-14:15";
        ImageLists[1].GetComponent<ImageCtronller>().textXX.text = "致辞";
        ImageLists[2].GetComponent<ImageCtronller>().texttime.text = "14:15-14:30";
        ImageLists[2].GetComponent<ImageCtronller>().textXX.text = "华为云汽车行业整体服务战略";
        ImageLists[3].GetComponent<ImageCtronller>().texttime.text = "14:30-14:45";
        ImageLists[3].GetComponent<ImageCtronller>().textXX.text = "北汽集团上云探索及实践分享";
        ImageLists[4].GetComponent<ImageCtronller>().texttime.text = "14:45-15:00";
        ImageLists[4].GetComponent<ImageCtronller>().textXX.text = "解放云助力企业数字化转型";
        ImageLists[5].GetComponent<ImageCtronller>().texttime.text = "15:00-15:15";
        ImageLists[5].GetComponent<ImageCtronller>().textXX.text = "智慧出行发展趋势及创新实践";
        ImageLists[6].GetComponent<ImageCtronller>().texttime.text = "15:15-15:30";
        ImageLists[6].GetComponent<ImageCtronller>().textXX.text = "自动驾驶全场景量产实践思考";
        ImageLists[7].GetComponent<ImageCtronller>().texttime.text = "15:30-15:45";
        ImageLists[7].GetComponent<ImageCtronller>().textXX.text = " 新兴造车企业的智能网联化策略和趋势洞见";


    }

}
