using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletControl : MonoBehaviour
{
    private float _speed = 50f;

    public Rigidbody _rbBullet;
    
    void Start()
    {
        _rbBullet = GetComponent<Rigidbody>();
    }

    
    void Update()
    {
        /*
        transform.position = Vector3.MoveTowards(transform.position, _target, _speed * Time.deltaTime);

        if(Vector3.Distance(transform.position, _target) < .01f)
        {
            Debug.Log("tiro Acertado");
        }
        */
    }
}
