using UnityEngine;


namespace Items
{
    // Stats Structs
    [System.Serializable]
    public struct Stats
    {
        [Header("Universal")]
        public float weight;         // Common to all items

        [Header("Weapon-Specific")]
        public float damage;         // Base damage
        public float penetration;    // Armor penetration
        public float accuracy;       // Hit chance
        public float range;          // Effective range in tiles
        public int ammo;             // Magazine size
        public float fireRate;       // Shots per turn
        public float weaponDurability;     // Weapon durability

        [Header("Armor-Specific")]
        public float armorValue;     // Damage taken

        [Header("Consumable-Specific")]
        public int healthRestored;   // Healing amount
        public int staminaRestored;  // Stamina restored
        public int maxUses;          // Number of uses
        public int duration;       // Remaining uses or condition
        public float cooldown;       // Time between uses
        public int healthIncrease;   // Temporary health boost
    }

    // Enums for Categorization
    public enum ItemCategory
    {
        Armor,
        Weapon,
        Consumable,
        Backpack
        //HWP // must be treated as unit not an item
    }

    public enum WeaponCategory
    {
        None,
        Standard,  // Early-game human weapons (id: 10-20)
        Laser,     // Mid-tier researched technology (id: 20-25)
        Alien      // Late-game alien tech (id: 30-35)
    }

    public enum WeaponSubType
    {
        None,
        // Standard Weapons
        Pistol,
        Rifle,
        Sniper,
        HeavyCannon,
        AutoCannon,
        RocketLauncher,
        StunRod, //from here, need to make on Unity
        Grenade,
        ProximityGrenade,
        HighExplosive,

        // Laser Technology
        LaserPistol,
        LaserRifle,
        HeavyLaser,
        LaserSniper,

        // Alien Weapons
        PlasmaPistol,
        PlasmaRifle,
        HeavyPlasma,
        PlasmaSniper,
        AlienGrenade, //from here, need to make on Unity
        SmallLauncher,
        BlasterLauncher,
/*
        // HWP Weapons must be treated as character not an item
        TankCannon,
        TankRocketLauncher,
        TankLaserCannon,
        HovertankPlasma,
        HovertankLauncher
*/
        
    }
    // Flyingsuit can be be applied later if time.
    public enum ArmorType {None, Coveralls, PersonalArmor, PowerSuit, FlyingSuit}  //(id:1-4)
    // using medikit and energyDrink only for now
    //SmokeGrenade, MotionScanner, MediKit, ElectroFlare, PsiAmp, MindProbe, Elerium115 
    public enum ConsumableType {None, Medikit, EnergyDrink, AdrenalineSyringe}  //(id:5-9 )
    // Medikit heals 3 health (given that the base helath is 6)
    // EnergyDrink restores 2 stamina (given that the base stamina is 5)
    // AdrenalineSyringe restores 5 stamina and increases health by 1
}