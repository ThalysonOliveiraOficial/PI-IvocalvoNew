using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DialogNPCMissao : MonoBehaviour
{
    public TextMeshProUGUI _tmpDialogo;
    public List<Dialogos> _questNPC = new List<Dialogos>();
    public bool _missaoOn;
    public int _contadorObjetivo;
    public int _contadorOrdemQuest;
    public List<bool> _missaoConcluida = new List<bool>();

    void Start()
    {
        _contadorOrdemQuest = 1;
    }

}
