using System.Collections.Generic;
using UnityEngine;

public class MPlayer : MonoBehaviour
{
    /*
    [SerializeField] [Range(1, 101)] float Accuracy;
    public MCell Cell { get; set; }
    private List<Vector2Int> _moves = null;
    bool _isMoving = false;
    private float SPEED = 5;

    private MItem _item;

    public void SetActive()
    {
        GetComponent<MeshRenderer>().material = MColours.Instance.PlayerActive;
    }

    public void SetIncative()
    {
        GetComponent<MeshRenderer>().material = MColours.Instance.PlayerIncative;
    }

    public void Move(MCell target)
    {
        if (MActions.Instance.ActionLock == true)
        {
            return;
        }
        var moves = MActions.Instance.Pathfind(new(Cell.X, Cell.Z), new(target.X, target.Z));

        if (moves == null || moves.Count == 0)
        {
            return;
        }

        MActions.Instance.ActionLock = true;
        _moves = moves;
        _isMoving = true;
        Cell.hasPlayer = false;
        target.hasPlayer = true;
        Cell = target;
    }

    private void Update()
    {
        if (_isMoving)
        {
            if (_moves.Count <= 0)
            {
                _moves = null;
                _isMoving= false;
                MActions.Instance.ActionLock = false;
                return;
            }
            var newPos = Vector3.MoveTowards(transform.position, new(_moves[0].x, transform.position.y, _moves[0].y), Time.deltaTime * SPEED);
            transform.position = newPos;
            if (transform.position == new Vector3(_moves[0].x, transform.position.y, _moves[0].y))
            {
                _moves.RemoveAt(0);
            }
        }
    }

    // ====
    // Item Mechanics

    public void PickupItem()
    {
        if (Cell.Item == null)
        {
            Debug.Log("No item here!");
            return;
        }
        if (_item != null)
        {
            Debug.Log("Item in hand already!");
            return;
        }

        MItem item = Cell.Item;
        Cell.Item = null;
        _item = item;
        item.gameObject.SetActive(false);
    }

    public void DropItem()
    {
        if (Cell.Item != null)
        {
            Debug.Log("Can't drop it here!");
            return;
        }
        if (_item == null)
        {
            Debug.Log("No item to drop!");
            return;
        }

        MItem item = _item;
        Cell.Item = item;
        _item = null;
        item.transform.position = new(Cell.transform.position.x + 0.3f, item.transform.position.y, Cell.transform.position.z - 0.3f);
        item.gameObject.SetActive(true);
    }

    // ====
    // Bullet Mechanics

    bool over = false;
    float OFFSET = 0.4f;
    private void OnMouseEnter()
    {
        over = true;
        GetComponent<MeshRenderer>().material = MColours.Instance.PlayerHover;
    }
    private void OnMouseDown()
    {
        GetComponent<MeshRenderer>().material = MColours.Instance.PlayerClick;
    }
    private void OnMouseUp()
    {
        if (over)
        {
            GetComponent<MeshRenderer>().material = MColours.Instance.PlayerHover;
            
            if (MActions.Instance.ActivePlayer == this)
            {
                Debug.Log("Can't shoot yourself...");
            }
            else
            {
                Shoot();
            }
        }
    }
    private void OnMouseExit()
    {
        over = false;
        if (MActions.Instance.ActivePlayer == this)
        {
            GetComponent<MeshRenderer>().material = MColours.Instance.PlayerActive;
        }
        else
        {
            GetComponent<MeshRenderer>().material = MColours.Instance.PlayerIncative;
        }
    }

    void Shoot() // called on the target
    {
        MPlayer shooter = MActions.Instance.ActivePlayer;

        if (MActions.Instance.ActionLock)
        {
            return;
        }
        MActions.Instance.ActionLock = true;
        GameObject bullet = Instantiate(MActions.Instance.Bullet);

        // set bullet colour from our item
        if (shooter._item != null)
        {
            bullet.GetComponent<MeshRenderer>().material = shooter._item.Colour;
        }

        // Math to find target based on accuracy
        float targetRadius = transform.localScale.x / 2;
        float bulletRadius = bullet.transform.localScale.x / 2;
        float hitOffset = targetRadius + bulletRadius;
        float missOffset = hitOffset / (shooter.Accuracy / 100);
        float targetOffset = Random.Range(-missOffset, missOffset);
        Vector3 shootDirection = transform.position - shooter.transform.position;
        Vector3 offsetDirection = Vector3.Cross(shootDirection, Vector3.up).normalized;
        Vector3 target = transform.position + offsetDirection * targetOffset;

        // init bullet
        bullet.transform.position = Vector3.MoveTowards(shooter.transform.position, transform.position, OFFSET); // offset the bullet in front of the player and towards the intended target
        var mbullet = bullet.GetComponent<MBullet>();
        mbullet.SetVelocity(target);
        mbullet.Flying = true;
    }
    */
}
