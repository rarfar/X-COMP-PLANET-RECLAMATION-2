using UnityEditor;
using UnityEngine;

public class MWallModel : MonoBehaviour
{
    public enum Direction
    {
        X,
        Z
    }

    public Direction WallDirection;
    [Min(0)] public int Position;
    [Min(0)] public int Start;
    public int End;

    private void Awake()
    {
        gameObject.SetActive(false);
    }

    public void EditWalls()  // editor script
    {
        if (End < Start)
        {
            End = Start;
        }

        int size = End - Start + 1;
        if (WallDirection == Direction.X)
        {
            transform.position = new Vector3(Start + (End - Start) / 2.0f, transform.position.y, Position + 0.5f);
            transform.localScale = new Vector3(size, transform.localScale.y, 0.15f);
        }
        else
        {
            transform.position = new Vector3(Position + 0.5f, transform.position.y, Start + (End - Start) / 2.0f);
            transform.localScale = new Vector3(0.15f, transform.localScale.y, size);
        }
    }
}

#if UNITY_EDITOR

[CustomEditor(typeof(MWallModel))]
public class MWallEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        var script = (MWallModel)target;
        
        script.EditWalls();
    }
}

#endif
