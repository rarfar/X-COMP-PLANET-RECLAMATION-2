using Database;
using System;
using System.Collections.Generic;
using System.IO;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Int : MonoBehaviour
{
    [SerializeField]
    public int integer;
    public Int(int intgr)
    { 
       this.integer = intgr; 
    }
}
public class MStatsSelection : MonoBehaviour
{

    [SerializeField] Transform bar;
    [SerializeField] TMP_Text lvlup;
    [SerializeField] TMP_Text exp;
    [SerializeField] TMP_Text points;

    [SerializeField] TMP_Text player;
    [SerializeField] Button prev;
    [SerializeField] Button next;

    [SerializeField] Transform menu;

    public List<MActor> Players;
    public List<CStats> Stats;
    private int pointer = 0;
    private int num;
    private Transform[] bars = new Transform[5];
    private int[,] valueArray;

    [SerializeField] TMP_Text[] barsValue;
    [SerializeField] Button[] barsMore;
    [SerializeField] Button[] barsLess;

    [SerializeField] Button finish;

    // Remaining upgrade points for each player.
    Dictionary<String, int> remainingPoints = new Dictionary<string, int>();

    public class StatManager
    {
        int currentLevel;
        int totalEXP;
        int maxHealth;
        int maxStamina;
        int experienceGiven;
        int maxActionUnits;
        int baseAccuracy;
        //int baseDamage;
        //int currentArmor;
        //int currentAccuracy;
        //int maxArmor;

        int currentHealth;
        int currentStamina;
        int currentActionsUnits;

        StatManager(int currentLevel, int totalEXP, int maxHealth, int maxStamina,
                    int experienceGiven, int maxActionUnits, int baseAccuracy, int currentHealth,
                    int currentStamina, int currentActionsUnits)
        {
            this.currentLevel = currentLevel;
            this.totalEXP = totalEXP;
            this.maxHealth = maxHealth;
            this.experienceGiven = experienceGiven;
            this.maxActionUnits = maxActionUnits;
            this.baseAccuracy = baseAccuracy;

            this.currentHealth = currentHealth;
            this.currentStamina = currentStamina;
            this.currentActionsUnits = currentActionsUnits;
        }

    }

