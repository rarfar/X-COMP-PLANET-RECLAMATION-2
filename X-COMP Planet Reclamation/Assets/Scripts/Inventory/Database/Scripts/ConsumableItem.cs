using System.Collections;
using UnityEngine;

namespace Items
{
    public class ConsumableItem : Item
    {
        [SerializeField] private bool isConsumable = true;
        private float cooldownTimer = 0.0f;

        public void Use(MStatsManager statsManager)
        {
            if (isConsumable && (baseStats.durability > 0 || baseStats.maxUses > 0))
            {
                if (cooldownTimer <= 0f)
                {
                    baseStats.durability--;
                    baseStats.maxUses--;
                    cooldownTimer = baseStats.cooldown;

                    switch(consumableType)
                    {
                        case ConsumableType.Medikit:
                            statsManager.ModifyHealth(baseStats.healthRestored);
                            Debug.Log("Health restored by " + baseStats.healthRestored);
                            break;
                        case ConsumableType.EnergyDrink:
                            statsManager.ModifyStamina(baseStats.staminaRestored);
                            Debug.Log("Stamina restored by " + baseStats.staminaRestored);
                            break;
                        case ConsumableType.AdrenalineSyringe:
                            statsManager.StartCoroutine(ApplyTemporaryHealthBoost(statsManager, baseStats.healthIncrease, 120f));
                            break;
                    }
                } 
            }
        }
        private IEnumerator ApplyTemporaryHealthBoost(MStatsManager statsManager, int healthIncrease, float duration)
        {   // if currently applied item is weaker than the new one.
            if (healthIncrease > statsManager.GetCurrentHealthBoost())
            {
                statsManager.maxHealth -= statsManager.GetCurrentHealthBoost();
                statsManager.SetCurrentHealthBoost(healthIncrease);
                statsManager.maxHealth += healthIncrease;
            }
            //duration waits
            yield return new WaitForSeconds(duration);
            statsManager.maxHealth -= healthIncrease;
            statsManager.SetCurrentHealthBoost(0);
            if (statsManager.currentHealth > statsManager.maxHealth)
            {
                statsManager.currentHealth = statsManager.maxHealth;
            }

        }
        public void UpdateCooldown()
        {
            if (cooldownTimer > 0)
            {
                cooldownTimer -= Time.deltaTime;
            }
        }


        public override Stats GetCurrentStats()
        {
            Stats stats = baseStats;
            if (!isConsumable)
            {
                stats.healthRestored = 0;
                stats.staminaRestored = 0;
                stats.maxUses = 0;
                stats.duration = 0;
                stats.healthIncrease = 0;
                stats.cooldown = 0;
            }
            return stats;
        }
    }
}