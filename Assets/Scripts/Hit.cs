using System.Collections;
using System.Collections.Generic;
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
        if (other.gameObject.name == "AtackCol")
        {
            _isHit = true;
           // Morte();
        } 
    }
    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.name == "AtackCol")
        {
            _isHit = false;
        }
    }

    void Morte()
    {
       transform.parent.gameObject.SetActive(false);
    }

}
