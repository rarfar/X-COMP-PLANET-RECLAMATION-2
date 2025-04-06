using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using TMPro;

public class StatsText : MonoBehaviour
{
    // Written by Kelly & 

    
    [SerializeField] TextMeshProUGUI text;
    // Update is called once per frame
    void Update()
    {
        string s = "";
            
        MStatsManager mS = MGameLoop.Instance.CurrentActor.actor.GetStats();
        s += "Player " + mS.GetPlayerName().ToString() + "\n";
        s += "Health: " + mS.GetHealth().ToString() + "\n";
        s += "Stamina: " + mS.GetStamina().ToString() + "\n";
        s += "Actions Left: " + mS.GetActionUnits().ToString() + "\n";
        s += "Armor: " + mS.GetArmor().ToString() + "\n";
        text.text = s;
    }
}
