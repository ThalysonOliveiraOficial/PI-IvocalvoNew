using Cinemachine;
using DG.Tweening;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
 
    public Transform _orientation;

    float _moveX,_moveZ;

    private CharacterController _controller;

    Vector3 _moveDir;

    [SerializeField] Animator _anim;

    [SerializeField] float _jumpHeight = 1f, _moveSpeed = 2.35f, _gravityValue = -9.81f;

    Vector3 _playerVelocity;

    [SerializeField] bool _groundedPlayer;
    [SerializeField] bool _checkJump;
    [SerializeField] bool _checkRunnig;

    [SerializeField] float _correndo = 0;
    [SerializeField] float _pulando = 0;
    [SerializeField] bool _pulandoCheck;

    float _timer;
    [SerializeField] float _timerValue;

    [SerializeField] CameraTerceiraPessoa _controleCam;

    [SerializeField] bool _checkAim;
    [SerializeField] bool _checkTiro;
    [SerializeField] bool _checkMorte;
    [SerializeField] bool _checkHitMo;
    int _ctrlTiro;

    public GameControl _gameCtrl;
    public Transform _posPedra;
    public GameObject _bala;

    public float _vidaInicialPlayer;
    float _vidaPlayer = 3;

    [SerializeField] ParticleSystem _hitPlayerPartc;
    [SerializeField] ParticleSystem _RestartPlayerPartc;

    [SerializeField] Transform _TelaGameOver;
    [SerializeField] Button _reiniciarBT;

    public bool _playerVivo;

    //timer pra criar delay no pulo pra n�o ter spam
    float _delayJumpTimer;
    [SerializeField] float _delayJumpTimerValue;
    [SerializeField] bool _pularDelay;

    //variavel pra salvar posi��o p/ checkpoint
    public Vector3 _posSalva;

    private void Start()
    {
        _gameCtrl = Camera.main.GetComponent<GameControl>();
        _controller = GetComponent<CharacterController>();

        _timer = _timerValue;

        _vidaInicialPlayer = _vidaPlayer;
        _playerVivo = true;

        _pularDelay = true;
        _delayJumpTimer = _delayJumpTimerValue;


        _posSalva.x = PlayerPrefs.GetFloat("posX");
        _posSalva.y = PlayerPrefs.GetFloat("posY");
        _posSalva.z = PlayerPrefs.GetFloat("posZ");

        transform.localPosition = _posSalva;

    }

    private void Update()
    {
        //desbugar o pulo
        if (_checkJump)
        {
            _timer -= Time.deltaTime;
            if (_timer < 0)
            {
                _checkJump = false;
                _timer = _timerValue;
            }
        }
        //Delay Pulo

        if (!_pularDelay)
        {
            _delayJumpTimer -= Time.deltaTime;
            if ( _delayJumpTimer < 0)
            {
                _pularDelay = true;
                _delayJumpTimer = _delayJumpTimerValue;
            }
        }

        AnimacaoPlayer();
        Gravidade();
        GroundCheck();

        if (!_checkMorte)
        {
            MovimentoPlayer();
            Pulo();
        }
        
    }


    private void MovimentoPlayer()
    {
        //orienta��o do movimento
        _moveDir = (_orientation.forward * _moveZ + _orientation.right * _moveX) * _moveSpeed;       

        //movimento
        _controller.Move(new Vector3(_moveDir.x, _controller.velocity.y, _moveDir.z) * Time.deltaTime);
        //_cine.m_XAxis.Value

        float _Velocparar = Mathf.Abs(_moveZ) + Mathf.Abs(_moveX);

        //checkar se o botao de correr foi apertado e mudar o _moveSpeed
        if (_checkRunnig && _Velocparar != 0)
        {
            _moveSpeed = 5.75f;
        }
        else
        {
            _moveSpeed = 2.35f;
        }
       
        _pulando = _controller.velocity.y;

        //mira
        if(_checkAim )
        {
            _gameCtrl.MiraCano.SetActive(true);
            _gameCtrl.MiraMarker.SetActive(true);
            _gameCtrl.BaladeiraOBJ.SetActive(true);
        }
        else
        {
            _gameCtrl.MiraCano.SetActive(false);
            _gameCtrl.MiraMarker.SetActive(false);
            _gameCtrl.BaladeiraOBJ.SetActive(false);
        }

    }

    void AnimacaoPlayer()
    {
        _anim.SetFloat("Andando", Mathf.Abs(_moveZ) + Mathf.Abs(_moveX));

        // checkar se esta correndo e ativar a anima��o
        if (_moveSpeed > 4)
        {
            _anim.SetFloat("Correndo", _correndo = 1);
        }
        else
        {
            _anim.SetFloat("Correndo", _correndo = 0);
        }

        _anim.SetBool("Chao", _pulandoCheck);
        _anim.SetFloat("Pulando", _pulando);
        _anim.SetBool("Mirar", _checkAim);
        _anim.SetBool("Atirar", _checkTiro);
        _anim.SetBool("Morto", _checkMorte);
        _anim.SetBool("Apanhar", _checkHitMo);
        _anim.SetFloat("Vida", _vidaPlayer);
    }

    void GroundCheck()
    {
        //Check chao. se chao y velocity == 0
        _groundedPlayer = _controller.isGrounded;
        if (_groundedPlayer && _playerVelocity.y < 0)
        {
            _playerVelocity.y = 0f;
            _pulandoCheck = true;
            
        }
        
    }
    public void SetMove(InputAction.CallbackContext value)
    {
        Vector3 m = value.ReadValue<Vector3>();
        _moveX = m.x;
        _moveZ = m.y;
       
    }

    public void SetJump(InputAction.CallbackContext value)
    {
        _checkJump = true;

    }
    public void SetRun(InputAction.CallbackContext value)
    {
        
         _checkRunnig = value.performed;
    }

    public void SetCombatAim(InputAction.CallbackContext value)
    {
        _checkAim = value.performed;
        if(_checkAim)
        {
            _controleCam.TrocarEstiloCamera(CameraTerceiraPessoa.CameraEstilo.Combat);

        }
        if(!_checkAim)
        {
            _controleCam.TrocarEstiloCamera(CameraTerceiraPessoa.CameraEstilo.Basic);
        }
       
    }

    public void SetTiro(InputAction.CallbackContext value)
    {
        _checkTiro = value.performed;
    } 
   

    private void Pulo()
    {
        if (_groundedPlayer && _checkJump && _pularDelay)
        {
            _checkJump = false;
            _playerVelocity.y = _playerVelocity.y + Mathf.Sqrt(_jumpHeight * -2.5f * _gravityValue);
            _pulandoCheck = false;
            _pularDelay = false;
        }
    }
    
    private void Gravidade()
    {
        _playerVelocity.y = _playerVelocity.y + _gravityValue * Time.deltaTime;
        _controller.Move(_playerVelocity * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("AtkInimigo") && !_checkHitMo)
        {
            _checkHitMo = true;
            _vidaInicialPlayer--;
            
            StartCoroutine(HitPartcPlayer());
            if (_vidaInicialPlayer <= 0)
            {
                //

                _TelaGameOver.DOScale(1f, 1.95f);
                _playerVivo = false;


                //Criar um Script separado
                _reiniciarBT.Select();

                //

                StartCoroutine(Morte());
            }
            StartCoroutine(HitTime());
        }

        if (other.CompareTag("Checkpoint"))
        {
            UnityEngine.Debug.Log(other.transform.localPosition);
            _gameCtrl.CheckpointSalvarPos(other.transform.localPosition);
        }
    }

    IEnumerator Morte()
    {
        _checkMorte = true;
        yield return new WaitForSeconds(4.4f);
        gameObject.SetActive(false);
    }

    IEnumerator HitTime()
    {
       
        yield return new WaitForSeconds(1.4f);
        _checkHitMo = false;
    }

    IEnumerator HitPartcPlayer()
    {
        _hitPlayerPartc.gameObject.SetActive(true);
        yield return new WaitForSeconds(1);
        _hitPlayerPartc.gameObject.SetActive(false);

    }

    IEnumerator RestartPlayerTime()
    {
        _RestartPlayerPartc.gameObject.SetActive(true);
        yield return new WaitForSeconds(0.3f);
        //gameObject.SetActive(true);
        _checkMorte = false;
        _RestartPlayerPartc.gameObject.SetActive(false);

    }

    public void RestartPlayer()
    {
        StartCoroutine(RestartPlayerTime());
    }

}
