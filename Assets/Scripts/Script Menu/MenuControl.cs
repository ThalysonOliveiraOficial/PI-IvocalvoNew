using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuControl : MonoBehaviour
{
    [SerializeField] List<Transform> _ItensMenu;
    [SerializeField] Transform _nomeDoJogo;
    
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
    public void GameIniciar()
    {
        SceneManager.LoadScene("Mapa_Arborizado");
    }

    public void IniciarMultiiplayer()
    {
        SceneManager.LoadScene("Multiplayer");
    }
    public void GameFechar()
    {
        Application.Quit();
    }
}