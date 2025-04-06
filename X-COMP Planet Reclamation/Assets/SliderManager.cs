using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Collections;
using Database;

public class SliderManager : MonoBehaviour
{

    public Slider musicSlider;
    
    public void ChangeVolume ()
    {
        AudioManager.instance.ChangeMusicVolume(musicSlider.value);
    }
}
