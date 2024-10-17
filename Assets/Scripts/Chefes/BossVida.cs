using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class BossVida : MonoBehaviour
{
    public GameControl _gameControl;

    [SerializeField] Slider _sliderBossVida;
    public float _vidaBoss;
    [SerializeField] float _vidaMaxBoss = 40;


    private void Awake()
    {
        _gameControl = Camera.main.GetComponent<GameControl>();
        _vidaMaxBoss = 40;
        _vidaBoss = 40;
        _sliderBossVida.maxValue = _vidaMaxBoss;
        _sliderBossVida.value = _sliderBossVida.maxValue;
    }

    void Start()
    {
        _sliderBossVida.maxValue = _vidaMaxBoss;
        _sliderBossVida.value = _sliderBossVida.maxValue;

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("AtqueDistancia"))
        {
            _vidaBoss = _vidaBoss - 3;
            HitBossVida();
        }
    }

    private void HitBossVida()
    {
        _sliderBossVida.DOValue(_vidaBoss, .5f, false);
    }
}
