using UnityEngine;

public class MShootable : MonoBehaviour
{
    public bool IsLit;
    [SerializeField] GameObject InRangeMarker;

    public void Light()
    {
        IsLit = true;
        InRangeMarker.SetActive(true);
    }

    public void Unlight()
    {
        IsLit = false;
        InRangeMarker.SetActive(false);
    }

    private bool over;
    private void OnMouseEnter()
    {
        over = true;
    }
    private void OnMouseDown()
    {

    }
    private void OnMouseUp()
    {
        if (over && IsLit && MGameLoop.Instance.CurrentState == MGameLoop.GameState.Attack)
        {
            MGameLoop.Instance.StartAction(new MShoot(transform));
        }
    }
    private void OnMouseExit()
    {
        over = false;
    }
}
