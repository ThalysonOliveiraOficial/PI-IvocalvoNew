using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SlotItem : MonoBehaviour
{
    public bool _ocupado;
    public int _slotNumber;
    public TextMeshProUGUI _textNumber;

    public void NumberItem()
    {
        _textNumber.text = "" + _slotNumber;
    }

}
