using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CutsceneManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public float changeTime;
    public string sceneName;
    
    void Update()
    {
        changeTime -= Time.deltaTime;
        if ( changeTime < 0 )
        {
            SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
        }    

    }
}
