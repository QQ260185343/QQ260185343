using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameObjectLookAtObject : MonoBehaviour
{
    public GameObject lookObject;
    // 初始化
    void Awake()
    {
        
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate ()
    {
        Vector3 tar = lookObject.transform.position;
        tar.y = transform.position.y;
       gameObject. transform.LookAt(tar);
      // gameObject.transform.LookAt(lookObject.transform .position);
    }
}
