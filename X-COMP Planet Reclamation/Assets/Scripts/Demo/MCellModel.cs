using UnityEditor;
using UnityEngine;

public class MCellModel : MonoBehaviour
{
    [Min(0)] public int PositionX;
    [Min(0)] public int PositionZ;
    [Min(0)] public int SizeX;
    [Min(0)] public int SizeZ;

    private void Awake()
    {
        gameObject.SetActive(false);
    }

    public void EditWalls()  // editor script
    {
        transform.position = new Vector3(PositionX - 0.5f + SizeX / 2.0f, transform.position.y, PositionZ - 0.5f + SizeZ / 2.0f);
        transform.localScale = new Vector3(SizeX, transform.localScale.y, SizeZ);
    }
}

#if UNITY_EDITOR

[CustomEditor(typeof(MCellModel))]
public class MCellEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        var script = (MCellModel)target;

        script.EditWalls();
    }
}

#endif
