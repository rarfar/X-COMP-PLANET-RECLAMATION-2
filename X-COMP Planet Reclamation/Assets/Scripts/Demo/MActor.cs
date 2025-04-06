using System;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UIElements;

[Serializable]
public class MActor : MonoBehaviour
{
    [ReadOnly] public MStatsManager statsManager;

    public void SetStatsManager()
    {
        statsManager = GetComponent<MStatsManager>();

    }
    public enum ActorType
    {
        Player,
        Enemy
    }
    
    public MStatsManager GetStats()
    {
        return statsManager;
    }
    public MWeapon Weapon = MWeapon.Default;
    public ActorType Type;


    public MCell Cell { get; set; }
    public Vector2Int Position { get { return new(Mathf.RoundToInt(transform.position.x), Mathf.RoundToInt(transform.position.z)); } }
}
