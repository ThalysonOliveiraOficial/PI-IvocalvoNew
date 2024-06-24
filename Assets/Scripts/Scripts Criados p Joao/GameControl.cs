using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameControl : MonoBehaviour
{
    public Transform _player;
    public Transform _playerObj;
    public Transform[] _iniMovPos;
    public GameObject InimigosPai;
    
    public GameObject Mira;
    public GameObject BaladeiraOBJ;
    public GameObject SaidaTiro;
    public GameObject _municao;

    public Canvas _hudCanvas;
    public Transform _ponteiroMiniMap;

    public PlayerMovement _playerMovScript;

    //Audio
    public List<AudioSource> _somHud;
    public List<AudioSource> _somGame;
    public List<AudioSource> _somMusic;
    public bool _muteHud;
    public bool _muteGame;
    public bool _muteMusic;
    public int _fase;
    public Transform _minicamera;

    public Transform _hudGameOver;
    public Transform _hudMissao;
    public GameObject CamCM;


    //Boss Iara
    public Transform _iaraModelP;



    //

    void Start()
    {

        StopCamPlayer(false);
        Debug.Log(PlayerPrefs.GetInt("StartSalve"));
        if (PlayerPrefs.GetInt("StartSalve") == 0)
        {
            PlayerPrefs.SetInt("StartSalve", 0);
        }


    }

    void Update()
    {
        _minicamera.position = new Vector3(_player.position.x, _minicamera.position.y, _player.position.z);

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

    public void StopCamPlayer(bool value)
    {
        CamCM.SetActive(value);
    }

    public void CheckpointSalvarPos(Vector3 pos)
    {
        PlayerPrefs.SetFloat("posX", pos.x);
        PlayerPrefs.SetFloat("posY", pos.y);
        PlayerPrefs.SetFloat("posZ", pos.z);


        Salvar();
        Debug.Log(PlayerPrefs.GetInt("StartSalve"));

    }

    public void Salvar()
    {
        PlayerPrefs.SetInt("StartSalve", 1);
        PlayerPrefs.SetInt("fase", _fase);

    }


    public void GameReiniciar()
    {
        SceneManager.LoadScene("Mapa_Arborizado");
    }
}


