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

    public Transform _LookAtCombate;

    public CameraEstilo _estiloAtual;

    public GameObject _cameraBasica;
    public GameObject _cameraCombate;

    //[SerializeField] GameControl _gameControl;
    

    public enum CameraEstilo
    {
        Basic,
        Combat,
    }

    private void Start()
    {
        //Cursor ficar invisivel
        //Cursor.lockState = CursorLockMode.Locked;
        //Cursor.visible = false;

        //_gameControl = GetComponent<GameControl>();

        TrocarEstiloCamera(CameraEstilo.Basic);
    }


    private void Update()
    {
        CameraRotacao();

        // Liberar e mostrar o Cursor quando o player estiver morto
        if(_player.gameObject.GetComponent<PlayerMovement>()._playerVivo == false)
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;

        }
        
    }

    public void CameraRotacao()
    {
        if (_estiloAtual == CameraEstilo.Basic)
        {
            //Orientação da rotação
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

        //camera de combate
        
        else if (_estiloAtual == CameraEstilo.Combat)
        {
            
            Quaternion targetRotation = Quaternion.Euler(0, transform.eulerAngles.y, 0);
            _player.transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, _rotationSpeed * Time.deltaTime);
            _playerObj.transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, _rotationSpeed * Time.deltaTime);

            /*
            Vector3 _dirCombate = _LookAtCombate.position - new Vector3(transform.position.x, _LookAtCombate.position.y, transform.position.z);
            _orientation.forward = _dirCombate.normalized;

            _playerObj.forward = _dirCombate.normalized;

            */
        }
        
    }

    public void TrocarEstiloCamera(CameraEstilo novoEstilo)
    {
        _cameraBasica.SetActive(false);
        _cameraCombate.SetActive(false);

        if (novoEstilo == CameraEstilo.Basic)
        {
            _cameraBasica.SetActive(true);
        }
        if (novoEstilo == CameraEstilo.Combat)
        {
            _cameraCombate.SetActive(true);
        }

        _estiloAtual = novoEstilo;
    }

}
