﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gold : MonoBehaviour {

    private Transform m_Transform;
   // private GUIText m_GUIText;

	void Start () {
        GameObject.Destroy(gameObject, Random.Range(15f, 20f));
        m_Transform = gameObject.GetComponent<Transform>();
      //  m_GUIText = GameObject.Find("Score").GetComponent<GUIText>();
    }

    private void Update()
    {
        m_Transform.Rotate(Vector3.forward, 10f);
    }

   

}
