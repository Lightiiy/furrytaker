using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    public AudioSource[] soundEffects;
    public AudioSource[] gameMusic;

    private int currentMusicIndex = -1;
    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
        StartMusic(1);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlaySFX(int indexOfSoundEffect)
    {
        soundEffects[indexOfSoundEffect].Stop();
        soundEffects[indexOfSoundEffect].pitch = Random.Range(0.9f, 1.1f);
        soundEffects[indexOfSoundEffect].Play();
    }

    public void StartMusic(int musicIndex)
    {
        if (currentMusicIndex >= 0)
        {
            StopMusic();
        }
        gameMusic[musicIndex].Play();
        currentMusicIndex = musicIndex;
    }

    public void StopMusic()
    {
        gameMusic[currentMusicIndex].Stop();
        currentMusicIndex = -1;
    }
}
