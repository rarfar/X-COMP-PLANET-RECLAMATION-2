using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

public class Shift_assets : MonoBehaviour
{
    [SerializeField] private List<GameObject> parents;

    public void ShiftPositions()
    {
        foreach (var parent in parents)
        {
            if (parent == null) continue;

            foreach (Transform obj in parent.transform)
            {
                obj.position -= new Vector3(0.5f, 0, 0.5f);
            }
        }

        Debug.Log("All child objects shifted by (0.5, 0, 0.5)");
    }
}

#if UNITY_EDITOR

[CustomEditor(typeof(Shift_assets))]
public class Shift_assetsEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        var script = (Shift_assets)target;
        if (GUILayout.Button("Shift Positions"))
        {
            script.ShiftPositions();
        }
    }
}

#endif
