using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Unit Stats")]
public class UnitStats : ScriptableObject
{
    [Header("General")]
    public int unitLevel = 1;
    public int totalEXP = 0;
    public int experienceGiven = 0;


    [Header("Health & Stamina")]
    public int baseHealth = 6;
    public int baseStamina = 5;
    public int baseHealthRecovery = 0;

    [Header("Combat")]
    public int baseAccuracy = 50;
    public int baseActionUnits = 2;
    public int baseDamage = 1;
    public int baseArmor = 0;
    // range & penetration & fire rate to be added later (maybe)
}

