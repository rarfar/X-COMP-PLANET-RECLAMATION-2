using UnityEngine;
using System.Collections.Generic;

public class MActions : MonoBehaviour
{
    /*
    [SerializeField] GameObject cellsParent;
    [SerializeField] GameObject wallsParent;
    [SerializeField] GameObject playersParent;

    public GameObject Bullet;

    //

    MCell[,] _cells;
    Dictionary<(Vector2Int, Vector2Int), MWall> _walls = new();
    List<MPlayer> _players = new();

    public bool ActionLock = false;  // ensures we only do one thing at a time
    private int _activePlayerIndex = 0;
    public MPlayer ActivePlayer { get { return _players[_activePlayerIndex]; } }

    public static MActions Instance;

    public Vector2Int MaxCoords { get { return new(_cells.GetLength(0) - 1, _cells.GetLength(1) - 1); } }

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        // put walls and cells into the model
        int maxX = 0;
        int maxZ = 0;
        foreach (Transform cell in cellsParent.transform)
        {
            int x = Mathf.RoundToInt(cell.transform.position.x);
            int z = Mathf.RoundToInt(cell.transform.position.z);
            maxX = Mathf.Max(x, maxX);
            maxZ = Mathf.Max(z, maxZ);
        }
        _cells = new MCell[maxX + 1, maxZ + 1];
        foreach (Transform cell in cellsParent.transform)
        {
            MCell mcell = cell.GetComponent<MCell>();
            _cells[Mathf.RoundToInt(cell.transform.position.x), Mathf.RoundToInt(cell.transform.position.z)] = mcell;
        }
        foreach(Transform wall in wallsParent.transform)
        {
            // model walls in both directions to be safe
            MWall mwall = wall.GetComponent<MWall>();
            int x1, x2, z1, z2;
            if (Mathf.RoundToInt(wall.transform.position.x * 2) != Mathf.RoundToInt(wall.transform.position.x) + Mathf.RoundToInt(wall.transform.position.x))
            {
                x1 = Mathf.RoundToInt(wall.transform.position.x - 0.5f);
                x2 = Mathf.RoundToInt(wall.transform.position.x + 0.5f);
                z1 = Mathf.RoundToInt(wall.transform.position.z);
                z2 = z1;
            }
            else
            {
                z1 = Mathf.RoundToInt(wall.transform.position.z - 0.5f);
                z2 = Mathf.RoundToInt(wall.transform.position.z + 0.5f);
                x1 = Mathf.RoundToInt(wall.transform.position.x);
                x2 = x1;
            }
            _walls[(new (x1, z1), new (x2, z2) )] = mwall;
            _walls[(new (x2, z2), new (x1, z1) )] = mwall;
        }
        foreach(Transform player in playersParent.transform)
        {
            MPlayer mplayer = player.GetComponent<MPlayer>();
            _players.Add(mplayer);
            MCell cell = _cells[Mathf.RoundToInt(player.transform.position.x), Mathf.RoundToInt(player.transform.position.z)];
            cell.hasPlayer = true;
            mplayer.Cell = cell;
        }

        ActivePlayer.SetActive();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ActivePlayer.SetIncative();
            _activePlayerIndex = _activePlayerIndex + 1 >= _players.Count ? 0 : _activePlayerIndex + 1;
            ActivePlayer.SetActive();
        }

        if (Input.GetKeyDown(KeyCode.P) && !ActionLock)
        {
            ActivePlayer.PickupItem();
        } else if  (Input.GetKeyDown(KeyCode.D) && !ActionLock)
        {
            ActivePlayer.DropItem();
        }
    }



    // Mock Pathfinding
    // use suboptimal dfs algo for simplicity in mini-demo
    public List<Vector2Int> Pathfind(Vector2Int source, Vector2Int target)
    {
        bool[,] visited = new bool[_cells.GetLength(0), _cells.GetLength(1)];
        List<Vector2Int> path = new();
        return DFSHelper(source, target, path, visited);
    }

    private List<Vector2Int> DFSHelper(Vector2Int current, Vector2Int target, List<Vector2Int> path, bool[,] visited)
    {
        foreach (Vector2Int next in new Vector2Int[] { new (current.x + 1, current.y), new (current.x - 1, current.y), new (current.x, current.y + 1), new (current.x, current.y - 1) } )
        {
            // found
            if (current ==  target)
            {
                return path;
            }
            // out of bounds
            if (next.x >= visited.GetLength(0) || next.y >= visited.GetLength(1) || next.x < 0 || next.y < 0)
            {
                continue;
            }
            // does not exist
            if (_cells[next.x, next.y] == null)
            {
                continue;
            }
            // backtrack
            if (visited[next.x, next.y])
            {
                continue;
            }
            // wall
            if (_walls.ContainsKey((current, next)))
            {
                continue;
            }
            // player
            if (_cells[next.x, next.y].hasPlayer)
            {
                continue;
            }

            var newPath = new List<Vector2Int>(path);
            newPath.Add(next);
            visited[next.x, next.y] = true;
            var result = DFSHelper(next, target, newPath, visited);
            if (result != null && result.Count != 0)
            {
                return result;
            }
        }
        return null;
    }
    */
}
