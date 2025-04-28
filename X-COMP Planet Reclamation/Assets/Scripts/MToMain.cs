using UnityEngine;
using UnityEngine.SceneManagement;

public class MToMain : MonoBehaviour
{
    float scenetime;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        scenetime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time - scenetime > 2)
        {
            SceneManager.LoadScene("Main Menu", LoadSceneMode.Single);
        }
    }
}
