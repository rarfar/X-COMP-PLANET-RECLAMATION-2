using UnityEngine;

public class MPrefabs : MonoBehaviour
{
    public GameObject Bullet;
    public static MPrefabs Instance; private void Awake() { Instance = this; }
}
