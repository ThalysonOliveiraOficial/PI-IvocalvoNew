using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    [SerializeField] CharacterController _controller;
    Vector3 _playerVelocity;
    float _moveZ, _rot, _gravityForce = -9.8f, _playerSpeed;
    [SerializeField] float _playerBaseSpeed = 2f, _turnSpeed = 1f, _jumpHeight = 1f;
    bool _groundedPlayer;
    [SerializeField] Animator _anim;




    //Girar Player
    private void TurnPlayer()
    {
        _rot += Input.GetAxisRaw("Horizontal") * _turnSpeed;
        transform.localEulerAngles = new Vector3(0,_rot,0); 
    }

    //Movimento do Player
    void Movimento()
    {
        //andar
        _moveZ = Input.GetAxisRaw("Vertical");
        _controller.Move(transform.forward * _moveZ * Time.deltaTime * _playerSpeed);
        
        //Correr com shift
        if(Input.GetAxisRaw("Fire3") > 0)
        {
            _playerSpeed = _playerBaseSpeed * 2;
        }
        else
        {
            _playerSpeed = _playerBaseSpeed;
        }

        _anim.SetFloat("Andar", _moveZ);
    }
    void Gravidade()
    {//gravidade
        _playerVelocity.y = _playerVelocity.y + _gravityForce *Time.deltaTime;
        _controller.Move(_playerVelocity * Time.deltaTime);
    }
    void Pulo()
    {
        //Check se Player esta no chao
        _groundedPlayer = _controller.isGrounded;
        if(_groundedPlayer && _playerVelocity.y < 0)
        {
            _playerVelocity.y = 0f;
            Debug.Log("chao");
        }
        //pulo
        if(Input.GetAxisRaw("Jump") > 0 && _groundedPlayer)
        {
            _playerVelocity.y = _playerVelocity.y + Mathf.Sqrt(_jumpHeight * -3 * _gravityForce);
        }
    }

    void Start()
    {
        
    }

    void Update()
    {
        TurnPlayer();
        Gravidade();
        Pulo();
        Movimento();
    }
}
