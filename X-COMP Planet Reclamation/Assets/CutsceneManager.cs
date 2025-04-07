using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CutsceneManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

        SceneManager.LoadScene("V2_Level_0", LoadSceneMode.Single);

    }
}
