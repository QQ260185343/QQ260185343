using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelCtronllers1_YHT : MonoBehaviour
{
    public List<GameObject> ImageLists;
    
    public int showNum = 0;
    void Awake()
    {


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



    /// <summary>
    /// 根据时间调整显示
    /// </summary>
    void UseTimeChangeNum()
    {
       
        if ((DateTime.Now > DateTime.Parse("09:00")) && (DateTime.Now < DateTime.Parse("09:50")))
        {
            showNum = 0;
        }
        else 
        if ((DateTime.Now > DateTime.Parse("09:50")) && (DateTime.Now < DateTime.Parse("10:55")))
        {
            showNum = 1;
        }
        else
        if ((DateTime.Now > DateTime.Parse("10:55")) && (DateTime.Now < DateTime.Parse("12:15")))
        {
            showNum = 2;
        }
        else
        if ((DateTime.Now > DateTime.Parse("14:00")) && (DateTime.Now < DateTime.Parse("18:00")))
        {
            showNum = 3;
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
                    ImageLists[i].GetComponent<ImageCtronllerYILOUYANHUI>().IsOnLight = false;
                    ImageLists[i].GetComponent<ImageCtronllerYILOUYANHUI>().IsToClick = true;
                }
                else
                {
                    ImageLists[i].GetComponent<ImageCtronllerYILOUYANHUI>().IsOnLight = true;
                    ImageLists[i].GetComponent<ImageCtronllerYILOUYANHUI>().IsToClick = true;
                }
            }
        }
        else
        {
            for (int i = 0; i < ImageLists.Count; i++)
            {
              
                    ImageLists[i].GetComponent<ImageCtronllerYILOUYANHUI>().IsOnLight = false;
                    ImageLists[i].GetComponent<ImageCtronllerYILOUYANHUI>().IsToClick = true;
               
            }
        }
       
    }


}
