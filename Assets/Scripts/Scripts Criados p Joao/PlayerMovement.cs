using Cinemachine;
using DG.Tweening;
using System.Collections;
using TMPro;
using Unity.VisualScripting;
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
    [SerializeField] bool _checkAndar;

    [SerializeField] float _correndo = 0;
    [SerializeField] float _pulando = 0;
    [SerializeField] bool _pulandoCheck;

    float _timer;
    [SerializeField] float _timerValue;

    [SerializeField] CameraTerceiraPessoa _controleCam;

    public bool _checkAim;
    [SerializeField] bool _checkTiro;
    [SerializeField] bool _checkMorte;
    [SerializeField] bool _checkHitMo;
    int _ctrlTiro;

    public GameControl _gameCtrl;

    public float _vidaInicialPlayer;
    public float _vidaPlayerMax = 10;

    [SerializeField] ParticleSystem _hitPlayerPartc;
    [SerializeField] ParticleSystem _RestartPlayerPartc;

    [SerializeField] Transform _TelaGameOver;
    [SerializeField] Button _reiniciarBT;

    public bool _playerVivo;

    //timer pra criar delay no pulo pra não ter spam
    float _delayJumpTimer;
    [SerializeField] float _delayJumpTimerValue;
    [SerializeField] bool _pularDelay;

    //variavel pra salvar checkpoint
    public Vector3 _posSalva;
    public float _vidaSalva;

    //Varialveis pra acessar HUD do jogo
    [SerializeField] VidaHud _SliderVida;
    
    //Variaveis para Mira e tiro
    private Transform _camTransform;
    [SerializeField] AtirarPool _AtirarPoolBala;

    public PlayerInput _playerInput;
    public InputAction _atirarAction;
    public Rigidbody _rbTiro;
    public float _forcaTiro = 20;
    public Transform _ponteiroMM;

    //grids Inventario
    GridItem _gridItem;

    //Dialogo NPC
    DialogNPCMissao _dialogNPCM;

    public Button _btDialogFechar;
    public bool _inventAberto;
    


    private void Start()
    {
        _gameCtrl = Camera.main.GetComponent<GameControl>();
        _controller = GetComponent<CharacterController>();
        _camTransform = Camera.main.transform;
        _AtirarPoolBala = GetComponent<AtirarPool>();
        _ponteiroMM = _gameCtrl._ponteiroMiniMap;
        _TelaGameOver = _gameCtrl._hudGameOver;
        _gridItem = Camera.main.GetComponent<GridItem>();
        _dialogNPCM = Camera.main.GetComponent<DialogNPCMissao>();
        //button fechar dialogo por no script HudInventario depois
        _btDialogFechar = _gameCtrl._hudCanvas.gameObject.GetComponent<HudInventario>()._imgRepsDialgNPC.gameObject.GetComponent<Button>();
        _inventAberto = _gameCtrl._hudCanvas.gameObject.GetComponent<HudInventario>()._inventHudAberto;

        _timer = _timerValue;

        _pularDelay = true;
        _delayJumpTimer = _delayJumpTimerValue;

        _vidaInicialPlayer = _vidaPlayerMax;
        _playerVivo = true;

        if(PlayerPrefs.GetInt("StartSalve") ==1)
        {
            //posiçao
            _posSalva.x = PlayerPrefs.GetFloat("posX");
            _posSalva.y = PlayerPrefs.GetFloat("posY");
            _posSalva.z = PlayerPrefs.GetFloat("posZ");
            transform.position = _posSalva;
            //vida
            _vidaSalva = PlayerPrefs.GetFloat("VidaPlayer");
            _vidaPlayerMax = _vidaSalva;
            //
        }

        _checkRunnig = true;
        _checkAndar = false;
    }

    private void Update()
    {
        _ponteiroMM.eulerAngles = new Vector3(0, 0, -_orientation.transform.eulerAngles.y);
        _inventAberto = _gameCtrl._hudCanvas.gameObject.GetComponent<HudInventario>()._inventHudAberto;

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
        
        //fazer personagem para de se mexer quando morrer ou abrir o inventario provavelmente ta errado, mas tá funcionando
        if (_inventAberto)
        {
            Debug.Log("Inventario aberto" );
        }
        else if (!_checkMorte || !_inventAberto)
        {
            MovimentoPlayer();
            Pulo();
        }
        if (_dialogNPCM._missaoConcluida[0] == true && _dialogNPCM._missaoConcluida[1] == false)
        {
            _dialogNPCM._contadorObjetivo = _gameCtrl._contKillInimigo;
        }
        
    }


    private void MovimentoPlayer()
    {
        //orientação do movimento
        _moveDir = (_orientation.forward * _moveZ + _orientation.right * _moveX) * _moveSpeed;       


        //movimento
        _controller.Move(new Vector3(_moveDir.x, _controller.velocity.y, _moveDir.z) * Time.deltaTime);
        

        float _Velocparar = Mathf.Abs(_moveZ) + Mathf.Abs(_moveX);


        if(_Velocparar == 0)
        {
            _checkRunnig = false;
        }
        if (_checkRunnig && _Velocparar != 0)
        {
            _moveSpeed = 5.75f;
        }
        else if (_checkAndar && !_checkRunnig && _Velocparar != 0)
        {
            _moveSpeed = 2.35f;
        }
       
        _pulando = _controller.velocity.y;

        //mira veloc e tiro(Bugado)
        if (_checkAim)
        {
            _moveSpeed = 2.35f;
        }

        
        if (_checkAim && _checkTiro)
        {
            Atirar();
            _checkTiro = false;
        }
        
    }

    void AnimacaoPlayer()
    {
        _anim.SetFloat("Andando", Mathf.Abs(_moveZ) + Mathf.Abs(_moveX));

        // checkar se esta correndo e ativar a animação
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
        _anim.SetFloat("Vida", _vidaInicialPlayer);
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
        Vector3 m = value.ReadValue<Vector2>();
        _moveX = m.x;
        _moveZ = m.y;
       
    }

    public void SetJump(InputAction.CallbackContext value)
    {
        _checkJump = true;

    }
    public void SetRun(InputAction.CallbackContext value)
    {
        _checkAndar = value.performed;
        
    }

    //Mira Input
    public void SetCombatAim(InputAction.CallbackContext value)
    {
        _checkAim = value.performed;

    }

    
    //Atirar Input
    public void SetTiro(InputAction.CallbackContext value)
    {
        _checkTiro = value.performed;
    } 
    

    public void Atirar()
    {
        GameObject bullet = AtirarPool.SharedInstance.GetPooledObject();
        if (bullet != null)
        {
            
            bullet.transform.position = _gameCtrl.SaidaTiro.transform.position;
            bullet.transform.rotation = _gameCtrl.SaidaTiro.transform.rotation;
            bullet.SetActive(true);
            _rbTiro = bullet.GetComponent<Rigidbody>();
            _rbTiro.velocity = Vector3.zero;
            _rbTiro.AddForce(transform.forward* _forcaTiro, ForceMode.Acceleration);

        }

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
        //colider itens
        if (other.gameObject.CompareTag("ItemTag"))
        {
            ItemControl _itemObj = other.GetComponent<ItemControl>();
            int tipoItem = _itemObj._itemDados.Tipo;
            bool bossItem = _itemObj._itemDados.ItemBoss;

            for (int i = 0; i <= 10; i++)
            {
                if (!bossItem && i == tipoItem)
                {
                    _gridItem._itensInvet[tipoItem].GetComponent<SlotItem>()._contadorNumber++;
                    _gridItem._itensInvet[tipoItem].GetComponent<SlotItem>().NumberItem();
                }
                else if (bossItem && (i == 6 || i == 10))
                {
                     _gridItem._itensInvet[tipoItem].GetComponent<Transform>().DOScale(1.44f, 1f);
                }

                else if (bossItem && (i == 7 || i == 8))
                {
                      _gridItem._itensInvet[tipoItem].GetComponent<Transform>().DOScale(1.52f, 1f);
                }

                else if (bossItem && i == 9)
                {
                      _gridItem._itensInvet[tipoItem].GetComponent<Transform>().DOScaleX(1.56f, 1f);
                      _gridItem._itensInvet[tipoItem].GetComponent<Transform>().DOScaleY(1.2f, 1f);
                      _gridItem._itensInvet[tipoItem].GetComponent<Transform>().DOScaleZ(1.5f, 1f);
                }
            }
            other.gameObject.SetActive(false);
            
        }

        //Dialogos com NPCs que dão missão
        //Missao 1 Pegar 8 Copaiba
        if (other.gameObject.CompareTag("NPCMissao1"))
        {
            _gameCtrl._hudCanvas.gameObject.GetComponent<HudInventario>().AbriDialogNPC();
            _btDialogFechar.Select();

            if (_gridItem._itensInvet[0].GetComponent<SlotItem>()._contadorNumber >= 8 && _dialogNPCM._missaoConcluida[0] == false)
            {
                _gridItem._itensInvet[0].GetComponent<SlotItem>()._contadorNumber = _gridItem._itensInvet[0].GetComponent<SlotItem>()._contadorNumber - 5;
                _dialogNPCM._tmpDialogo.text = "" + _dialogNPCM._questNPC[0]._dialogo2;
                _dialogNPCM._missaoConcluida[0] = true;
            }else if (_dialogNPCM._missaoConcluida[0] == false)
            {
                _dialogNPCM._contadorObjetivo = 5;
                _dialogNPCM._tmpDialogo.text = "" + _dialogNPCM._questNPC[0]._dialogo1;
            }
        }
        //Missao 2 matar monstros
        if (other.gameObject.CompareTag("NPCMissao2"))
        {
            _gameCtrl._hudCanvas.gameObject.GetComponent<HudInventario>().AbriDialogNPC();
            _btDialogFechar.Select();

            if (_dialogNPCM._contadorObjetivo == 0 && _dialogNPCM._missaoConcluida[1] == false)
            {
                _dialogNPCM._tmpDialogo.text = "" + _dialogNPCM._questNPC[1]._dialogo2;
                _dialogNPCM._missaoConcluida[1] = true;
            }
            else if (_dialogNPCM._missaoConcluida[0] == true && _dialogNPCM._missaoConcluida[1] == false)
            {
                _dialogNPCM._tmpDialogo.text = "" + _dialogNPCM._questNPC[1]._dialogo1;
                _dialogNPCM._contadorObjetivo = 10;
            }
        }

        //missão Iara PRIMEIRO FAZER A IARA MORRER NO SCRIPT
        //if (other.gameObject.CompareTag("NPCMissaoIara"))
        //{
        //  _gameCtrl._hudCanvas.gameObject.GetComponent<HudInventario>().AbriDialogNPC();
        //  _btDialogFechar.Select();
        //
        //}

        // por colider com atk da iara

        //entrar na area da iara

        if (other.gameObject.CompareTag("BossFightOn"))
        {
            _gameCtrl._bossOn = true;
        }

        // collieder com inimigo
        if (other.CompareTag("AtkInimigo") && !_checkHitMo)
        {
            _checkHitMo = true;
            _vidaInicialPlayer--;
            _gameCtrl._hudCanvas.GetComponent<VidaHud>().HitSlider();
            
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

        if (other.CompareTag("agua"))
        {
            _vidaInicialPlayer = 0;

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
        }

        if (other.CompareTag("Checkpoint"))
        {
            UnityEngine.Debug.Log(other.transform.localPosition);
            _gameCtrl.CheckpointSalvarPos(other.transform.position);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("BossFightOn"))
        {
            _gameCtrl._bossOn = false;
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
