using UnityEngine;

public class MCell : MonoBehaviour
{
    public bool HasPlayer = false;
    private bool IsLit = false;
    
    bool over = false;

    public void Light()
    {
        IsLit = true;
        if (MGameLoop.Instance.CurrentState == MGameLoop.GameState.Move)
        {
            GetComponent<MeshRenderer>().material = MColours.Instance.CellLit;
        }
        else if (MGameLoop.Instance.CurrentState == MGameLoop.GameState.Attack)
        {
            GetComponent<MeshRenderer>().material = MColours.Instance.CellInRange;
        }
    }

    public void Unlight()
    {
        IsLit = false;
        GetComponent<MeshRenderer>().material = MColours.Instance.CellBase;
    }

    private void OnMouseEnter()
    {
        over = true;
        GetComponent<MeshRenderer>().material = MColours.Instance.CellClick;

    }
    
    private void OnMouseDown()
    {
        if (IsLit && !HasPlayer && (MGameLoop.Instance.CurrentState == MGameLoop.GameState.Move))
        {
            GetComponent<MeshRenderer>().material = MColours.Instance.CellClick;

        }
    }
    private void OnMouseUp()
    {
        if (over && IsLit && !HasPlayer && (MGameLoop.Instance.CurrentState == MGameLoop.GameState.Move))
        {
            if (MGameLoop.Instance.CurrentState == MGameLoop.GameState.Move)
            {
                GetComponent<MeshRenderer>().material = MColours.Instance.CellLit;
            }
            else if (MGameLoop.Instance.CurrentState == MGameLoop.GameState.Attack)
            {
                GetComponent<MeshRenderer>().material = MColours.Instance.CellInRange;
            }
            MGameLoop.Instance.StartAction(new MMove(new Vector2Int(Mathf.RoundToInt(transform.position.x), Mathf.RoundToInt(transform.position.z))));
        }
    }
    private void OnMouseExit()
    {
        over = false;
        if (IsLit)
        {
            if (MGameLoop.Instance.CurrentState == MGameLoop.GameState.Move)
            {
                GetComponent<MeshRenderer>().material = MColours.Instance.CellLit;
            }
            else if (MGameLoop.Instance.CurrentState == MGameLoop.GameState.Attack)
            {
                GetComponent<MeshRenderer>().material = MColours.Instance.CellInRange;
            }
        }
        else
        {
            GetComponent<MeshRenderer>().material = MColours.Instance.CellBase;
        }
    }
}
