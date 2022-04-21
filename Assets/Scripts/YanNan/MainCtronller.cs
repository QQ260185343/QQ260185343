using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCtronller : MonoBehaviour
{
    // 初始化
    void Awake()
    {

    }
    // Start is called before the first frame update
    void Start()
    {
      //  StartCoroutine(StartWait());  
    }

    IEnumerator StartWait()
    {
        yield return new WaitForSeconds (0.1f);
        GameObject.Find("Scene Gizmo Camera").AddComponent<MainCtronller>();
    }
    // Update is called once per frame
    void Update()
    {

    }


    public void SetCameraCanBeCtronller()
    {
      
    }
}
