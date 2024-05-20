using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletControl : MonoBehaviour
{
    private float _speed = 50f;

    public Vector3 _target { get; set; }

    public bool _hit {  get; set; }
    
    void Start()
    {
        
    }

    
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, _target, _speed * Time.deltaTime);

        if(Vector3.Distance(transform.position, _target) < .01f)
        {
            gameObject.SetActive(false);
        }

    }
}
