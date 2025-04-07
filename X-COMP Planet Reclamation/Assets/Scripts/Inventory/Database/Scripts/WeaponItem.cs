using UnityEngine;

namespace Items
{
    public class WeaponItem : Item
    {
        [SerializeField] private bool isWeapon = true;


        [Header("Durability")]
        public int maxDurability = 100; // Maximum durability of the armor
        public int durability = 100;    // Current durability of the armor

        public bool IsBroken => durability <= 0; // Check if the armor is broken

        public void ReduceDurability(int amount)
        {
            durability -= amount;
            if (durability < 0)
            {
                durability = 0;
                Debug.LogWarning($"{itemName} is broken.");
            }
            if (durability > 0 && durability < 5)
            {
                Debug.LogWarning($"{itemName} is almost borken.");
            }
        }

        public void RepairDurability(int amount)
        {
            durability += amount;
            if (durability > maxDurability)
            {
                durability = maxDurability;
            }
            Debug.Log($"{itemName} repaired by {amount}. Current durability: {durability}");
        }
        public override Stats GetCurrentStats()
        {
            Stats stats = baseStats;
            if (!isWeapon || weaponSubType == 0)
            {
                stats.damage = 0;
                stats.penetration = 0;
                stats.accuracy = 0;
                stats.range = 0;
                stats.ammo = 0;
                stats.weaponDurability = 100;
            }
            switch (weaponCategory)
            {
                case WeaponCategory.Laser:
                    stats.damage *= 1.4f;
                    break;
                case WeaponCategory.Alien:
                    stats.damage *= 2.0f;
                    stats.penetration += 5.5f;
                    break;
            }
            return stats;
        }
    }
}
