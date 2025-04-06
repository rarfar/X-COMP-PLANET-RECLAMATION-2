using UnityEngine;



namespace Items
{
    [CreateAssetMenu(fileName = "Item", menuName = "Scriptable Objects/Item")]
    public class Item : ScriptableObject
    {
        [Header("Basic Info")]
        public int id;
        public string itemName;
        public ItemCategory category;
        public Sprite icon;
        public GameObject prefab;
        [Range(0f, 1f)] public float dropChance = 0.3f;

        [Header("Category-Specific")]
        public WeaponCategory weaponCategory;
        public WeaponSubType weaponSubType;
        public ArmorType armorType;
        public ConsumableType consumableType;
        public bool isStackable;
        [Header("Stack-Size")] public int maxStackSize = 1;

        [Header("Universal Stats")]
        public Stats baseStats;
        public bool isUnique;

        public virtual Stats GetCurrentStats()
        {
            return baseStats;
        }
    }
}
