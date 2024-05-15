using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EstaminaHud : MonoBehaviour
{
    [SerializeField] Slider _sliderEstamina;

    public float timerTotal = 15;
    public float oldTimer;
    public int _estaminaStatus;

    void Start()
    {
        oldTimer = timerTotal;
        _sliderEstamina.maxValue = timerTotal;
        _sliderEstamina.value = timerTotal;
    }

    void Update()
    {
        if (_estaminaStatus == 1) //Ativar contagem (drenagem do slider)
        {
            oldTimer -= Time.deltaTime * 3;
            _sliderEstamina.value = oldTimer;

            if(oldTimer < 0)
            {
                _estaminaStatus = 2;
            }
        }
        else if(_estaminaStatus == 2) //Ativar contagem (regeneração do slider)
        {
            oldTimer += Time.deltaTime * 2f;
            _sliderEstamina.value = oldTimer;

            if (oldTimer > _sliderEstamina.maxValue)
            {
                _estaminaStatus = 0;
            }
        }

    }
}
