using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BossMovement : MonoBehaviour
{
    public GameControl _gameCtrl;
    public int _vidaChefe = 100;
    public NavMeshAgent _agent;
    public float _distPlayer;

    void Start()
    {
        _gameCtrl = Camera.main.GetComponent<GameControl>();

    }

    void Update()
    {
        
    }

    public void MovimentoChefe()
    {

    }

}
