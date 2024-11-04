using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AtaqueBoss : MonoBehaviour
{
    public GameControl _gameCtrl;
    public BossMovement _bossmov;
    public int _atkAtual;

    public bool _iara;

    public int _contadorAtk;
    public ParticleSystem _ataque1Spray;
    public ParticleSystem _ataque2Linha;

    Transform _player;

    void Start()
    {
        _gameCtrl = Camera.main.GetComponent<GameControl>();
        _bossmov = GetComponent<BossMovement>();
        _player = _gameCtrl._player;
        _iara = _bossmov._iara;
        _atkAtual = _bossmov._ataque;
        _contadorAtk = 0;

        _ataque1Spray.Stop();
        _ataque2Linha.Stop();
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

            _ataque1Spray.transform.position = new Vector3(_player.position.x - 1.5f, _player.position.y, _player.position.z - 1.5f);
            SprayAtk1();
            _contadorAtk = 0;

        }

        if(_atkAtual == 3 && _contadorAtk == 1)
        {
            _ataque2Linha.transform.position = new Vector3(_player.position.x, _player.position.y, _player.position.z - 5);
            LinhaAtk2();
            _contadorAtk = 0;

        }
    }

    IEnumerator Ataque1Spray()
    {
        _ataque1Spray.Play();
        _ataque1Spray.GetComponent<CapsuleCollider>().enabled = true;
        yield return new WaitForSeconds(1.2f);
        _ataque1Spray.Stop();
        _ataque1Spray.GetComponent<CapsuleCollider>().enabled = false;
    }

    private void SprayAtk1()
    {
        StartCoroutine(Ataque1Spray());
    }

    IEnumerator Ataque2Linha()
    {
        _ataque2Linha.Play();
        _ataque2Linha.GetComponent<BoxCollider>().enabled = true;
        yield return new WaitForSeconds(1.2f);
        _ataque2Linha.Stop();
        _ataque2Linha.GetComponent<BoxCollider>().enabled = false;
    }

    private void LinhaAtk2()
    {
        StartCoroutine(Ataque2Linha());
    }

}
