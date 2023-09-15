using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Hit : MonoBehaviour
{
    Animator _anim;
    public bool _isHit = false;
    
    void Start()
    {
        _anim = GetComponent<Animator>();
    }

    
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        
    }
   
    void Morte()
    {
       transform.parent.gameObject.SetActive(false);
    }

}
