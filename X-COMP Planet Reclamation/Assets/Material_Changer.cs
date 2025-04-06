using UnityEngine;
using UnityEditor;

[ExecuteInEditMode] // Allows script to run in Edit Mode
public class MaterialChanger : MonoBehaviour
{
    public string objectName; // Name of the objects to change
    public Material newMaterial; // New material to apply

    [ContextMenu("Change Material in Editor")] // Adds right-click option in Inspector
    public void ChangeMaterial()
    {
        // Find all objects with the specified name
        GameObject[] objectsWithSameName = GameObject.FindObjectsOfType<GameObject>();

        foreach (GameObject obj in objectsWithSameName)
        {
            if (obj.name == objectName) // Check name instead of tag
            {
                Renderer renderer = obj.GetComponent<Renderer>();

                if (renderer != null)
                {
                    Undo.RecordObject(renderer, "Change Material"); // Enable undo support
                    renderer.sharedMaterial = newMaterial; // Use sharedMaterial to change it permanently
                    EditorUtility.SetDirty(renderer); // Mark it as changed
                }
            }
        }
    }
}
