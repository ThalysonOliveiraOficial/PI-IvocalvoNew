using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VidaHud : MonoBehaviour
{
    [SerializeField] Slider _sliderVida;
    [SerializeField] int _life;

    void Start()
    {
        //vida do slider = vida do personagem
        _life = System.Convert.ToInt32(_sliderVida.maxValue);
        _sliderVida.value = _sliderVida.maxValue;
    }



    void Update()
    {


    }

    public void HitSlider()
    {
        _life--;
        _sliderVida.DOValue(_life, .5f, false);
    }
}