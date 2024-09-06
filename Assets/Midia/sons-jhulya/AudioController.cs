using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Audiocontroller : MonoBehaviour
{
    public AudioSource AudioSourceMusicadefundo;
    public AudioClip[] Musicasdefundo;
    // Start is called before the first frame update
    void Start()
    {
        int IndexDaMusicadefundo = Random.Range(0,Musicasdefundo.Length);
        AudioClip Musicasdefundo_ = Musicasdefundo[IndexDaMusicadefundo];
        AudioSourceMusicadefundo.clip = Musicasdefundo_;
        AudioSourceMusicadefundo.loop = true;
       AudioSourceMusicadefundo.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
