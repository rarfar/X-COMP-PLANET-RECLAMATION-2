using UnityEngine;

public class MWeapon
{
    public int Damage;
    public int Range;

    public static MWeapon Default = new MWeapon(1, 5);

    public MWeapon(int damage, int range)
    {
        Damage = damage;
        Range = range;
    }
}
