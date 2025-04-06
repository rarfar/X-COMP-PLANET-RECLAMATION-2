using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Assertions;

public class MGenerateGrid : MonoBehaviour
{
    [SerializeField] GameObject BreakableWallsParent;
    [SerializeField] GameObject ActorsParent;

    [SerializeField] GameObject WallModelParent;
    [SerializeField] GameObject CellModelParent;

    [SerializeField] GameObject CellPrefab;
    [SerializeField] GameObject EmptyGameObjectPrefab;

    public (MCell[,], HashSet<(Vector2Int, Vector2Int)>, Dictionary<(Vector2Int, Vector2Int), MShootable>, Dictionary<MShootable, (Vector2Int, Vector2Int)>, List<MActor>, List<MActor>) GenerateGrid()
    {
        // find max cell dimensions
        int maxX = 0;
        int maxZ = 0;
        foreach (Transform cellModel in CellModelParent.transform)
        {
            var model = cellModel.GetComponent<MCellModel>();
            maxX = Mathf.Max(maxX, model.PositionX + model.SizeX - 1);
            maxZ = Mathf.Max(maxZ, model.PositionZ + model.SizeZ - 1);
        }

        // init vars
        var grid = new MCell[maxX + 1, maxZ + 1];
        HashSet<(Vector2Int, Vector2Int)> walls = new();
        Dictionary<(Vector2Int, Vector2Int), MShootable> breakableWalls = new();
        Dictionary<MShootable, (Vector2Int, Vector2Int)> breakableWallsReverse = new();
        List<MActor> players = new();
        List<MActor> enemies = new();

        // create cells
        var generatedCells = Instantiate(new GameObject());
        generatedCells.name = "GENERATED_CELLS";
        foreach (Transform cellModel in CellModelParent.transform)
        {
            var model = cellModel.GetComponent<MCellModel>();
            for (int x = model.PositionX; x < model.PositionX + model.SizeX; x++)
            {
                for (int z = model.PositionZ; z < model.PositionZ + model.SizeZ; z++)
                {
                    if (grid[x, z] != null)
                    {
                        continue;
                    }

                    GameObject cell = Instantiate(CellPrefab, generatedCells.transform);
                    cell.transform.position = new Vector3(x, model.transform.position.y, z);
                    cell.transform.localScale = new Vector3(1, model.transform.localScale.y, 1);
                    grid[x, z] = cell.GetComponent<MCell>();
                }
            }
        }

        // create walls
        foreach (Transform wallModel in WallModelParent.transform)
        {
            var model = wallModel.GetComponent<MWallModel>();

            for (int i = model.Start; i <= model.End; i++)
            {
                int x1, x2, z1, z2;
                if (model.WallDirection == MWallModel.Direction.X)
                {
                    z1 = model.Position;
                    z2 = model.Position + 1;
                    x1 = i;
                    x2 = i;
                }
                else
                {
                    x1 = model.Position;
                    x2 = model.Position + 1;
                    z1 = i;
                    z2 = i;
                }

                walls.Add((new(x1, z1), new(x2, z2)));
                walls.Add((new(x2, z2), new(x1, z1)));
            }
        }

        foreach (Transform bwall in BreakableWallsParent.transform)
        {
            int x1, x2, z1, z2;
            if (Mathf.RoundToInt(bwall.transform.position.x * 2) != Mathf.RoundToInt(bwall.transform.position.x) + Mathf.RoundToInt(bwall.transform.position.x))
            {
                x1 = Mathf.RoundToInt(bwall.transform.position.x - 0.5f);
                x2 = Mathf.RoundToInt(bwall.transform.position.x + 0.5f);
                z1 = Mathf.RoundToInt(bwall.transform.position.z);
                z2 = z1;
            }
            else
            {
                z1 = Mathf.RoundToInt(bwall.transform.position.z - 0.5f);
                z2 = Mathf.RoundToInt(bwall.transform.position.z + 0.5f);
                x1 = Mathf.RoundToInt(bwall.transform.position.x);
                x2 = x1;
            }
            walls.Add((new(x1, z1), new(x2, z2)));
            walls.Add((new(x2, z2), new(x1, z1)));

            var breakable = bwall.GetComponent<MShootable>();
            Assert.IsNotNull(breakable);
            breakableWalls[(new(x1, z1), new(x2, z2))] = breakable;
            breakableWalls[(new(x2, z2), new(x1, z1))] = breakable;
            breakableWallsReverse[breakable] = (new(x2, z2), new(x1, z1));
        }

        foreach (Transform a in ActorsParent.transform)
        {
            MActor actor = a.GetComponent<MActor>();
            MCell cell = grid[Mathf.RoundToInt(actor.transform.position.x), Mathf.RoundToInt(actor.transform.position.z)];
            cell.HasPlayer = true;
            actor.Cell = cell;

            if (actor.Type == MActor.ActorType.Player)
            {
                players.Add(actor);
            }
            else
            {
                enemies.Add(actor);
            }
        }

        return (grid, walls, breakableWalls, breakableWallsReverse, players, enemies);
    }
}
