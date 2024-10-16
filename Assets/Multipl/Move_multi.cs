using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class Move_multi : MonoBehaviour
{
    public Vector3 _move;
    Rigidbody2D _rigidbody2;
    public TextMeshPro _textPlayer;
    public blocoNumero _blocoNumero;
    void Start()
    {
        _rigidbody2 = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {//
        _rigidbody2.velocity = _move;
    }

    public void SetMove(InputAction.CallbackContext value)
    {
        _move = value.ReadValue<Vector3>().normalized;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Bloco"))
        {
            _blocoNumero = collision.gameObject.GetComponent<blocoNumero>();
            _textPlayer.text = "" + _blocoNumero._numeroBloco;
        }
    }
}

