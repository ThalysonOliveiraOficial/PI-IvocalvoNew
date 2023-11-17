using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TiroBaladeira : MonoBehaviour
{
    public Rigidbody _rbPedra;
    public GameControl gameControl;
    
    [SerializeField] float _forca = 75;

    void Start()
    {
        
        _rbPedra = GetComponent<Rigidbody>();
        gameControl=Camera.main.GetComponent<GameControl>();
        //Invoke("Pedrada", 1);
    }

    
    void Update()
    {
        
    }

    public void Pedrada()
    {
        _rbPedra.velocity = Vector3.zero;
        transform.position = gameControl._player.GetComponent<PlayerMovement>()._posPedra.position;
        _rbPedra.AddForce(gameControl._player.GetComponent<PlayerMovement>()._posPedra.forward*_forca,ForceMode.Impulse);
        //_rbPedra.add

    }

}
