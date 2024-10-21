using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Conta : MonoBehaviour
{   public int _numb1, _numb2, _resp;
    public string _conta;

    public TextMeshPro _textResp;
    GameContaControl _contaControl;
    
    // Start is called before the first frame update
    void Start()
    {
        _resp = _numb1 + _numb2;
        ContaSet("?");
       

        _contaControl = Camera.main.GetComponent<GameContaControl>();

        float r = Random.Range(0.2f, 0.5f);
        Invoke("NumbEnviar", r);
    }

    public void ContaSet(string conta)
    {
        _conta = _numb1 + " + " + +_numb2 + " = " + conta;
        _textResp.text = _conta;
    }

    void NumbEnviar()
    {
        _contaControl._respList.Add(_resp);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
