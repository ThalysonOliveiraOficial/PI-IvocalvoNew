using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTerceiraPessoa : MonoBehaviour
{
    [Header("References")]
    public Transform _orientation;
    public Transform _player;
    public Transform _playerObj;
    public Rigidbody _rb;

    public float _rotationSpeed;


    private void Start()
    {
        //Cursor ficar invisivel
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }


    private void Update()
    {
        //Orienta��o da rota��o
        Vector3 _viewDir = _player.position - new Vector3(transform.position.x, _player.position.y, transform.position.z);
        _orientation.forward = _viewDir.normalized;

        //Rotacionar o Objeto Player
        float _hInput = Input.GetAxisRaw("Horizontal");
        float _vInput = Input.GetAxisRaw("Vertical");
        Vector3 _InputDir = _orientation.forward * _vInput + _orientation.right * _hInput;

        if (_InputDir != Vector3.zero)
        {
            _playerObj.forward = Vector3.Slerp(_playerObj.forward, _InputDir.normalized, Time.deltaTime * _rotationSpeed);
        }
    }
}
