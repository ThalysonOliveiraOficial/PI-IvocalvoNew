using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameContaControl : MonoBehaviour
{
    public List<int> _respList;
    public List<blocoNumero> _blocoNueroList;
    public int NumberPlayers;
    public int[] pontosPlayer;
    public TextMeshProUGUI[] textMeshProUGUI;


    public Transform _panelVitoria;
    public TextMeshProUGUI _textvitoria;

    public Button _buttonVitoria;
        
    // Start is called before the first frame update
    private void Start()
    {
        Invoke("SetblocoNumber", 1);
        textMeshProUGUI[0].text = "Pontos 0";
        textMeshProUGUI[1].text = "Pontos 0";
    }

    // Update is called once per frame
    public void SetblocoNumber()
    {
        for (int i = 0; i < _blocoNueroList.Count; i++)
        {
            _blocoNueroList[i]._numeroBloco = _respList[i];
            _blocoNueroList[i]._textBloco.text = " " + _respList[i];
        }
    }

    public void AbrirPanelVitoria()
    {
        _panelVitoria.DOScale(1, 1.5f);
        _buttonVitoria.Select();
    }

    public void VoltarJogoPrincipal()
    {
         SceneManager.LoadScene("Menu");
    }
}
