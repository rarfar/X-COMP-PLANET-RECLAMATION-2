using System;
using System.IO;
using Database;
using Items;
using UnityEngine;
using UnityEngine.TestTools;

[Serializable]
public class MStatsManager : MonoBehaviour
{
    // Initilize all stats, even those not used by a particular unit type.

    private void Awake()
    {
        currentLevel = baseStats.unitLevel;
        totalEXP = baseStats.totalEXP;
        maxHealth = baseStats.baseHealth;
        maxStamina = baseStats.baseStamina;
        experienceGiven = baseStats.experienceGiven;
        maxActionUnits = baseStats.baseActionUnits;
        baseAccuracy = baseStats.baseAccuracy;

        currentHealth = maxHealth;
        currentStamina = maxStamina;
        currentActionsUnits = maxActionUnits;
        currentAccuracy = baseAccuracy;
        currentArmor = 0;
        maxArmor = 5;
        if (playerName == null || playerName.Length == 0)
        {
            playerName = "Bob";
        }
        

        if (equippedWeapon != null && equippedWeapon.category == ItemCategory.Weapon)
        {
            //
            //EquipWeapon(equippedWeapon);
            //Debug.Log("Worked");
        }
    }

    [SerializeField] UnitStats baseStats;
    [SerializeField] Item equippedWeapon;
    public string playerName;

    [Header("Experience")]
    public int currentLevel;
    public int totalEXP;
    public int experienceGiven;

    [Header("Health & Stamina")]
    public int maxHealth;
    private int currentHealthBoost = 0;
    [ReadOnly] public int currentHealth;
    public int maxStamina;
    [ReadOnly] public int currentStamina;
    //added armor
    public int maxArmor;
    [ReadOnly] public int currentArmor;

    [Header("Combat")]
    public int baseAccuracy;
    [ReadOnly] public int currentAccuracy;

    public int baseDamage;

    [ReadOnly] public int maxActionUnits;
    [ReadOnly] public int currentActionsUnits;
    // Use these functions to get and set / update values.

