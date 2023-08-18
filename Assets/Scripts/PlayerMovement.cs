using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.XR;

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
    
    CharacterController _controller;

    float _timer;
    [SerializeField] float _timerValue;

    
    
    private void MovimentoPlayer()
    {
        //orienta��o do movimento
        _moveDir = _orientation.forward * _moveZ + _orientation.right * _moveX;       

        //movimento
        _controller.Move(_moveDir * _moveSpeed * Time.deltaTime);


        _anim.SetFloat("Andando", Mathf.Abs(_moveZ) + Mathf.Abs(_moveX));

        //checkar se o botao de correr foi apertado e mudar o _moveSpeed
        if (_checkRunnig)
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
            _anim.SetFloat("Correndo", _correndo =1);
        }
        else
        {
            _anim.SetFloat("Correndo", _correndo = 0);
        }

        //fazer as anima��es de pulo saberem quando o y do player estiver aumentando, para por a anima��o dele subindo e quando o y estiver diminuindo para por descendo

        _pulando = _playerVelocity.y;

    }

    void GroundCheck()
    {
        //Check chao. se chao y velocity == 0
        _groundedPlayer = _controller.isGrounded;
        if (_groundedPlayer && _playerVelocity.y < 0)
        {
            _playerVelocity.y = 0f;
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

    private void Pulo()
    {
        if (_groundedPlayer  && _checkJump)
        {
            _checkJump = false;
            _playerVelocity.y = _playerVelocity.y + Mathf.Sqrt(_jumpHeight * -2.5f * _gravityValue);

        }
    }
    
    private void Gravidade()
    {
        _playerVelocity.y = _playerVelocity.y + _gravityValue * Time.deltaTime;
        _controller.Move(_playerVelocity * Time.deltaTime);
    }

    private void Start()
    {
        _controller = GetComponent<CharacterController>();
        _timer = _timerValue;
    }

    private void Update()
    {
        //desbugar o pulo
        if (_checkJump)
        {
            _timer -= Time.deltaTime;
            if(_timer < 0)
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

}
