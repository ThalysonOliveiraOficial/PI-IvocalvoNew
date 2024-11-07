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

    public bool _jogoPausado;

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

        _jogoPausado = false;
    }

    // Update is called once per frame
    void Update()
    {//
        _jogoPausado = _contaControl._pause;

        if(_jogoPausado)
        {
            Debug.Log("Jogo pausado");
        }else{
            _rigidbody2.velocity = _move;
        }
        
    }

    public void SetMove(InputAction.CallbackContext value)
    {
        _move = value.ReadValue<Vector3>().normalized * 2.5f;
    }

public void SetPause(InputAction.CallbackContext value)
{
    _contaControl.PausarAbrir();
    
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
                    _contaControl.AbrirPanelVitoria();
                    _contaControl._textvitoria.text = "Player 2";
                    Debug.Log("para o jogo, apresenta tela de jogador 1 vencedor");
                }
                else if (_contaControl.pontosPlayer[0] == 2)
                {
                    _contaControl.AbrirPanelVitoria();
                    _contaControl._textvitoria.text = "Player 1";
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

