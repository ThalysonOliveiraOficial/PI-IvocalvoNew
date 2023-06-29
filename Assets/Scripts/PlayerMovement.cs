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

    Vector3 _moveDir;

    [SerializeField] float _jumpHeight = 1f;
    Vector3 _playerVelocity;
    bool _groundedPlayer;
    float _gravityValue = -9.8f;

    [SerializeField] Rigidbody _rb;
    [SerializeField] CharacterController _controller;

    private void MeuInput()
    {
        _hInput = Input.GetAxisRaw("Horizontal");
        _vInput = Input.GetAxisRaw("Vertical");
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

        //Correr
        if (Input.GetAxisRaw("Fire3") > 0)
        {
            _moveSpeed = 7.6f;
        }
        else
        {
            _moveSpeed = 3.8f;
        }

        _controller.Move(_moveDir * _moveSpeed * Time.deltaTime);
    }

    private void Pulo()
    {
        if (Input.GetAxisRaw("Jump") > 0 && _groundedPlayer)
        {
            _playerVelocity.y = _playerVelocity.y + Mathf.Sqrt(_jumpHeight * -3f * _gravityValue);
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
