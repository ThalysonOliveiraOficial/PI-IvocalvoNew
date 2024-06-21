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
    public float _distPos;
    public int _posSelec;
    public float _speed;
    public Transform _player;
    [SerializeField] bool _fixAtPlayer;
    [SerializeField] Transform[] _pos;
    Animator _anim;

    public bool _iara;



    void Start()
    {
        _gameCtrl = Camera.main.GetComponent<GameControl>();
        //_agentBoss =GetComponent<NavMeshAgent>();
        _player = _gameCtrl._player;
        _anim = GetComponent<Animator>();

    }

    void Update()
    {
        MovimentoChefe();

    }

    public void MovimentoChefe()
    {
        if (_iara) {
            IaraCheck();
        }

    }

    void IaraCheck()
    {
        _distPlayer = Vector3.Distance(transform.position, _player.position);
        _distPos = Vector3.Distance(transform.position, _pos[_posSelec].position);

        if (_distPos >= 1 + .5f)
        {
            //movendo
            _anim.SetBool("Movendo", true);
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(_pos[_posSelec].transform.position.x, transform.position.y, _pos[_posSelec].transform.position.z), _speed * Time.deltaTime);

        }
        else
        {
            _anim.SetBool("Movendo", false);
            //parado
        }
        if (_fixAtPlayer) transform.LookAt(_player.position);

        if (_distPlayer < 31)
        {
            _fixAtPlayer = true;
        }
        else
        {
            _fixAtPlayer = false;
        }
    }

}
