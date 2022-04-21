using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelCtronllers_3YJ : MonoBehaviour
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

        if ((DateTime.Now > DateTime.Parse("13:00")) && (DateTime.Now < DateTime.Parse("13:30")))
        {
            showNum = 0;
        }
        else
        if ((DateTime.Now > DateTime.Parse("13:30")) && (DateTime.Now < DateTime.Parse("13:35")))
        {
            showNum = 1;
        }
        else
        if ((DateTime.Now > DateTime.Parse("13:05")) && (DateTime.Now < DateTime.Parse("14:05")))
        {
            showNum = 2;
        }
        else
        if ((DateTime.Now > DateTime.Parse("14:05")) && (DateTime.Now < DateTime.Parse("14:20")))
        {
            showNum = 3;
        }
        else
        if ((DateTime.Now > DateTime.Parse("14:20")) && (DateTime.Now < DateTime.Parse("14:30")))
        {
            showNum = 4;
        }
        else
        if ((DateTime.Now > DateTime.Parse("14:30")) && (DateTime.Now < DateTime.Parse("14:45")))
        {
            showNum = 5;
        }
        else
        if ((DateTime.Now > DateTime.Parse("14:45")) && (DateTime.Now < DateTime.Parse("15:00")))
        {
            showNum = 6;
        }
        else
        if ((DateTime.Now > DateTime.Parse("15:00")) && (DateTime.Now < DateTime.Parse("15:15")))
        {
            showNum = 7;
        }
        else
        if ((DateTime.Now > DateTime.Parse("15:15")) && (DateTime.Now < DateTime.Parse("15:30")))
        {
            showNum = 8;
        }
        else
        if ((DateTime.Now > DateTime.Parse("15:30")) && (DateTime.Now < DateTime.Parse("16:15")))
        {
            showNum = 9;
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
        ImageLists[0].GetComponent<ImageCtronller>().texttime.text = "13:00-13:30";
        ImageLists[0].GetComponent<ImageCtronller>().textXX.text = "签到";
        ImageLists[1].GetComponent<ImageCtronller>().texttime.text = "13:30-13:35";
        ImageLists[1].GetComponent<ImageCtronller>().textXX.text = "致辞";
        ImageLists[2].GetComponent<ImageCtronller>().texttime.text = "13:35-14:05";
        ImageLists[2].GetComponent<ImageCtronller>().textXX.text = "持续创新，构建生态";
        ImageLists[3].GetComponent<ImageCtronller>().texttime.text = "14:05-14:20";
        ImageLists[3].GetComponent<ImageCtronller>().textXX.text = "千军万马上战场,共筑北京新生态";
        ImageLists[4].GetComponent<ImageCtronller>().texttime.text = "14:20-14:30";
        ImageLists[4].GetComponent<ImageCtronller>().textXX.text = "茶歇";
        ImageLists[5].GetComponent<ImageCtronller>().texttime.text = "14:30-14:45";
        ImageLists[5].GetComponent<ImageCtronller>().textXX.text = "风雨同舟,云上共生-中软国际";
        ImageLists[6].GetComponent<ImageCtronller>().texttime.text = "14:45-15:00";
        ImageLists[6].GetComponent<ImageCtronller>().textXX.text = "共探中长尾新生态";
        ImageLists[7].GetComponent<ImageCtronller>().texttime.text = "15:00-15:15";
        ImageLists[7].GetComponent<ImageCtronller>().textXX.text = " 数字化转型及云市场洞察报告分享";
        ImageLists[8].GetComponent<ImageCtronller>().texttime.text = "15:15-15:30";
        ImageLists[8].GetComponent<ImageCtronller>().textXX.text = "天眼查助力商业智能决策经验分享";
        ImageLists[9].GetComponent<ImageCtronller>().texttime.text = "15:30-16:15";
        ImageLists[9].GetComponent<ImageCtronller>().textXX.text = "主题分享:协同OA/用友BIP/沃丰科技UDESK 智能客服";

    }

}
