using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HudInventario : MonoBehaviour
{
    [SerializeField] GameControl _gameCtrl;

    public Transform _panelMissao;
    public Transform _TelaTutorial;

    public Transform _panelInvet;

    public Transform _panelDlgQuestNPC;
    public Transform _imgRepsDialgNPC;

    public bool _inventHudAberto;
    public Button _btInvHudInicial;

    //bot�o de confirma��o do panel quando o jogo abre
    public Button _btConfirmStart;

    public Transform _panelConcluirMissao;
    // criar um enumerator coroutine e uma fun��o pra fazer  _panelConcluirMissao aparecer e desaparecer quando uma missao for concluida

    void Start()
    {
        _gameCtrl = Camera.main.GetComponent<GameControl>();
        _inventHudAberto = true;

        AbrirHudTutorial();

    }

    public void AbrirHudInvet()
    {
        _panelInvet.DOScale(1f, .25f);
        _inventHudAberto = true;
        _btInvHudInicial.Select();
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }
    public void FecharHudInvet()
    {
        _panelInvet.DOScale(0f, .25f);
        _inventHudAberto = false;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void AbrirHudTutorial()
    {
        _panelMissao.DOScale(1f,0f);
        _TelaTutorial.DOScale(1f, .25f);
        _inventHudAberto = true;

        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;

        _btConfirmStart.Select();

    }

    public void FecharHudTutorial()
    {
        _TelaTutorial.DOScale(0f, .25f);

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        _inventHudAberto = false;

        _panelMissao.DOScale(0f, .25f);
    }

    public void AbriDialogNPC()
    {
        _panelDlgQuestNPC.DOScale(1, .25f);
        _imgRepsDialgNPC.DOScale(1, 1.25f);
        _inventHudAberto = true;

        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    public void FecharDialogoNPC()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        _inventHudAberto = false;
        _imgRepsDialgNPC.DOScale(0, .12f);
        _panelDlgQuestNPC.DOScale(0, .25f);
    }

    //IEnumerator para tela de conclus�o de miss�o

}
