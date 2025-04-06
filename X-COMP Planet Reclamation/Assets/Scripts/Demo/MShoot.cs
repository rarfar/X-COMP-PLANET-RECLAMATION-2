using UnityEngine;

public class MShoot : MAction
{
    public static MShoot CurrentInstance;  // only used by MBullet class

    float OFFSET = 0.4f;
    float SPEED = 2f;
    float RANGE_BUFFER = 2;

    Transform Target;
    Vector3 Destination;
    Vector3 Velocity;
    Transform Bullet;
    public GameObject Collision;
    MActor shooter;

    public MShoot(Transform target)
    {
        CurrentInstance = this;
        Target = target;
    }

    public int GetCost()
    {
        return 1;
    }

    public bool IsValid()
    {
        return true;
    }

    public void Begin()
    {
        shooter = MGameLoop.Instance.CurrentActor.actor;
        GameObject bullet = Object.Instantiate(MPrefabs.Instance.Bullet);

        // Math to find target based on accuracy
        float targetRadius = Target.localScale.x / 2;
        float bulletRadius = bullet.transform.localScale.x / 2;
        float hitOffset = targetRadius + bulletRadius;
        float missOffset = hitOffset / (shooter.GetStats().GetAccuracy() / 100.0f);
        float targetOffset = Random.Range(-missOffset, missOffset);
        Vector3 shootDirection = Target.position - shooter.transform.position;
        Vector3 offsetDirection = Vector3.Cross(shootDirection, Vector3.up).normalized;


        
        Destination = Target.position + offsetDirection * targetOffset;
        bullet.transform.position = Vector3.MoveTowards(shooter.transform.position, Target.position, OFFSET); // offset the bullet in front of the player and towards the intended target
        Velocity = (Destination - bullet.transform.position).normalized * SPEED;
        Bullet = bullet.transform;
    }

    public void Progress()
    {
        Bullet.position += Velocity * Time.deltaTime;

        // out of bounds or out of range
        if (Bullet.transform.position.x > MGameLoop.Instance.Grid.GetLength(0) ||
            Bullet.transform.position.z > MGameLoop.Instance.Grid.GetLength(1) ||
            Bullet.transform.position.x < -1 || Bullet.transform.position.z < -1 ||
            Vector3.Distance(Bullet.transform.position, MGameLoop.Instance.CurrentActor.actor.transform.position) > MGameLoop.Instance.CurrentRange + RANGE_BUFFER)
        {
            Object.Destroy(Bullet.gameObject);
            MGameLoop.Instance.EndAction();
        }
    }

    public void End()
    {
        if (Collision != null) // bullet did not reach out of bounds/range
        {
            Object.Destroy(Bullet.gameObject);
            if (Collision.GetComponent<MActor>() != null)
            {
                var target = Collision.GetComponent<MActor>();
                // reduce armor first then remove armor
                if (target.GetStats().GetArmor() == 0)
                {
                    target.GetStats().ModifyHealth(-MGameLoop.Instance.CurrentActor.actor.Weapon.Damage);
                }
                else if (target.GetStats().GetArmor() > MGameLoop.Instance.CurrentActor.actor.Weapon.Damage)
                {
                    target.GetStats().ModifyArmor(-MGameLoop.Instance.CurrentActor.actor.Weapon.Damage);
                }
                else
                {
                    MGameLoop.Instance.CurrentActor.actor.Weapon.Damage -= target.GetStats().GetArmor();
                    target.GetStats().ModifyArmor(-target.GetStats().GetArmor());
                    target.GetStats().ModifyHealth(-MGameLoop.Instance.CurrentActor.actor.Weapon.Damage);
                }
                if (target.GetStats().GetHealth() <= 0)
                {
                    if (target.Type == MActor.ActorType.Player)
                    {
                       
                        MGameLoop.Instance.Players.Remove(target);
                        Object.Destroy(target.gameObject);
                    }
                    else
                    {
                        shooter.GetStats().GiveEXP(target.GetStats().GetExperienceGiven());
                        MGameLoop.Instance.Enemies.Remove(target);
                        Object.Destroy(target.gameObject);
                    }
                }
            }
            else if (Collision.GetComponent<MShootable>() != null) // destructible wall
            {
                var wall = Collision.GetComponent<MShootable>();
                var (tile1, tile2) = MGameLoop.Instance.BreakableWallsReverse[wall];
                MGameLoop.Instance.BreakableWalls.Remove((tile1, tile2));
                MGameLoop.Instance.BreakableWalls.Remove((tile2, tile1));
                MGameLoop.Instance.Walls.Remove((tile1, tile2));
                MGameLoop.Instance.Walls.Remove((tile2, tile1));
                MGameLoop.Instance.BreakableWallsReverse.Remove(wall);
                Object.Destroy(wall.gameObject);
            }
        }
        CurrentInstance = null;
    }
}
