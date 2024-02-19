using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameControl : MonoBehaviour
{
    public Transform _player;
    public Transform[] _iniMovPos;
    public GameObject InimigosPai;
    
    public GameObject MiraCano;
    public GameObject MiraMarker;
    public GameObject BaladeiraOBJ;

    public Canvas _hudCanvas;

    public void GameReiniciar()
    {
        SceneManager.LoadScene("Mapa_Arborizado");
    }
}


