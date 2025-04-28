using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MUI : MonoBehaviour
{
    public void SwitchScene(String s)
    {
        SceneManager.LoadScene(s);
    }
    // SFX for button click
    public void ButtonClick(string s)
    {
        AudioManager.instance.PlaySFX(s);
    }


    public void MainMenu()
    {
        SceneManager.LoadScene("Main Menu");
    }


    // For restarting levels after game over
    public void RetryScene()
    {
        SceneManager.LoadScene(MGameLoop.Instance.CurrentSceneIndex);
        
    }

    public void NextLevel()
    {
        SceneManager.LoadScene(MGameLoop.Instance.CurrentSceneIndex + 1);
    }

    public void Quit()
    {
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
    }
}