    public string GetPlayerName()
    {
        return playerName;
    }
    public int GetTotalEXP()
    {
        return totalEXP;
    }
    public void GiveEXP(int amount)
    {
        totalEXP += amount;
    }
    public void SetEXP(int amount)
    {
        totalEXP = amount;
    }
    public int GetExperienceGiven()
    {
        return experienceGiven;
    }
    public int GetCurrentLevel()
    {
        return currentLevel;
    }
    public bool LevelUp()
    {
        if (MLeveling.CanLevelUp(currentLevel, totalEXP)) {
            currentLevel += 1;
            return true;
        } else
        {
            return false;
        }
    }
    public int GetHealth()
    {
        return currentHealth;
    }
    /*
     * Returns 1 if alive, 0 if dead
     * Negative values == Decrease health
     * Positive values == Increase health
     */
    public int ModifyHealth(int amount)
    {
        if (currentHealth + amount <= 0)
        {
            currentHealth = 0;
            return 0;
        }
        else if (currentHealth + amount >= maxHealth)
        {
            currentHealth = maxHealth;
            return 1;
        }
        else
        {
            currentHealth += amount;
            return 1;
        }
    }
    public void ResetHealth()
    {
        currentHealth = maxHealth;
    }
    //currentHealthBoost() is used to track of
    public int GetCurrentHealthBoost()
    {
        return currentHealthBoost;
    }
    public void SetCurrentHealthBoost(int amount)
    {
        currentHealthBoost = amount;
    }
    public int GetStamina()
    {
        return currentStamina;
    }
    public void ModifyStamina(int amount)
    {
        if (currentStamina + amount <= 0)
        {
            currentStamina = 0;
        }
        else if (currentStamina + amount >= maxStamina)
        {
            currentStamina = maxStamina;
        }
        else
        {
            currentStamina += amount;
        }
    }
    public void ResetStamina()
    {
        currentStamina = maxStamina;
    }
    public void DecreaseStamina()
    {
        currentStamina--;
    }
    public void SetStamina(int amount)
    {
        currentStamina = amount;
    }
    // added armor
    public int GetArmor()
    {
        return currentArmor;
    }
    public void ModifyArmor(int amount)
    {
        if (currentArmor + amount <= 0)
        {
            currentArmor = 0;
        }
        else if (currentArmor + amount >= maxArmor)
        {
            currentArmor = maxArmor;
        }
        else
        {
            currentArmor += amount;
        }
    }
    public int GetAccuracy()
    {
        return currentAccuracy;
    }
    public int GetActionUnits()
    {
        return currentActionsUnits;
    }
    // Decreases the currentActionUnits by 1;
    public void DecreaseActionUnits()
    {
        if (currentActionsUnits > 0)
        {
            currentActionsUnits--;
        }
    }
    public void ResetActionUnits()
    {
        currentActionsUnits = maxActionUnits;
    }
    public void ModifyActionUnits(int amount)
    {
        if (currentActionsUnits + amount <= 0)
        {
            currentActionsUnits = 0;
        }
        else if (currentActionsUnits + amount >= maxActionUnits)
        {
            currentActionsUnits = maxActionUnits;
        }
        else
        {
            currentActionsUnits += amount;
        }
    }
    public void EquipArmor(ArmorItem armor)
    {
        if(armor != null)
        {
            Stats armorStats = armor.GetCurrentStats();
            maxArmor += (int)armorStats.armorValue;
            currentArmor = maxArmor;
        }
    }
    public void UnequipArmor(ArmorItem armor)
    {
        if(armor != null)
        {
            Stats armorStats = armor.GetCurrentStats();
            maxArmor -= (int)armorStats.armorValue;
            if (currentArmor > maxArmor)
            {
                currentArmor = maxArmor;
            }
            Debug.Log($"{armor.itemName} has been unequipped.");
        }
    }
    public void EquipWeapon(WeaponItem weapon)
    {
        if (weapon != null)
        {
            Debug.Log("Got Weapon");
            if (weapon.IsBroken)
            {
                Debug.LogWarning($"Cannot equip broken weapon");
                return;
            }

            Stats weaponStats = weapon.GetCurrentStats();
            baseDamage += (int)weaponStats.damage;
            currentAccuracy += (int)weaponStats.accuracy;
            

            Debug.Log($"Equipped {weapon.itemName}.");
        }
    }

    public void UnequipWeapon()
    {
        if (equippedWeapon != null)
        {
            
            Stats weaponStats = equippedWeapon.GetCurrentStats();
            baseDamage -= (int)weaponStats.damage;
            currentAccuracy -= (int)weaponStats.accuracy;
        }
    }

    public CStats SaveObject()
    {
        CStats obj = new(playerName, currentLevel, totalEXP, maxHealth, maxStamina, experienceGiven, maxActionUnits, baseAccuracy, currentHealth, currentStamina, currentActionsUnits);
        return obj;
    }

    public void loadValues(int v)
    {
        string fileName = Application.persistentDataPath + "/stats" + v + ".json";

        // If a new character is the level, skip.
        if (!File.Exists(fileName))
        {
            return;
        }
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
        Debug.Log("Is Data Null: " + data == null);

        //this.playerName = playerName;
        //this.currentLevel = currentLevel;
        //this.totalEXP = totalEXP;
        //this.maxHealth = maxHealth;
        //this.experienceGiven = experienceGiven;
        //this.maxActionUnits = maxActionUnits;
        //this.baseAccuracy = baseAccuracy;

        //this.currentHealth = currentHealth;
        //this.currentStamina = currentStamina;
        //this.currentActionsUnits = currentActionsUnits;

        currentLevel = data.currentLevel;
        totalEXP = data.totalEXP;
        maxHealth = data.maxHealth;
        //maxStamina = data.baseStamina;
        experienceGiven = data  .experienceGiven;
        //maxActionUnits = data.baseActionUnits;
        baseAccuracy = data.baseAccuracy;

        currentHealth = data.currentHealth;
        currentStamina = data.currentStamina;
        currentActionsUnits = data.currentActionsUnits;
        currentAccuracy = baseAccuracy;
        //currentArmor = 0;
        //maxArmor = 5;
    }

}