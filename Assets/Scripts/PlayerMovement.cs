using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
 
    public Transform _orientation;

    float _moveX,_moveZ;

    Vector3 _moveDir;

    [SerializeField] float _jumpHeight = 1f, _moveSpeed = 3.5f, _gravityValue = -9.81f;

    Vector3 _playerVelocity;

    [SerializeField] bool _groundedPlayer;
    [SerializeField ]bool _checkJump;

    [SerializeField] Animator _anim;
    
    CharacterController _controller;

    float _timer;
    [SerializeField] float _timerValue;
    
    private void MovimentoPlayer()
    {
        //orientação do movimento
        _moveDir = _orientation.forward * _moveZ + _orientation.right * _moveX;

        

        //movimento
        _controller.Move(_moveDir * _moveSpeed * Time.deltaTime);


        _anim.SetFloat("Andando", Mathf.Abs(_moveZ + _moveX));
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