    private void Awake()
    {
        num = loadInt("num");

        //valueArray

        for (int i = 0; i < num; i++)
        {
            valueArray = new int[num, 5];

            Debug.Log("num" + i);
            Stats.Add(loadStatsManager("stats" + i));

            for(int j  = 0; j < 5; j++)
            {
                int[] tempArray = new int[5];

                switch(j)
                {
                    case 0:
                        valueArray[i, j] = Stats[i].maxActionUnits;
                        break;
                    case 1:
                        valueArray[i, j] = Stats[i].maxHealth;
                        break;
                    case 2:
                        valueArray[i, j] = Stats[i].maxStamina;
                        break;
                    case 3:
                        valueArray[i, j] = Stats[i].baseAccuracy;
                        break;
                    case 4:
                        valueArray[i, j] = Stats[i].maxHealth;
                        break;
                }
            }
        }

        foreach (CStats a in Stats)
        {
            Debug.Log("stats");
            Debug.Log(a.currentLevel);
        }

    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        next.onClick.AddListener(nextClick);
        prev.onClick.AddListener(prevClick);

        finish.onClick.AddListener(finishSelection);

        loadPlayer(pointer);

        Debug.Log(valueArray[0,0]);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void nextClick()
    {
        if (pointer < num-1) pointer++;
        else pointer = 0;

        Debug.Log(pointer);

        loadPlayer(pointer);

    }

    public void prevClick()
    {
        if (pointer > 0) pointer--;
        else pointer = num - 1;

        Debug.Log(pointer);

        loadPlayer(pointer);
    }

    public void finishSelection()
    {
        for (int i = 0; i < num-1; i++)
        {
            valueArray = new int[num, 5];

            Debug.Log("num" + i);
            Stats.Add(loadStatsManager("stats" + i));

            for (int j = 0; j < 5; j++)
            {
                switch (j)
                {
                    case 0:
                        Stats[i].maxActionUnits = valueArray[i, j];
                        break;
                    case 1:
                        Stats[i].maxHealth = valueArray[i, j];
                        break;
                    case 2:
                        Stats[i].maxStamina = valueArray[i, j];
                        break;
                    case 3:
                        Stats[i].baseAccuracy = valueArray[i, j];
                        break;
                    case 4:
                        Stats[i].maxHealth = valueArray[i, j];
                        break;
                }
            }
            saveStatsManager("stats" + num, Stats[i]);
        }

        SceneManager.LoadScene(MGameLoop.Instance.NextLevel, LoadSceneMode.Single);
    }

    public void valueClickMore(int stat)
    {
        CStats stats = Stats[num];
        if (remainingPoints[stats.playerName] > 0)
        {
            remainingPoints[stats.playerName] -= 1;
            points.text = "Points: " + remainingPoints[stats.playerName].ToString();

            valueArray[pointer, stat] += 1;
            barsValue[stat].text = valueArray[pointer, stat].ToString();
        }

    }

    public void valueClickLess(int stat)
    {
        CStats stats = Stats[num];
        // Can't go over allocated amount for this level up
        if (remainingPoints[stats.playerName] <= MLeveling.GetLevelingPoints(Stats[num].currentLevel))
        {
            remainingPoints[stats.playerName] += 1;
            points.text = "Points: " + remainingPoints[stats.playerName].ToString();

            valueArray[pointer, stat] -= 1;
            barsValue[stat].text = valueArray[pointer, stat].ToString();
        }
        
    }

    public void loadPlayer(int num)
    {
        CStats stats = Stats[num];
        player.text = stats.playerName;
        exp.text = "EXP: " + stats.totalEXP;
        int lvl = stats.currentLevel;
        int totalexp = stats.totalEXP;

        if (MLeveling.CanLevelUp(lvl, totalexp))
        {
            int newLevel = MLeveling.GetLevelFromEXP(totalexp);
            lvlup.text = "Level UP: " + lvl.ToString() + " -> " + newLevel.ToString();
            Stats[num].currentLevel = newLevel;

            if (remainingPoints.TryAdd(stats.playerName, MLeveling.GetLevelingPoints(Stats[num].currentLevel)))
            {
                Debug.Log("Added Character to Stats");
            }
            points.text = "Points: " + remainingPoints[stats.playerName].ToString();

        } else
        {
            lvlup.text = "Level: " + lvl.ToString();
           
            if (remainingPoints.TryAdd(stats.playerName, 0))
            {
                Debug.Log("Added Character to Stats");
            }
            points.text = "Points: 0";
        }

        barsValue[0].text = valueArray[num, 0].ToString();//stats.currentActionsUnits.ToString();

        barsValue[1].text = valueArray[num, 1].ToString();//stats.currentActionsUnits.ToString();

        barsValue[2].text = valueArray[num, 2].ToString();//stats.currentActionsUnits.ToString();

        barsValue[3].text = valueArray[num, 3].ToString();//stats.currentActionsUnits.ToString();

        barsValue[4].text = valueArray[num, 4].ToString();//stats.currentActionsUnits.ToString();
    }

    public CStats loadStatsManager(string file)
    {
        string fileName = Application.persistentDataPath + "/" + file + ".json";

        Debug.Log("LOAD" + fileName);

        string s = "";
        string line = "";
        using (StreamReader sr = File.OpenText(fileName))
        {
            while ((line = sr.ReadLine()) != null)
            {
                Debug.Log(line);
                s += line;
            }
        }

        CStats data = JsonUtility.FromJson<CStats>(s);
        return data;
    }

    public int loadInt(string file)
    {
        string fileName = Application.persistentDataPath + "/" + file + ".json";

        Debug.Log("LOAD" + fileName);

        string s = "";
        string line = "";
        bool line2 = false;
        using (StreamReader sr = File.OpenText(fileName))
        {
            while ((line = sr.ReadLine()) != null)
            {
                if(line2)
                {
                    s = line;
                    break;
                }
                line2 = true;

            }
        }

        string[] parts = s.Split(":");
        return int.Parse(parts[1]);
    }

    public void saveInt(string filename, int integer)
    {
        string data = JsonUtility.ToJson(integer, true);
        string file = Application.persistentDataPath + "/" + filename + ".json";

        if (!File.Exists(file)) File.CreateText(file).Dispose();
        File.WriteAllText(file, data);
    }

    public void saveStatsManager(string file, CStats stats)
    {
        string data = JsonUtility.ToJson(stats, true);
        string path = Application.persistentDataPath + "/" + file + ".json";

        if (!File.Exists(path)) File.CreateText(path);
        File.WriteAllText(path, data);
        Debug.Log(data);
    }
}