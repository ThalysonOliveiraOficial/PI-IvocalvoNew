using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    public AudioSource AudioSourceMusicadefundo;
    public AudioClip[] MusicasdeFundo;
    // Start is called before the first frame update
    void Start()
    {
        AudioClip MusicasdeFundoAmbiente = MusicasdeFundo[0];
        AudioSourceMusicadefundo.clip = MusicasdeFundoAmbiente;
        AudioSourceMusicadefundo.Play();

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
