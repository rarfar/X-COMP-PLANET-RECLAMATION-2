using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Collections;
using Database;


public class SettingManager : MonoBehaviour
{
    private SettingVolumeChange settingVolumeChange;
    [SerializeField] Slider volumeSlider;

    // Quality dropdown
    public void SetQuality(int i)
    {
        QualitySettings.SetQualityLevel(i);
    }

    // Slider related

    public void Toggle()
    {
        AudioManager.instance.ToggleMusic();
    }
    public void Change(float vol)
    {
        AudioManager.instance.ChangeMusicVolume(vol);
    }


    /*
    public void Start()
    {
        if (!PlayerPrefs.HasKey("musicvolume"))
        {
            PlayerPrefs.SetFloat("musicvolume", 1);
        }
        else
        {
            LoadVolume();
        }
    }
    public void ChangeVolume()
    {
        AudioListener.volume = volumeSlider.value;
        SaveVolume();
    }
    private void SaveVolume()
    {
        PlayerPrefs.SetFloat("musicvolume", volumeSlider.value);
    }
    private void LoadVolume()
    {
        volumeSlider.value = PlayerPrefs.GetFloat("musicvolume");
    }
    */
}
