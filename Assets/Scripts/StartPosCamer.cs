using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartPosCamer : MonoBehaviour
{
    
    public Vector3 _rot;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        _rot = Camera.main.transform.eulerAngles;
        transform.eulerAngles = new Vector3(_rot.x, transform.eulerAngles.y, transform.eulerAngles.z);
    }
}
