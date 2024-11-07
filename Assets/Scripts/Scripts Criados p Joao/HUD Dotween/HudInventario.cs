using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

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

    public Transform _panelIaraVida;
    public Slider _vidaIaraSlider;

    public Transform _panelVitoriaIara;
    public Button _vitoriaVoltarMenu;

    //panel para começar multiplayer
    public Transform _panelDialogMult;
    public Button _btMultDialog; 

    //
    [SerializeField] private Transform _panelLoading;
    [SerializeField] private Slider _sliderLoading;
    [SerializeField] private TextMeshProUGUI _textoCarregamento;

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
        _panelMissao.DOScale(1f, 0f);
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

    public void SelecionarBTMissao()
    {
        _btInvHudInicial.Select();
    }
    //IEnumerator para tela de conclus�o de miss�o
    IEnumerator ConcluirMissao()
    {
        _panelConcluirMissao.DOScale(1, .7f);
        yield return new WaitForSeconds(4.4f);
        _panelConcluirMissao.DOScale(0, .7f);
    }

    public void MissaoConcluir()
    {
        ConcluirMissao();
    }

    public void BossIaraOn()
    {
        _panelIaraVida.DOScale(1, 1.58f);

    }
    public void BossIaraOff()
    {
        _panelIaraVida.DOScale(0, .5f);
    }

    public void VitoriaIara()
    {
        _panelVitoriaIara.DOScale(1f, 0.5f);
        _inventHudAberto = true;
        _vitoriaVoltarMenu.Select();
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }


    //loading menu
    
    private IEnumerator CarregarMenu(){
        AsyncOperation _asyncOperation =  SceneManager.LoadSceneAsync("Menu");
        while(!_asyncOperation.isDone){
            _sliderLoading.value =_asyncOperation.progress;
            _textoCarregamento.text = "Carregando: "+ (_asyncOperation.progress * 100f) +"%";
            yield return null;
        }
    }

    public void VoltarProMenu()
    {
        _panelLoading.gameObject.SetActive(true);
        StartCoroutine(CarregarMenu());
    }

    //loading multiplayer

    public void IniciarMultiiplayer()
    {
        _panelLoading.gameObject.SetActive(true);
        StartCoroutine(CarregarMultiplayer());
    }

    private IEnumerator CarregarMultiplayer(){
        AsyncOperation _asyncOperation =  SceneManager.LoadSceneAsync("Multiplayer");
        while(!_asyncOperation.isDone){
            _sliderLoading.value =_asyncOperation.progress;
            _textoCarregamento.text = "Carregando: "+ (_asyncOperation.progress * 100f) +"%";
            yield return null;
        }
    }

    public void MultiplayerDialogo()
    {
        _panelDialogMult.DOScale(1f, 0.5f);
        _inventHudAberto = true;
        _btMultDialog.Select();
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    public void FecharMultiplayerDialogo()
    {
        _inventHudAberto = false;
        _panelDialogMult.DOScale(0f, 0.5f);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
}
