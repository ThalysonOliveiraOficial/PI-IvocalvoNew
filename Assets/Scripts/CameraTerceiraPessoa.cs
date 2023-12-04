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

    public Transform _olharCombate;

    public CameraEstilo _estiloAtual;

    public GameObject _cameraBasica;
    public GameObject _cameraCombate;

    public CameraTerceiraPessoa _camScript;

    //public Vector3 _camRota;

    public enum CameraEstilo
    {
        Basic,
        Combat,
    }

    private void Start()
    {
        //Cursor ficar invisivel
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        _camScript = GetComponent<CameraTerceiraPessoa>();

        TrocarEstiloCamera(CameraEstilo.Basic);
    }


    private void Update()
    {
        //if (Input.GetKeyDown(KeyCode.Alpha1)) TrocarEstiloCamera(CameraEstilo.Basic);
        //if (Input.GetKeyDown(KeyCode.Alpha2)) TrocarEstiloCamera(CameraEstilo.Combat);

        //como conservar a rotação da camera, na troca de estilos de camera
        /*
        if (_cameraBasica)
        {
            _camRota =  Camera.main.transform.eulerAngles;
        }
        if (_cameraCombate)
        {
            Camera.main.transform.eulerAngles = _camRota;
        }
        */
        

        //Orientação da rotação
        Vector3 _viewDir = _player.position - new Vector3(transform.position.x, _player.position.y, transform.position.z);
        _orientation.forward = _viewDir.normalized;

        //Rotacionar o Objeto Player
        if(_estiloAtual == CameraEstilo.Basic)
        {
            float _hInput = Input.GetAxisRaw("Horizontal");
            float _vInput = Input.GetAxisRaw("Vertical");
            Vector3 _InputDir = _orientation.forward * _vInput + _orientation.right * _hInput;

            if (_InputDir != Vector3.zero)
            {
                _playerObj.forward = Vector3.Slerp(_playerObj.forward, _InputDir.normalized, Time.deltaTime * _rotationSpeed);
            }
        }else if(_estiloAtual == CameraEstilo.Combat)
        {
            Vector3 _dirCombate = _olharCombate.position - new Vector3(transform.position.x, _olharCombate.position.y, transform.position.z);
            _orientation.forward = _dirCombate.normalized;

            _playerObj.forward = _dirCombate.normalized;
        }
        
    }

    public void TrocarEstiloCamera(CameraEstilo novoEstilo)
    {
        _cameraBasica.SetActive(false);
        _cameraCombate.SetActive(false);

        if (novoEstilo == CameraEstilo.Basic) _cameraBasica.SetActive(true);
        if (novoEstilo == CameraEstilo.Combat) _cameraCombate.SetActive(true);

        _estiloAtual = novoEstilo;
    }

}
