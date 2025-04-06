using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

public class MSceneResizer : MonoBehaviour
{
    [SerializeField] List<GameObject> parents;
    public void Resize()
    {
        foreach (var parent in parents)
        {
            foreach (Transform obj in parent.transform)
            {
                // do what you want for child objects
                //obj.position = obj.position / 2;
                //obj.localScale = obj.localScale / 2;
            }
        }
    }
}

#if UNITY_EDITOR

[CustomEditor(typeof(MSceneResizer))]
public class MSceneResizerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        var script = (MSceneResizer)target;
        if (GUILayout.Button("Resize and Reposition"))
        {
            script.Resize();
        }
    }
}

#endif
