using System;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class MMove : MAction
{
    private float SPEED = 5;

    private Vector2Int Target;
    private List<Vector2Int> Moves;
    private MCell Cell;
    public MMove(Vector2Int target)
    {
        Target = target;
    }
    public int GetCost()
    {
        // Only decrease action units when initial move is done.
        if (MGameLoop.Instance.CurrentActor.actor.GetStats().GetStamina() < MGameLoop.Instance.CurrentActor.actor.GetStats().maxStamina)
        {
            return 0;
        }

        return 1;
    }

    public bool IsValid()
    {
        return true;
    }

    public void Begin()
    {
        var pm = MGameLoop.Instance.PotentialMoves[Target];
        Moves = pm.path;
        Cell = pm.cell;

        MGameLoop.Instance.CurrentActor.actor.Cell.HasPlayer = false;
        MGameLoop.Instance.CurrentActor.actor.Cell = Cell;
        Cell.HasPlayer = true;

        if (MGameLoop.Instance.CurrentActor.actor.Type == MActor.ActorType.Player)
        {
            MGameLoop.Instance.UnlightTiles();
            MGameLoop.Instance.UnlightTargets();
        }
    }

    public void Progress()
    {
        var player = MGameLoop.Instance.CurrentActor.actor.transform;
        var target = new Vector3(Moves[0].x, player.position.y, Moves[0].y);
        var newPos = Vector3.MoveTowards(player.position, target, Time.deltaTime * SPEED);
        player.position = newPos;
        if (player.position == target)
        {
            MGameLoop.Instance.CurrentActor.actor.GetStats().DecreaseStamina();
            Moves.RemoveAt(0);
            if (Moves.Count == 0)
            {
                MGameLoop.Instance.EndAction();
            }
        }
    }

    public void End()
    {

    }
}
