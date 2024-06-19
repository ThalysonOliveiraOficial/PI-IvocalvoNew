using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BossMovement : MonoBehaviour
{
    public GameControl _gameCtrl;
    public int _vidaChefe = 100;
    public NavMeshAgent _agentBoss;
    public float _distPlayer;
    public Transform _player;
    [SerializeField] bool _fixAtPlayer;
    


    void Start()
    {
        _gameCtrl = Camera.main.GetComponent<GameControl>();
        //_agentBoss =GetComponent<NavMeshAgent>();
        _player = _gameCtrl._player;

    }

    void Update()
    {
        MovimentoChefe();

    }

    public void MovimentoChefe()
    {
        _distPlayer = Vector3.Distance(transform.position, _player.position);
        Debug.Log(_distPlayer);

        if(_fixAtPlayer ) transform.LookAt(_player.position);

        if(_distPlayer < 31)
        {
            _fixAtPlayer = true;
        }
        else
        {
            _fixAtPlayer = false;
        }

    }

}
