using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControleInimigo : MonoBehaviour
{
    [SerializeField] UnityEngine.AI.NavMeshAgent _agent;
    [SerializeField] Transform[] _pos;
    [SerializeField] int _numberPos;
    [SerializeField] bool _checkPos;

    [SerializeField] float _velocAnim;
    [SerializeField] Animator _anima;

    [SerializeField] Transform _player;
    [SerializeField] float _distPlayer;
    [SerializeField] bool _segPlayer;

    Hit _hit;

    void Start()
    {
        _agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        _anima = GetComponent<Animator>();
        _hit = GetComponent<Hit>();
    }

    // Update is called once per frame
    void Update()
    {
        Movimento();
        SeguirPlayer();
        Anima();

    }

    void Movimento()
    {
        _agent.SetDestination(_pos[_numberPos].transform.position);

        if (_agent.remainingDistance < 5 && _checkPos == false)
        {
            _checkPos = true;
            _segPlayer = true;
            _numberPos++;
            Invoke("TimeCheckPos", 1f);
        }
        if (_numberPos > 3)
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

            _agent.SetDestination(_pos[_numberPos].transform.position);
            _segPlayer = false;
        }
        if ( _distPlayer <= 1.6)
        {
            _agent.isStopped = true;
        }else if (_distPlayer > 2)
        {
            _agent.isStopped = false;
        }
    }

    void Anima()
    {
        _velocAnim = Mathf.Abs(_agent.velocity.x + _agent.velocity.z);
        _anima.SetFloat("Veloc", _velocAnim);
        _anima.SetBool("Hit", _hit._isHit);
    }

    void TimeCheckPos()
    {
        if (_checkPos)
        {
            _checkPos = false;
        }
    }

}
