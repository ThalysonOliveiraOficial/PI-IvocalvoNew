using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class HitInimigo : MonoBehaviour
{

    public ControleInimigo _ctrlInimigo;
    [SerializeField] ParticleSystem _porradaHit;
    [SerializeField] ParticleSystem _mortePart;
    [SerializeField] ParticleSystem _Ressurgir;

    public SkinnedMeshRenderer _renderIni;
    public Collider _colliderIni;

    private GameControl _gameControl;
    
     
    void Start()
    {
        _ctrlInimigo = GetComponent<ControleInimigo>();
        _colliderIni = GetComponent<Collider>();
        _gameControl = Camera.main.GetComponent<GameControl>();

    }

    
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("AtaqueDistancia"))
        {
            other.gameObject.SetActive(false);
            _ctrlInimigo._iniLife--;

            if(_ctrlInimigo._iniLife >= 1)
            {
                _ctrlInimigo._hitCheck = true;
                StartCoroutine(HitPorrada());
            }
            

            if ( _ctrlInimigo._iniLife == 0)
            {
                if(_gameControl._contKillInimigo > 0)
                {
                    _gameControl._contKilDecre = _gameControl._contKilDecre + 1;
                    _gameControl._contKillInimigo = _gameControl._contKillInimigo - 1;
                    Debug.Log("contador monstros: " +_gameControl._contKillInimigo);
                }
                
                StartCoroutine(Morte());
            }
        }
    }

    IEnumerator HitPorrada()
    {
        _porradaHit.gameObject.SetActive(true);
        yield return new WaitForSeconds(1);
        _porradaHit.gameObject.SetActive(false);
    }

    IEnumerator Morte()
    {
        _ctrlInimigo._agent.velocity = new Vector3(0, 0, 0);
        _ctrlInimigo._deathCheck = true;
        yield return new WaitForSeconds(4.3f);
        _colliderIni.enabled = false;
        _renderIni.enabled = false;
        _mortePart.gameObject.SetActive(true);
        yield return new WaitForSeconds(1);
        _mortePart.gameObject.SetActive(false);
        transform.gameObject.SetActive(false);
    }

    IEnumerator RestartTime()
    {
        if(_ctrlInimigo != null)
        {
            _ctrlInimigo._deathCheck = false;
            _Ressurgir.gameObject.SetActive(true);
            yield return new WaitForSeconds(0.3f);
            _renderIni.enabled = true;
            _colliderIni.enabled = true;
            _Ressurgir.gameObject.SetActive(false);
        }

    }

    public void RestartIni()
    {
        //_iniLife = _iLifeini;
        StartCoroutine(RestartTime());
    }
}

