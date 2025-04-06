using TMPro;
using UnityEngine;

public class GameOverText : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI text;

    // Update is called once per frame
    void Update()
    {
        string s = "";
        int CurrentSceneIndex = MGameLoop.Instance.CurrentSceneIndex;
        // Unique text for each level?
        if (CurrentSceneIndex == 6)
        {
            s = "Your journey ended before it started";
        }
        if (CurrentSceneIndex == 6)
        {
            s = "Your journey just begun but was cut short";
        }
        text.text = s;
    }
}
