using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControleInimigo : MonoBehaviour
{
    public UnityEngine.AI.NavMeshAgent _agent;
    [SerializeField] int _numberPos;
    [SerializeField] bool _checkPos;

    [SerializeField] float _velocAnim;
    [SerializeField] Animator _anima;

    public Transform _player;
    [SerializeField] GameControl _gameControl;
    [SerializeField] float _distPlayer;
    [SerializeField] bool _segPlayer;

    public float _iniLife;
    public float _iLifeini = 3;

    // HitCheck depois mudar pra morte,
    public bool _hitCheck;

    public bool _deathCheck;

    float _checkTime;
    [SerializeField] float _timeLimit; // corpo seco 1.4 segundos tempLimit

    [SerializeField] bool _atacando;
    [SerializeField] BoxCollider _ataqueColl;

    void Start()
    {
        _agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        _anima = GetComponent<Animator>();
        
        _gameControl = Camera.main.GetComponent<GameControl>();

        _segPlayer = true;
        _atacando = false;

        
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!_hitCheck)
        {
            Movimento();
            SeguirPlayer();
        }
        else if(_iniLife==0)
        {
           // Hit(true);
        }
        
        Anima();

        //Script de contagem regressiva
        if (_hitCheck)
        {
            _agent.velocity = new Vector3(0,0,0);
            _checkTime -= Time.deltaTime;
            if( _checkTime < 0)
            {
                //Hit(false);
                _hitCheck = false;
                _checkTime = _timeLimit;
            }
        }
        

    }

    void Movimento()
    {
        float _distMovp = Vector3.Distance(transform.position, _gameControl._iniMovPos[_numberPos].transform.position);

        _agent.SetDestination(_gameControl._iniMovPos[_numberPos].transform.position);

        if (_distMovp < 4 && _checkPos == false)
        {
            _atacando = false;
            _checkPos = true;
            _segPlayer = true;
            _numberPos++;
            Invoke("TimeCheckPos", 1f);
        }
        if (_numberPos >= _gameControl._iniMovPos.Length)
        {
            _numberPos = 0;
        }
    }

    void SeguirPlayer()
    {
        //Preciso fazer Inimigo ficar parado enquanto ataca


        _distPlayer = Vector3.Distance(transform.position, _player.position);

        if (_distPlayer < 4.5 && _segPlayer)
        {
            _agent.SetDestination(_player.position);

            if(_distPlayer <= 1 && _gameControl._player.gameObject.GetComponent<PlayerMovement>()._vidaInicialPlayer > 0)
            {
                _atacando = true;
            }
            else _atacando = false;

        }
        else if (_distPlayer > 4.8 && !_segPlayer)
        {
            _atacando = false;
            _agent.SetDestination(_gameControl._iniMovPos[_numberPos].transform.position);
            _segPlayer = false;
        }
    }

    void Anima()
    {
        _velocAnim = Mathf.Abs(_agent.velocity.x + _agent.velocity.z);
        _anima.SetFloat("Veloc", _velocAnim);
        _anima.SetBool("Hit", _hitCheck);
        _anima.SetBool("Morte", _deathCheck);
        _anima.SetBool("Atacar", _atacando);
        
    }

    void TimeCheckPos()
    {
        if (_checkPos)
        {
            _checkPos = false;
        }
    }

}
