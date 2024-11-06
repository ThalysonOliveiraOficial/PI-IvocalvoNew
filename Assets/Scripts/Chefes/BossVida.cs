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
    public bool _1vez;

    void Start()
    {
        _gameControl = Camera.main.GetComponent<GameControl>();
        _vidaMaxBoss = 40;
        _vidaBoss = 40;
        _sliderBossVida = _gameControl.GetComponent<GameControl>()._hudCanvas.GetComponent<HudInventario>()._vidaIaraSlider;
        _1vez = true;
    }

    private void Update()
    {
        if (_1vez && GetComponent<BossMovement>()._player.GetComponent<PlayerMovement>()._desbugarVidaIara)
        {
            _sliderBossVida.maxValue = _vidaMaxBoss;
            _sliderBossVida.value = _sliderBossVida.maxValue;
            _1vez = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("AtaqueDistancia"))
        {
            _vidaBoss = _vidaBoss - 2.5f;
            HitBossVida();
        }
    }

    public void HitBossVida()
    {
        _sliderBossVida.DOValue(_vidaBoss, .5f, false);
    }
}
