using UnityEngine;

public interface MAction
{
    // None of these methods should be called within the implementing class, t
    // they are only called in the game loop.
    public int GetCost();
    public bool IsValid();
    public void Begin();
    public void Progress();
    public void End();
}
