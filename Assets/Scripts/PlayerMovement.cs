using Cinemachine;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
 
    public Transform _orientation;

    float _moveX,_moveZ;

    Vector3 _moveDir;

    [SerializeField] float _jumpHeight = 1f, _moveSpeed = 2.35f, _gravityValue = -9.81f;

    Vector3 _playerVelocity;

    [SerializeField] bool _groundedPlayer;
    [SerializeField] bool _checkJump;
    [SerializeField] bool _checkRunnig;
    

    [SerializeField] Animator _anim;
    [SerializeField] float _correndo = 0;
    [SerializeField] float _pulando = 0;
    [SerializeField] bool _pulandoCheck;
    
    CharacterController _controller;

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

    

    private void Start()
    {
        _gameCtrl = Camera.main.GetComponent<GameControl>();
        _controller = GetComponent<CharacterController>();
        _timer = _timerValue;
        _vidaInicialPlayer = _vidaPlayer;

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

        Gravidade();
        GroundCheck();
        MovimentoPlayer();
        Pulo();
    }


    private void MovimentoPlayer()
    {
        //orienta��o do movimento
        _moveDir = (_orientation.forward * _moveZ + _orientation.right * _moveX) * _moveSpeed;       

        //movimento
        _controller.Move(new Vector3(_moveDir.x, _controller.velocity.y, _moveDir.z) * Time.deltaTime);
        //_cine.m_XAxis.Value


        _anim.SetFloat("Andando", Mathf.Abs(_moveZ) + Mathf.Abs(_moveX));

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

        

        // checkar se esta correndo e ativar a anima��o
        if (_moveSpeed > 4)
        {
           _anim.SetFloat("Correndo", _correndo = 1);
        }
        else
        {
           _anim.SetFloat("Correndo", _correndo = 0);
        }
       

        _pulando = _controller.velocity.y;

        _anim.SetBool("Chao", _pulandoCheck);
        _anim.SetFloat("Pulando", _pulando);
        _anim.SetBool("Mirar", _checkAim);
        _anim.SetBool("Atirar", _checkTiro);
        _anim.SetBool("Morto", _checkMorte);


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
        if (_groundedPlayer  && _checkJump)
        {
            _checkJump = false;
            _playerVelocity.y = _playerVelocity.y + Mathf.Sqrt(_jumpHeight * -2.5f * _gravityValue);
            _pulandoCheck = false;

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
            if (_vidaInicialPlayer <= 0)
            {
                StartCoroutine(Morte());
            }
            StartCoroutine(HitTime());
        }
    }

    IEnumerator Morte()
    {
        _checkMorte = true;
        yield return new WaitForSeconds(4.4f);
        gameObject.SetActive(false);
        _checkMorte = false;
    }

    IEnumerator HitTime()
    {
       
        yield return new WaitForSeconds(1.4f);
        _checkHitMo = false;
    }


}
