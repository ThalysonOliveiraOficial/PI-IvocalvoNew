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
    public Conta _conta;
    GameContaControl _contaControl;
    public int _playerN;
    public Sprite[] SpriteIMG;
    SpriteRenderer _spriteRenderer;
    void Start()
    {
        _rigidbody2 = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _contaControl = Camera.main.GetComponent<GameContaControl>();
        _contaControl.NumberPlayers++;
        _playerN = _contaControl.NumberPlayers;
        if (_playerN == 1)
        {
            _spriteRenderer.sprite = SpriteIMG[0];
        }
        else
        {
            _spriteRenderer.sprite = SpriteIMG[1];
        }
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
        if (collision.gameObject.CompareTag("Conta"))
        {
            _conta = collision.gameObject.GetComponent<Conta>();
            if(_conta._resp == _blocoNumero._numeroBloco)
            {
                
                _conta.ContaSet("" + _blocoNumero._numeroBloco);
                if (_playerN == 1)
                {
                    _contaControl.pontosPlayer[0]++;
                    _contaControl.textMeshProUGUI[0].text = "Pontos " + _contaControl.pontosPlayer[0];
                }
                else
                {
                    _contaControl.pontosPlayer[1]++;
                    _contaControl.textMeshProUGUI[1].text = "Pontos " + _contaControl.pontosPlayer[1];
                }

                if (_contaControl.pontosPlayer[1]==2)
                {
                    Debug.Log("para o jogo, apresenta tela de jogador 1 vencedor");
                }
                else if (_contaControl.pontosPlayer[0] == 2)
                {
                    Debug.Log("para o jogo, apresenta tela de jogador 2 vencedor");
                }
                

            }
            else
            {
                Debug.Log("Errou");
            }
        }
    }
}

