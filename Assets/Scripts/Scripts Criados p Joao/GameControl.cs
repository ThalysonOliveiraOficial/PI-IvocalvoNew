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

    //Audio
    public List<AudioSource> _somHud;
    public List<AudioSource> _somGame;
    public List<AudioSource> _somMusic;
    public bool _muteHud;
    public bool _muteGame;
    public bool _muteMusic;

    //

    void Update()
    {
        for (int i = 0; i < _somHud.Count; i++)
        {
            _somHud[i].mute = _muteHud;
        }

        for (int i = 0; i < _somGame.Count; i++)
        {
            _somGame[i].mute = _muteGame;
        }

        for (int i = 0; i < _somMusic.Count; i++)
        {
            _somMusic[i].mute = _muteMusic;
        }

    }

    public void GameReiniciar()
    {
        SceneManager.LoadScene("Mapa_Arborizado");
    }
}


