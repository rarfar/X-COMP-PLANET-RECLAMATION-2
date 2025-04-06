using System;
using System.IO;
using UnityEngine;
using Database;

public class Saving : MonoBehaviour
{
    public void saveGame()
    {

    }

    public void loadGame()
    {

    }
    public void TestPopulateSave()
    {
        CStats tCStats = new CStats("", 0, 0, 0, 0, 0, 0, 0, 0, 0, 0);
        Weapon tW1 = JsonUtility.FromJson<Weapon>(readFile(Application.dataPath + "/Scripts/Database/Weapons/0.json"));
        Weapon tW2 = JsonUtility.FromJson<Weapon>(readFile(Application.dataPath + "/Scripts/Database/Weapons/1.json"));
        Equipment tE = JsonUtility.FromJson<Equipment>(readFile(Application.dataPath + "/Scripts/Database/Equipment/0.json"));

        Debug.Log(tW1);
        Debug.Log(tW2);
        Debug.Log(tE);

        Equipments tEs = new Equipments();
        tEs.eqpmtHead = tE;
        tEs.eqpmtBody = tE;
        tEs.eqpmtCape = tE;
        tEs.eqpmtPants = tE;
        tEs.eqpmtLArm = tE;
        tEs.eqpmtRArm = tE;
        tEs.eqpmtLLeg = tE;
        tEs.eqpmtRLeg = tE;
        tEs.eqpmtLHand = tE;
        tEs.eqpmtRHand = tE;
        tEs.eqpmtLFoot = tE;
        tEs.eqpmtRFoot = tE;

        Character tCharacter = new Character("John", tCStats, tW1, tW2, tEs);
        Inventory tInventory = new Inventory(0, new Equipment[] { tE }, new Weapon[] { tW1, tW2 }, 10, 10);

        Save tSave = new Save(0, 1, new Character[] { tCharacter}, new Inventory[] {tInventory});

        string tSaveData = JsonUtility.ToJson(tSave, true);

        string saveFilePath = Application.persistentDataPath + "/SaveData.json";
        string saveFilePath2 = Application.dataPath + "Database/SaveData.json";

        if (!File.Exists(saveFilePath)) File.CreateText(saveFilePath);
        //if (!File.Exists(saveFilePath2)) File.CreateText(saveFilePath2);

        File.WriteAllText(saveFilePath, tSaveData);
        //File.WriteAllText(saveFilePath2, tSaveData);

        Debug.Log("JSON: " + tSaveData);

    }

    public void TestLoading()
    {
        string saveFilePath = Application.persistentDataPath + "/SaveData.json";
        //string saveFilePath2 = Application.dataPath + "Database/SaveData.json";

        //Debug.Log(Application.dataPath + "/Scripts/Database/Weapons/0.json");
        Debug.Log(saveFilePath);
        //Weapon tW1 = JsonUtility.FromJson<Weapon>(Application.dataPath + "/Scripts/Database/Weapons/0.json");


        string s = "";
        string line = "";
        using (StreamReader sr = File.OpenText(saveFilePath))
        {
            while ((line = sr.ReadLine()) != null)
            {
                s += line;
            }
        }

        Debug.Log("LOADED FILE: " + s);

        Save saveSlot = JsonUtility.FromJson<Save>(s);
        Debug.Log(saveSlot.level);

        Database.Character[] chars = saveSlot.characters;
        Database.Inventory[] inv = saveSlot.inventory;


    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        TestPopulateSave();
        TestLoading();
    }



    // Update is called once per frame
    void Update()
    {
        
    }



    string readFile(string filePath)
    {
        string s = "";
        string line = "";
        using (StreamReader sr = File.OpenText(filePath))
        {
            while ((line = sr.ReadLine()) != null)
            {
                s += line;
            }
        }
        return s;
    }
}
