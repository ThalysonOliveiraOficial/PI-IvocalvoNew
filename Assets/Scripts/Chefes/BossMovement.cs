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
    public List<int> _posSelecLis;
    public int _selecSort;
    public float _speed;
    public Transform _player;
    [SerializeField] bool _fixAtPlayer;
    [SerializeField] Transform[] _pos;
    Animator _anim;

    public bool _iara;

    float _timer;
    [SerializeField] float _timerValue = 10;

    void Start()
    {
        _gameCtrl = Camera.main.GetComponent<GameControl>();
        //_agentBoss =GetComponent<NavMeshAgent>();
        _player = _gameCtrl._player;
        _anim = GetComponent<Animator>();
        _timer = _timerValue;
        _posSelec = 1;
        //_timerValue = 8;
        Shuffle(_posSelecLis);
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

        //timer pra iara mudar de posição
        _timer -= Time.deltaTime;
        if (_timer < 0)
        {
            Debug.Log("TEMPO");
            // de 0 a 3 pois variavel maxima do rando.range é "excluida" logo deve por um acima

            _posSelec = _posSelecLis[_selecSort];
            _selecSort++;
            if(_selecSort> _posSelecLis.Count - 1)
            {
                _selecSort = 0;
                Shuffle(_posSelecLis);
            }

            Debug.Log(_posSelec);
            _timer = _timerValue;

        }



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
