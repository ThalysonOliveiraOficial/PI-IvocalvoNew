using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuControl : MonoBehaviour
{
    [SerializeField] List<Transform> _ItensMenu;
    [SerializeField]
    // Start is called before the first frame update
    void Start()
    {
        // StartCoroutine(TimeItens());
    }

    private IEnumerator TimeItens()
    {
        for (int i = 0; i < _ItensMenu.Count; i++)
        {
            _ItensMenu[i].DOScale(1.2f, 0.25f);
            yield return new WaitForSeconds(.25f);
            _ItensMenu[i].DOScale(1f, 0.25f);
        }
    }

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

    // Update is called once per frame
    void Update()
    {

    }
}