using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class MBullet : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject == MGameLoop.Instance.CurrentActor.actor.gameObject || MShoot.CurrentInstance == null)
        {
            // collision with shooter or second collision
            return;
        }
        MShoot.CurrentInstance.Collision = collision.gameObject;
        MGameLoop.Instance.EndAction();
    }
}
