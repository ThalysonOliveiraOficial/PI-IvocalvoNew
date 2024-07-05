using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AtaqueBoss : MonoBehaviour
{
    public GameControl _gameCtrl;
    public BossMovement _bossmov;
    public int _atkAtual;

    public bool _iara;

    public Transform _atkArea1;
    public Transform _atkArea2;
    public int _contadorAtk;

    Transform _player;

    void Start()
    {
        _gameCtrl = Camera.main.GetComponent<GameControl>();
        _bossmov = GetComponent<BossMovement>();
        _player = _gameCtrl._player;
        _iara = _bossmov._iara;
        _atkAtual = _bossmov._ataque;
        _contadorAtk = 0;
    }

    void Update()
    {
        AtaquesArea();

    }

    void AtaquesArea()
    {
        _atkAtual = _bossmov._ataque;

        if (_atkAtual == 2 && _contadorAtk == 1)
        {
            _atkArea1.position = new Vector3(_player.position.x, _player.position.y, _player.position.z);
            Debug.Log(_atkArea1.position);
            _contadorAtk = 0;
        }
    }

}
