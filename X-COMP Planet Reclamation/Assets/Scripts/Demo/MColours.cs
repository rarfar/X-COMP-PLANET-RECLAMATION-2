using UnityEngine;

public class MColours : MonoBehaviour
{
    public Material CellBase;
    public Material CellLit;
    public Material CellClick;
    public Material CellInRange;

    public static MColours Instance;
    private void Awake()
    {
        Instance = this;
    }
    
}
