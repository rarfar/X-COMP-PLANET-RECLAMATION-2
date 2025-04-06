using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Collections;
using Database;

// Script for controlling game volume settings
// Written by Meixuan Kelly An
public class SettingVolumeChange : MonoBehaviour
{
    [SerializeField] Slider volumeSlider;
    
    void Start()
    {
        if (!PlayerPrefs.HasKey("musicvolume")) 
        {
            PlayerPrefs.SetFloat("musicvolume", 1);
        }
        Load();
    }

    public void ChangeVolume()
    {
        AudioListener.volume = volumeSlider.value;
        Save();
    }
    public void ResetVolume()
    {
        PlayerPrefs.SetFloat("musicvolume", 1);
    }
    private void Save()
    {
        PlayerPrefs.SetFloat("musicvolume", volumeSlider.value);
    }
    private void Load()
    {
        volumeSlider.value = PlayerPrefs.GetFloat("musicvolume");
    }
}
