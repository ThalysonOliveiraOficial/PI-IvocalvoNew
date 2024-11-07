using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuControl : MonoBehaviour
{
    [SerializeField] List<Transform> _ItensMenu;
    [SerializeField] Transform _nomeDoJogo;

    //coisas para o loading
    [SerializeField] private Transform _panelLoading;
    [SerializeField] private Slider _sliderLoading;
    [SerializeField] private TextMeshProUGUI _textoCarregamento;


    
    // Start is called before the first frame update
    void Start()
    {
        ChamarMenu();
        // StartCoroutine(TimeItens());
    }

    private IEnumerator TimeItens()
    {
        for (int i = 0; i < _ItensMenu.Count; i++)
        {
            _nomeDoJogo.DOScale(1f, 0.20f);
            _ItensMenu[i].DOScale(1.2f, 0.25f);
            yield return new WaitForSeconds(.25f);
            _ItensMenu[i].DOScale(1f, 0.25f);
            
        }
    }

    //fazer o nome do jogo balan�ar
    /*
    private IEnumerator NomeDoJogo()
    {
        _nomeDoJogo.DO

    }
    */

    public void ChamarMenu()
    {
        _ItensMenu[0].GetComponent<Button>().Select();
        StartCoroutine(TimeItens());
    }

    public void ItensOFF()
    {
        for (int i = 0; i < _ItensMenu.Count; i++)
        {
            _ItensMenu[i].transform.localScale = Vector3.zero;
        }

    }

    //loading
    public void GameIniciar()
    {
        _panelLoading.gameObject.SetActive(true);
        StartCoroutine(CarregarCena());
    }
    private IEnumerator CarregarCena(){
        AsyncOperation _asyncOperation =  SceneManager.LoadSceneAsync("Mapa_Arborizado");
        while(!_asyncOperation.isDone){
            _sliderLoading.value =_asyncOperation.progress;
            _textoCarregamento.text = "Carregando: "+ (_asyncOperation.progress * 100f) +"%";
            yield return null;
        }
    }

    //Multiplayer
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

    public void GameFechar()
    {
        Application.Quit();
    }
}