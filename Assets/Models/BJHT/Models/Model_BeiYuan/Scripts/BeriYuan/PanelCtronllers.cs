using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelCtronllers : MonoBehaviour
{
    public List<GameObject> ImageLists;
    [Header("这是第几个展板")]
    public int ShowPanelNum = 0;
    public int showNum, showNumOld = 0;
    void Awake()
    {
        if (ShowPanelNum == 0)
        {
            setAllText0();
        }
        if (ShowPanelNum == 1)
        {
            setAllText1();
        }
        if (ShowPanelNum == 2)
        {
            setAllText2();
        }

    }
    // Start is called before the first frame update
    void Start()
    {

    }

    float tome0 = 0;
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
        }




    }

    public void setAlllist()
    {
        if (showNum>=0)
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
        }
        else
        {
            for (int i = 0; i < ImageLists.Count; i++)
            {
              
                    ImageLists[i].GetComponent<ImageCtronller>().IsOnLight = true;
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
    public void setAllText00()
    {
        ImageLists[0].GetComponent<ImageCtronller>().texttime.text = "13:30-13:35";
        ImageLists[0].GetComponent<ImageCtronller>().textXX.text = "致辞";
        ImageLists[1].GetComponent<ImageCtronller>().texttime.text = "13:35-14:05";
        ImageLists[1].GetComponent<ImageCtronller>().textXX.text = "持续创新，构建生态";
        ImageLists[2].GetComponent<ImageCtronller>().texttime.text = "14:05-14:20";
        ImageLists[2].GetComponent<ImageCtronller>().textXX.text = "千军万马上战场，共筑北京新生态";
        ImageLists[3].GetComponent<ImageCtronller>().texttime.text = "14:20-14:30";
        ImageLists[3].GetComponent<ImageCtronller>().textXX.text = "/";
        ImageLists[4].GetComponent<ImageCtronller>().texttime.text = "14:30-14:50";
        ImageLists[4].GetComponent<ImageCtronller>().textXX.text = "风雨同舟，云上共生—中软国际";
        ImageLists[5].GetComponent<ImageCtronller>().texttime.text = "14:50-15:10";
        ImageLists[5].GetComponent<ImageCtronller>().textXX.text = "共探中长尾新生态";
        ImageLists[6].GetComponent<ImageCtronller>().texttime.text = "15:10-15:30";
        ImageLists[6].GetComponent<ImageCtronller>().textXX.text = "数字化转型及云市场洞察";
        ImageLists[7].GetComponent<ImageCtronller>().texttime.text = "15:30-15:50";
        ImageLists[7].GetComponent<ImageCtronller>().textXX.text = "主题分享：协同OA";
        ImageLists[8].GetComponent<ImageCtronller>().texttime.text = "15:50-16:10";
        ImageLists[8].GetComponent<ImageCtronller>().textXX.text = "主题分享：用友BIP";
        ImageLists[9].GetComponent<ImageCtronller>().texttime.text = "16:10-16:30";
        ImageLists[9].GetComponent<ImageCtronller>().textXX.text = "主题分享：沃丰科技UDESK 智能客服";

    }
    public void setAllText1()
    {
        ImageLists[0].GetComponent<ImageCtronller>().texttime.text = "14:00-14:05";
        ImageLists[0].GetComponent<ImageCtronller>().textXX.text = "欢迎致辞";
        ImageLists[1].GetComponent<ImageCtronller>().texttime.text = "14:05-14:10";
        ImageLists[1].GetComponent<ImageCtronller>().textXX.text = "欢迎致辞";
        ImageLists[2].GetComponent<ImageCtronller>().texttime.text = "14:10-14:15";
        ImageLists[2].GetComponent<ImageCtronller>().textXX.text = "欢迎致辞";
        ImageLists[3].GetComponent<ImageCtronller>().texttime.text = "14:20-14:30";
        ImageLists[3].GetComponent<ImageCtronller>().textXX.text = "华为云汽车行业整体服务战略";
        ImageLists[4].GetComponent<ImageCtronller>().texttime.text = "14:30-14:45";
        ImageLists[4].GetComponent<ImageCtronller>().textXX.text = "北汽集团上云探索及实践分享";
        ImageLists[5].GetComponent<ImageCtronller>().texttime.text = "14:45-15:00";
        ImageLists[5].GetComponent<ImageCtronller>().textXX.text = "智能出行创新实践分享";
        ImageLists[6].GetComponent<ImageCtronller>().texttime.text = "15:00-15:15";
        ImageLists[6].GetComponent<ImageCtronller>().textXX.text = "自动驾驶全场景量产实践分享";
        ImageLists[7].GetComponent<ImageCtronller>().texttime.text = "15:15-15:30";
        ImageLists[7].GetComponent<ImageCtronller>().textXX.text = "新兴造车企业的智能网联化策略和趋势洞见";
        ImageLists[8].GetComponent<ImageCtronller>().texttime.text = "15:30-15:45";
        ImageLists[8].GetComponent<ImageCtronller>().textXX.text = "普华永道-汽车行业数字化转型核心要素解读";
        ImageLists[9].GetComponent<ImageCtronller>().texttime.text = "15:45-16:00";
        ImageLists[9].GetComponent<ImageCtronller>().textXX.text = "智变观察团启动仪式";

    }
    public void setAllText2()
    {
        ImageLists[0].GetComponent<ImageCtronller>().texttime.text = "09:00-09:10";
        ImageLists[0].GetComponent<ImageCtronller>().textXX.text = "主持人开场：介绍与会领导&嘉宾";
        ImageLists[1].GetComponent<ImageCtronller>().texttime.text = "09:10-09:15";
        ImageLists[1].GetComponent<ImageCtronller>().textXX.text = "中关村数字经济产业联盟宣传片";
        ImageLists[2].GetComponent<ImageCtronller>().texttime.text = "09:15-09:20";
        ImageLists[2].GetComponent<ImageCtronller>().textXX.text = "华为技术有限公司宣传片";
        ImageLists[3].GetComponent<ImageCtronller>().texttime.text = "09:20-09:30";
        ImageLists[3].GetComponent<ImageCtronller>().textXX.text = "致辞";
        ImageLists[4].GetComponent<ImageCtronller>().texttime.text = "09:30-09:40";
        ImageLists[4].GetComponent<ImageCtronller>().textXX.text = "致辞";
        ImageLists[5].GetComponent<ImageCtronller>().texttime.text = "09:40-09:50";
        ImageLists[5].GetComponent<ImageCtronller>().textXX.text = "致辞";
        ImageLists[6].GetComponent<ImageCtronller>().texttime.text = "09:50-10:00";
        ImageLists[6].GetComponent<ImageCtronller>().textXX.text = "2021年度联盟工作报告及章程修改说明";
        ImageLists[7].GetComponent<ImageCtronller>().texttime.text = "10:00-10:25";
        ImageLists[7].GetComponent<ImageCtronller>().textXX.text = "数字化转型主题演讲";
        ImageLists[8].GetComponent<ImageCtronller>().texttime.text = "10:25-10:30";
        ImageLists[8].GetComponent<ImageCtronller>().textXX.text = "京西智谷人工智能计算中心签约发布";
        ImageLists[9].GetComponent<ImageCtronller>().texttime.text = "10:35-10:45";
        ImageLists[9].GetComponent<ImageCtronller>().textXX.text = "北京河图公司成立发布";
        ImageLists[10].GetComponent<ImageCtronller>().texttime.text = "10:45-11:05";
        ImageLists[10].GetComponent<ImageCtronller>().textXX.text = "汽车行业上云最佳路径白皮书";
        ImageLists[11].GetComponent<ImageCtronller>().texttime.text = "11:05-11:45";
        ImageLists[11].GetComponent<ImageCtronller>().textXX.text = "中数盟成员单位主题演讲/需求信息发布";
        ImageLists[12].GetComponent<ImageCtronller>().texttime.text = "11:45-12:25";
        ImageLists[12].GetComponent<ImageCtronller>().textXX.text = "中数盟成员单位主题演讲/供应信息发布";


    }
}
