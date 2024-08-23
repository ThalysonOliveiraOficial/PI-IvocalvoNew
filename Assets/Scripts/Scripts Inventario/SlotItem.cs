using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SlotItem : MonoBehaviour
{
    public bool _ocupado;
    public int _contadorNumber;
    public TextMeshProUGUI _textNumber;

    public void NumberItem()
    {
        _textNumber.text = "" + _contadorNumber;
    }

}
