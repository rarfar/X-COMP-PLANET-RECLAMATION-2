using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class AudioManager : MonoBehaviour
{
    // Written by Kelly

    public static AudioManager instance;
    public Sound[] musicList, sfxList;
    public AudioSource musicSource, sfxSource;

    // Singleton design
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            // Music continues playing between scenes
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void Start()
    {
        PlayMusic("Temp Theme");
    }
    
    public void PlayMusic (string musicName)
    {
        Sound s = Array.Find(musicList, x => x.name == musicName);

        musicSource.clip = s.audioClip;
        musicSource.Play();
    }

    public void PlaySFX(string sfxName)
    {
        Sound s = Array.Find(sfxList, y => y.name == sfxName);

        sfxSource.PlayOneShot(s.audioClip);
    }

    // Setting and slider related
    public void ToggleMusic ()
    {
        musicSource.mute = !musicSource.mute;
    }
    public void ChangeMusicVolume(float vol)
    {
        musicSource.volume = vol;

    }
}
