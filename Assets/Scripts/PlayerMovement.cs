using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    float _moveSpeed;
    
    public Transform _orientation;

    float _hInput;
    float _vInput;
    float _runInput;

    Vector3 _moveDir;

    [SerializeField] float _jumpHeight = 1f;
    Vector3 _playerVelocity;
    bool _groundedPlayer;
    float _gravityValue = -18f;
    bool _isRunning;
    bool _isMoving;

    [SerializeField] Animator _anim;
    [SerializeField] Rigidbody _rb;
    [SerializeField] CharacterController _controller;


    private void MeuInput()
    {
        _hInput = Input.GetAxisRaw("Horizontal");
        _vInput = Input.GetAxisRaw("Vertical");
        _runInput = Input.GetAxisRaw("Fire3");
    }

    private void MovimentoPlayer()
    {
        //Check chao
        _groundedPlayer = _controller.isGrounded;
        if (_groundedPlayer && _playerVelocity.y < 0)
        {
            _playerVelocity.y = 0f;
        }

        _moveDir = _orientation.forward * _vInput + _orientation.right * _hInput;

        //checkar se esta andando
        if(_hInput != 0 || _vInput != 0)
        {
            _isMoving = true;
        }
        else if(_hInput == 0 && _vInput == 0)
        {
            _isMoving = false;
        }

        //Correr
        if (_runInput > 0)
        {
            _moveSpeed = 5f;
            _isRunning = true;
        }
        else
        {
            _moveSpeed = 3.8f;
            _isRunning = false;
        }

        _controller.Move(_moveDir * _moveSpeed * Time.deltaTime);

        _anim.SetBool("Andando", _isMoving);
        _anim.SetBool("Correndo", _isRunning);
    }

    private void Pulo()
    {
        if (Input.GetAxisRaw("Jump") > 0 && _groundedPlayer)
        {
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
        _rb.freezeRotation = true;
    }

    private void Update()
    {
        MeuInput();
        Gravidade();
        MovimentoPlayer();
        Pulo();
    }

}
