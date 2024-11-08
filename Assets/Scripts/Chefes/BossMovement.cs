using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class BossMovement : MonoBehaviour
{
    public GameControl _gameCtrl;
    //public int _vidaChefe = 100;
    public float _distPlayer;
    public float _distPos;

    public int _posSelec;
    public List<int> _posSelecLis;
    public int _selecSort;

    public float _speed;
    public Transform _player;
    [SerializeField] bool _fixAtPlayer;
    [SerializeField] Transform[] _pos;
    Animator _anim;

    public bool _iara;

    float _timer;
    [SerializeField] float _timerValue = 12;
    float _timerAtk;
    [SerializeField] float _timerVatk;

    public bool _seMovendo;
    public bool _atacando;

    public int _ataque;
    public List<int> _ataqueLis;
    int _ataqueSort;

    public AtaqueBoss _atkBoss;
    public float _iaraVida;
    public bool _iaraMorta;

    public ParticleSystem _curaPart;

    void Start()
    {
        _gameCtrl = Camera.main.GetComponent<GameControl>();
        _atkBoss = GetComponent<AtaqueBoss>();
        //_agentBoss =GetComponent<NavMeshAgent>();
        _player = _gameCtrl._player;
        _anim = GetComponent<Animator>();
        _timer = _timerValue;
        _posSelec = 1;
        //_timerValue = 8;
        Shuffle(_posSelecLis);
        _iaraMorta = false;
        _curaPart.Stop();

    }

    void Update()
    {
        _iaraVida = GetComponent<BossVida>()._vidaBoss;
        AnimaIara();

        if (_iaraVida <= 0)
        {
            _iaraMorta = true;
            Morte();
        }

        if (!_iaraMorta)
        {
            MovimentoChefe();
        }
        
        _iara = _gameCtrl._bossOn;
    }

    public void MovimentoChefe()
    {
        if (_iara) {
            IaraCheck();
        }

    }
    void AnimaIara()
    {
        //Anima��es
        _anim.SetBool("Movendo", _seMovendo);
        _anim.SetInteger("Ataque", _ataque);
        _anim.SetBool("Atacando", _atacando);
        _anim.SetBool("Morta", _iaraMorta);
    }

    void IaraCheck()
    {
        _distPlayer = Vector3.Distance(transform.position, _player.position);
        _distPos = Vector3.Distance(transform.position, _pos[_posSelec].position);

        //timer pra iara mudar de posi��o
        _timer -= Time.deltaTime;
        if (_timer < 0)
        {
            _posSelec = _posSelecLis[_selecSort];
            _selecSort++;
            if(_selecSort> _posSelecLis.Count - 1)
            {
                _selecSort = 0;
                Shuffle(_posSelecLis);
            }

            _timer = _timerValue;

        }

        if (_distPos >= 1 + .5f)
        {
            //movendo
            _seMovendo = true;
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(_pos[_posSelec].transform.position.x, transform.position.y, _pos[_posSelec].transform.position.z), _speed * Time.deltaTime);

        }
        else
        {
            _seMovendo = false;
            //parado
        }
        if (_fixAtPlayer) transform.LookAt(_player.position);

        // Atacando
        if (!_seMovendo)
        {
            _timerAtk -= Time.deltaTime;
            if (_timerAtk < 0)
            {
                _atacando = true;

                _ataque = _ataqueLis[_ataqueSort];
                //mudar o contador pra ativar o ataque em area
                if (_ataque == 2 || _ataque == 3) _atkBoss._contadorAtk = 1;
                //ataque 1 � uma cura, se a iara estiver com 40 ou mais de vida, faz avida setar em 40 de novo
                if (_ataque == 1 && _iaraVida <= 40)
                {
                    GetComponent<BossVida>()._vidaBoss = GetComponent<BossVida>()._vidaBoss + 4;
                    IaraCuraAtk();
                    Debug.Log("iara curou");
                }else
                {
                    Debug.Log("iara vida cheia");
                    
                }

                _ataqueSort++;
                if (_ataqueSort > _ataqueLis.Count - 1)
                {
                    _ataqueSort = 0;
                    Shuffle(_ataqueLis);
                }

                _timerAtk = _timerVatk;
            }
        }
        else
        {
            _atacando = false;
            _ataque = 0;
        }

        if (_distPlayer < 35)
        {
            _fixAtPlayer = true;
            //Debug.Log(_distPlayer);
        }
        else
        {
            _fixAtPlayer = false;
        }

    }

    IEnumerator MorteIara()
    {
        _gameCtrl._hudCanvas.GetComponent<HudInventario>().BossIaraOff();
        _gameCtrl._hudCanvas.GetComponent<HudInventario>().VitoriaIara();
        yield return new WaitForSeconds(4.4f);
        // fazer painel para recome�ar o jogo depois de matar iara
        gameObject.SetActive(false);
    }

    IEnumerator CuraIara()
    {
        _curaPart.Play();
        yield return new WaitForSeconds(1.4f);
        _curaPart.Stop();

    }

    private void IaraCuraAtk()
    {
        GetComponent<BossVida>().HitBossVida();
        StartCoroutine(CuraIara());
    }

    private void Morte()
    {
        StartCoroutine(MorteIara());

    }

    void Shuffle(List<int> lists)
    {
        for (int j = lists.Count - 1; j > 0; j--)
        {
            int rnd = UnityEngine.Random.Range(0, j + 1);
            int temp = lists[j];
            lists[j] = lists[rnd];
            lists[rnd] = temp;
        }
    }

}
