using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControleInimigo : MonoBehaviour
{
    [SerializeField] UnityEngine.AI.NavMeshAgent _agent;
    [SerializeField] int _numberPos;
    [SerializeField] bool _checkPos;

    [SerializeField] float _velocAnim;
    [SerializeField] Animator _anima;

    [SerializeField] Transform _player;
    [SerializeField] GameControl _gameControl;
    [SerializeField] float _distPlayer;
    [SerializeField] bool _segPlayer;

    float _iniLife;
    public float _iLifeini = 3;

    // HitCheck depois mudar pra morte,
    public bool _hitCheck;

    public bool _deathCheck;

    float _checkTime;
    [SerializeField] float _timeLimit; // corpo seco 1.4 segundos tempLimit


    void Start()
    {
        _agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        _anima = GetComponent<Animator>();
        
        _gameControl = Camera.main.GetComponent<GameControl>();
        _player = _gameControl._player;

        _segPlayer = true;
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
            Hit(true);
        }
        
        Anima();

        //Script de contagem regressiva
        if (_hitCheck)
        {
            _checkTime -= Time.deltaTime;
            if( _checkTime < 0)
            {
                Hit(false);
                _hitCheck = false;
                _checkTime = _timeLimit;
            }
        }
        

    }

    void Movimento()
    {
        float _distMovp = Vector3.Distance(transform.position, _gameControl._iniMovPos[_numberPos].transform.position);

        _agent.SetDestination(_gameControl._iniMovPos[_numberPos].transform.position);

        if (_distMovp < 5 && _checkPos == false)
        {
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
        _distPlayer = Vector3.Distance(transform.position, _player.position);

        if (_distPlayer < 6.5 && _segPlayer)
        {
            _agent.SetDestination(_player.position);

        }
        else if (_distPlayer > 6.5 && !_segPlayer)
        {

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
        
    }

    void TimeCheckPos()
    {
        if (_checkPos)
        {
            _checkPos = false;
        }
    }

    void Hit(bool on)
    {
        if (on)
        {
            _agent.velocity = new Vector3(0, 0, 0);
            gameObject.GetComponent<CapsuleCollider>().enabled = false;
            transform.gameObject.SetActive(false);
            //Debug.Log("morte");
        }
      
    }
    

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("AtaqueMelee"))
        {
            _hitCheck = true;
            _iniLife--;
        }
    }

    public void Restart() 
    {
        _iniLife = _iLifeini;
        gameObject.GetComponent<CapsuleCollider>().enabled = true;
    }
}
