using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HudInventario : MonoBehaviour
{
    [SerializeField] GameControl _gameCtrl;

    public Transform _panelMissao;
    public Transform _TelaMissaoIara;

    public Transform _panelInvet;

    void Start()
    {
        _gameCtrl = Camera.main.GetComponent<GameControl>();

        AbrirHudMissao();

    }

    public void AbrirHudInvet()
    {
        _panelInvet.DOScale(1f, .25f);

        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }
    public void FecharHudInvet()
    {
        _panelInvet.DOScale(0f, .25f);

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void AbrirHudMissao()
    {
        _panelMissao.DOScale(1f,0f);
        _TelaMissaoIara.DOScale(1f, .25f);

        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;

    }

    public void FecharHudMissao()
    {
        _TelaMissaoIara.DOScale(0f, .25f);

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        _panelMissao.DOScale(0f, .25f);
    }
}
