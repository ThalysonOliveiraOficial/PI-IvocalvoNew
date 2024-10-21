using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameContaControl : MonoBehaviour
{
    public List<int> _respList;
    public List<blocoNumero> _blocoNueroList;
    // Start is called before the first frame update
    private void Start()
    {
        Invoke("SetblocoNumber", 1);
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
}
