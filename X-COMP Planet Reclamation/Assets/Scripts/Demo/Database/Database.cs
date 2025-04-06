//using UnityEngine;
using System;
using UnityEngine;
using UnityEngine.Rendering;

namespace Database
{

    [Serializable]
    public class Character
        {
            public string name;
            public CStats stats;

            Weapon weaponL;
            Weapon weaponR;

            Equipment eqpmtHead;
            Equipment eqpmtBody;
            Equipment eqpmtCape;
            Equipment eqpmtPants;
            Equipment eqpmtLArm;
            Equipment eqpmtRArm;
            Equipment eqpmtLLeg;
            Equipment eqpmtRLeg;
            Equipment eqpmtLHand;
            Equipment eqpmtRHand;
            Equipment eqpmtLFoot;
            Equipment eqpmtRFoot;

        public Character(string name, CStats stats, Weapon weaponL, Weapon weaponR, Equipments equipments)
        {
            this.name = name;
            this.stats = stats;
            this.weaponL = weaponL;
            this.weaponR = weaponR;
            this.eqpmtHead = equipments.eqpmtHead;
            this.eqpmtBody = equipments.eqpmtBody;
            this.eqpmtCape = equipments.eqpmtCape;
            this.eqpmtPants = equipments.eqpmtPants;
            this.eqpmtLArm = equipments.eqpmtLArm;
            this.eqpmtRArm = equipments.eqpmtRArm;
            this.eqpmtLLeg = equipments.eqpmtLLeg;
            this.eqpmtRLeg = equipments.eqpmtRLeg;
            this.eqpmtLHand = equipments.eqpmtLHand;
            this.eqpmtRHand = equipments.eqpmtRHand;
            this.eqpmtLFoot = equipments.eqpmtLFoot;
            this.eqpmtRFoot = equipments.eqpmtRFoot;
        }
    }

    [Serializable]
    public class Equipments
    {
        public Equipment eqpmtHead;
        public Equipment eqpmtBody;
        public Equipment eqpmtCape;
        public Equipment eqpmtPants;
        public Equipment eqpmtLArm;
        public Equipment eqpmtRArm;
        public Equipment eqpmtLLeg;
        public Equipment eqpmtRLeg;
        public Equipment eqpmtLHand;
        public Equipment eqpmtRHand;
        public Equipment eqpmtLFoot;
        public Equipment eqpmtRFoot;

        public Equipments()
        {

        }
    }

    [Serializable]
    public class CStats
        {
        public string playerName;
        public int currentLevel;
        public int totalEXP;
        public int maxHealth;
        public int maxStamina;
        public int experienceGiven;
        public int maxActionUnits;
        public int baseAccuracy;
        //int baseDamage;
        //int currentArmor;
        //int currentAccuracy;
        //int maxArmor;

        public int currentHealth;
        public int currentStamina;
        public int currentActionsUnits;

        public CStats(string playerName, int currentLevel, int totalEXP, int maxHealth, int maxStamina,
                    int experienceGiven, int maxActionUnits, int baseAccuracy, int currentHealth,
                    int currentStamina, int currentActionsUnits)
        {
            this.playerName = playerName;
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

    [Serializable]
    public class Enemy
        {
            public int id;
            public string type;
            EStats stats;

            Weapon weaponL;
            Weapon weaponR;

            Equipment eqpmtHead;
            Equipment eqpmtBody;
            Equipment eqpmtCape;
            Equipment eqpmtPants;
            Equipment eqpmtLArm;
            Equipment eqpmtRArm;
            Equipment eqpmtLLeg;
            Equipment eqpmtRLeg;
            Equipment eqpmtLHand;
            Equipment eqpmtRHand;
            Equipment eqpmtLFoot;
            Equipment eqpmtRFoot;

        public Enemy(int id, string type, EStats stats, Weapon weaponL, Weapon weaponR, Equipments equipments)
        {
            this.id = id;
            this.type = type;
            this.stats = stats;
            this.weaponL = weaponL;
            this.weaponR = weaponR;
            this.eqpmtHead = equipments.eqpmtHead;
            this.eqpmtBody = equipments.eqpmtBody;
            this.eqpmtCape = equipments.eqpmtCape;
            this.eqpmtPants = equipments.eqpmtPants;
            this.eqpmtLArm = equipments.eqpmtLArm;
            this.eqpmtRArm = equipments.eqpmtRArm;
            this.eqpmtLLeg = equipments.eqpmtLLeg;
            this.eqpmtRLeg = equipments.eqpmtRLeg;
            this.eqpmtLHand = equipments.eqpmtLHand;
            this.eqpmtRHand = equipments.eqpmtRHand;
            this.eqpmtLFoot = equipments.eqpmtLFoot;
            this.eqpmtRFoot = equipments.eqpmtRFoot;
        }
    }

    [Serializable]
    public class EStats
        {
            public int level; //Level
            public int AU; //Action units
            public int health;
            public int stamina;
            public int accuracy;
            public int recoverySpeed;
            public int expGiven;

            public EStats(int level, int aU, int health, int stamina, int accuracy, int recoverySpeed, int expGiven)
        {
            this.level = level;
            AU = aU;
            this.health = health;
            this.stamina = stamina;
            this.accuracy = accuracy;
            this.recoverySpeed = recoverySpeed;
            this.expGiven = expGiven;
        }
    }

    [Serializable]
    public class Weapon
        {
        public int id;
        public int masterId;
        public int ammo;
        public int capacity;
        public int grip;
        public string type;
        public int damage;
        public int size;
        public int weight;
        public int accuracy;

            public Weapon(int id, int masterId, int ammo, int capacity, int grip, string type, int damage, int size, int weight, int accuracy) //: this(id, masterId)
        {
            this.id = id;
            this.masterId = masterId;
            this.ammo = ammo;
            this.capacity = capacity;
            this.grip = grip;
            this.type = type;
            this.damage = damage;
            this.size = size;
            this.weight = weight;
            this.accuracy = accuracy;
        }
    }

    [Serializable]
    public class Equipment
        {
            public int id;
            public int masterId;
            public int defense;
            public int damage;
            public int wear;

        public Equipment(int id, int masterId, int defense, int damage, int wear)
        {
            this.id = id;
            this.masterId = masterId;
            this.defense = defense;
            this.damage = damage;
            this.wear = wear;
        }
    }

    [Serializable]
    public class Inventory
        {
            public int id;
            public Database.Equipment[] equipment;
            public Database.Weapon[] weapons;
            public int maxEquipment;
            public int maxWeapons;

        public Inventory(int id, Database.Equipment[] equipment, Database.Weapon[] weapons, int maxEquipment, int maxWeapons)
        {
            this.id = id;
            this.equipment = equipment;
            this.weapons = weapons;
            this.maxEquipment = maxEquipment;
            this.maxWeapons = maxWeapons;
        }
    }

    [Serializable]
    public class Save
    {
        public int id;
        public int level;
        public Character[] characters;
        public Inventory[] inventory;
        private int v1;
        private int v2;

        public Save(int id, int level, Character[] characters, Inventory[] inventory)
        {
            this.id = id;
            this.level = level;
            this.characters = characters;
            this.inventory = inventory;
        }
    }
}
