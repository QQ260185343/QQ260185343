using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImageCtronllerYILOUYANHUI : MonoBehaviour
{


    public bool IsOnLight = false;
    public bool IsToClick = false;
    public Text texttime;
    public Text textXX;
    public Sprite sprite0, sprite1;
    // 初始化
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
        if (IsToClick)
        {
            changeColor();
        }
          
        
    }



    public void changeColor()
    {
        if (IsOnLight)
        {
           
            texttime.fontStyle = FontStyle.Bold;
            texttime.color = new Color(55 / 255f, 215 / 255f, 167 / 255f);
            textXX.horizontalOverflow = HorizontalWrapMode.Overflow;
            
            textXX.fontStyle = FontStyle.Bold;
            textXX.color = new Color(55 / 255f, 215 / 255f, 167 / 255f);
            gameObject.GetComponent<Image>().sprite = sprite1;
        }
        else
        {
          
            texttime.fontStyle = FontStyle.Normal;

            texttime.color = new Color(201 / 255f, 227 / 255f, 254 / 255f);
            textXX.fontStyle = FontStyle.Normal;
            textXX.horizontalOverflow = HorizontalWrapMode.Overflow;
            textXX.color = new Color(201 / 255f, 227 / 255f, 254 / 255f);
            gameObject.GetComponent<Image>().sprite = sprite0;
        }
      //  Debug.Log(textXX.color.ToString());
        IsToClick = false;
    }
}


