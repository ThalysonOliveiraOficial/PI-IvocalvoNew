using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControleInimigo : MonoBehaviour
{
    [SerializeField] UnityEngine.AI.NavMeshAgent _agent;
    [SerializeField] Transform[] _pos;
    [SerializeField] int _numberPos;
    [SerializeField] bool _checkPos;
    void Start()
    {
        _agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        _agent.destination = _pos[_numberPos].transform.position;

        //remainingDistance para medir a distancia entre o inimigo e o alvo
        //boleana que usamos para entrar somente uma vez no if
        //Invoke � a fun��o nativa para delay da fun��o
        //_numberPos numero da posi��o que tem q seguir

        if (_agent.remainingDistance < 5 && _checkPos == false)
        {
            _checkPos = true;
            _numberPos++;
            Invoke("TimeCheckPos", 1f);
        }
        if (_numberPos > 3)
        {
            _numberPos = 0;
        }
    }

    void TimeCheckPos()
    {
        if (_checkPos)
        {
            _checkPos = false;
        }
    }

}
