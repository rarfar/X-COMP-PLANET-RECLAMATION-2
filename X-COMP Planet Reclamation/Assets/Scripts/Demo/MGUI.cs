using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Experimental.GraphView.GraphView;

public class MGUI : MonoBehaviour
{
    GameObject Button;
    MGameLoop.GameState gameState;

    public void ClickMove()
    {
        if (MGameLoop.Instance.GetCurrentAction() == null)
        {
            MGameLoop.Instance.CurrentState = MGameLoop.GameState.Move;
            MGameLoop.Instance.UnlightTiles();
            MGameLoop.Instance.LightTiles();
            MGameLoop.Instance.UnlightTargets();
        }

    }

    public void ClickAttack()
    {
        if (MGameLoop.Instance.GetCurrentAction() == null)
        {
            MGameLoop.Instance.CurrentState = MGameLoop.GameState.Attack;
            MGameLoop.Instance.UnlightTiles();
            MGameLoop.Instance.LightTiles();
            MGameLoop.Instance.LightTargets();
        }

    }

    public void ClickSwitchCharacter()
    {
        Debug.Log("Switch Character");
        if (MGameLoop.Instance.GetCurrentAction() == null)
        {
            int index = MGameLoop.Instance.CurrentActor.index + 1 >= MGameLoop.Instance.Players.Count ? 0 : MGameLoop.Instance.CurrentActor.index + 1;
            MGameLoop.Instance.ChangePlayer(index);

            MGameLoop.Instance.CurrentState = MGameLoop.GameState.Move;
            MGameLoop.Instance.UnlightTiles();
            MGameLoop.Instance.LightTiles();
            MGameLoop.Instance.UnlightTargets();
        }
    }

    public void ClickEndTurn()
    {
        Debug.Log("End Turn");
        if (MGameLoop.Instance.GetCurrentAction() == null)
        {
            foreach (var p in MGameLoop.Instance.Players)
            {
                p.GetStats().ResetActionUnits();
                p.GetStats().ResetStamina();
            }
            
            MGameLoop.Instance.CurrentActor = (0, MGameLoop.Instance.Enemies[0]);
            MGameLoop.Instance.UnlightTiles();
            MGameLoop.Instance.UnlightTargets();
        }
    }
}
