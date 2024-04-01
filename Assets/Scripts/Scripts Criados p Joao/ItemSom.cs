using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSom : MonoBehaviour
{
    public bool _somHud;
    public bool _somMusic;
    public bool _somGame;
    public GameControl _gameCtrl;
    public AudioSource _audioSource;

    void Start()
    {
        _gameCtrl = Camera.main.GetComponent<GameControl>();
        _audioSource = GetComponent<AudioSource>();


        if (_somHud)
        {
            _gameCtrl._somHud.Add(_audioSource);
        }
        else if (_somMusic)
        {
            _gameCtrl._somMusic.Add(_audioSource);
        }
        else if (_somGame)
        {
            _gameCtrl._somGame.Add(_audioSource);
        }
    }
    void Update()
    {
        
    }
}
