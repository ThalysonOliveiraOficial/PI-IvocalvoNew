using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lacos : MonoBehaviour
{
    [SerializeField] int _numb;
    [SerializeField] int[] _numbM;
    [SerializeField] bool[] _checkM;

    [SerializeField] List<int> _numbL;
    [SerializeField] List<float> _ffM;

    // Start is called before the first frame update
    void Start()
    {
       // _numbM[1] = 1;
        for (int i = 0; i < _numbM.Length; i++)
        {
            _numb=1;
            _numbM[i] = 1;
        }

        for (int i = 0; i < 3; i++)
        {
            _numbL.Add(i);
        }
        // Matriz com Boleano chamado _checkM
        // faça todas ficarem true dentro do laço

        for (int i = 0; i < _checkM.Length; i++)
        {
            _checkM[i] = true;
        }
        // Lista com float chamado _ffM
        // faça todas ficarem com valor de "i" dentro do laço

        for (int i = 0; i < 3; i++)
        {
            _ffM.Add(i);
        }
        _ffM[1] = 2.2f;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
