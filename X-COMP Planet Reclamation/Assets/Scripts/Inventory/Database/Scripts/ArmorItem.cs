using UnityEngine;

namespace Items
{
    public class ArmorItem : Item
    {
        [SerializeField] private bool isArmor = true;
        

        public override Stats GetCurrentStats()
        {
            Stats stats = baseStats;
            if (!isArmor || armorType == 0)
            {
                stats.armorValue = 0;
            }
            else
            {
                switch (armorType)
                {
                    case ArmorType.FlyingSuit:
                        stats.armorValue += 10.0f;
                        break;
                    case ArmorType.PowerSuit:
                        stats.armorValue += 15.0f;
                        break;
                    case ArmorType.PersonalArmor:
                        stats.armorValue += 5.0f;
                        break;
                    case ArmorType.Coveralls:
                        stats.armorValue += 10.0f;
                        break;
                }
            }
            return stats;
        }
    }
}