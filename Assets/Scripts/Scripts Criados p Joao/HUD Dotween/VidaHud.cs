using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VidaHud : MonoBehaviour
{
    public GameControl _gameControl;

    [SerializeField] Slider _sliderVida;
    [SerializeField] float _life;

    void Start()
    {
        _gameControl = Camera.main.GetComponent<GameControl>();

        _sliderVida.maxValue = _gameControl._player.GetComponent<PlayerMovement>()._vidaPlayerMax;

        //vida do slider = vida do personagem
        _life = _sliderVida.maxValue;
        //_life = System.Convert.ToInt32(_sliderVida.maxValue);
        _sliderVida.value = _sliderVida.maxValue;
    }



    void Update()
    {


    }

    public void HitSlider()
    {
        _life = _gameControl._player.GetComponent<PlayerMovement>()._vidaInicialPlayer;
        //_life--;
        _sliderVida.DOValue(_life, .5f, false);
    }
}