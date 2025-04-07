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

    public string NextLevel;

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
            //Players.Add(loadActor("player" + i));
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

        //Transform[] barsInstances = new Transform[5]

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

    public void doNothing(int i)
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
        //SceneManager.LoadScene(MGameLoop.Instance.NextLevel);
    }

    public void loadPlayer(int num)
    {
        CStats stats = Stats[num];
        player.text = stats.playerName;
        exp.text = "EXP: " + stats.totalEXP;

        //TMP_Text actionUnitsText = bar1.Find("TITLE").gameObject.GetComponent<TMP_Text>();
        //TMP_Text actionUnitsValue = bar1value.gameObject.GetComponent<TMP_Text>();

        //TMP_Text healthText = bar2.Find("TITLE").gameObject.GetComponent<TMP_Text>();
        //TMP_Text healthValue = bar2.Find("VALUE").gameObject.GetComponent<TMP_Text>();

        //TMP_Text staminaText = bar3.Find("TITLE").gameObject.GetComponent<TMP_Text>();
        //TMP_Text staminaValue = bar3.Find("VALUE").gameObject.GetComponent<TMP_Text>();

        //TMP_Text accuracyText = bar4.Find("TITLE").gameObject.GetComponent<TMP_Text>();
        //TMP_Text accuracyValue = bar4.Find("VALUE").gameObject.GetComponent<TMP_Text>();

        //TMP_Text healthRecoverySpeedText = bar5.Find("TITLE").gameObject.GetComponent<TMP_Text>();
        //TMP_Text healthRecoverySpeedValue = bar5.Find("VALUE").gameObject.GetComponent<TMP_Text>();

        //actionUnitsText.text = "Action Units";
        barsValue[0].text = stats.currentActionsUnits.ToString();

        //healthText.text = "Health";
        barsValue[1].text = stats.currentActionsUnits.ToString();

        //staminaText.text = "Stamina";
        barsValue[2].text = stats.currentActionsUnits.ToString();

        //accuracyText.text = "Accuracy";
        barsValue[3].text = stats.currentActionsUnits.ToString();

        //healthRecoverySpeedText.text = "Health Recovery Speed";
        barsValue[4].text = stats.currentActionsUnits.ToString();


        //(Button)bar1.Find("MORE").GameObject()
            //.onClick.
            //onClick.AddListener();
            //next.onClick.AddListener(nextClick);
    }

    public void nothing()
    {

    }

    public void valueClickMore(int stat)
    {
        Debug.Log(pointer + " " + stat);
        valueArray[pointer, stat] += 1;
        barsValue[stat].text = valueArray[pointer, stat].ToString();
    }

    public void valueClickLess(int stat)
    {
        valueArray[pointer, stat] -= 1;
        barsValue[stat].text = valueArray[pointer, stat].ToString();
    }

    public MActor loadActor(string file)
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

        MActor data = JsonUtility.FromJson<MActor>(s);
        return data;
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
}
