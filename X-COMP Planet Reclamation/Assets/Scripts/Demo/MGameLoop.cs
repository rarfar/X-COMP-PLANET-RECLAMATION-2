using Database;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;
using UnityEngine.Android;
using UnityEngine.InputSystem.XR.Haptics;
using UnityEngine.SceneManagement;



public class MGameLoop : MonoBehaviour
{
    public static MGameLoop Instance; private void Awake() { Instance = this; }

    public (int index, MActor actor) CurrentActor = (-1, null);
    public List<MActor> Players;
    public List<MActor> Enemies;
    MAction CurrentAction;
    public Dictionary<Vector2Int, (MCell cell, List<Vector2Int> path)> PotentialMoves;
    public int CurrentRange;
    public MBot CurrentBot = new MBot(); // currently stateless so it never changes
    private bool GameOver;

    // grid
    public MCell[,] Grid;
    public HashSet<(Vector2Int, Vector2Int)> Walls;  // must contain boith orders
    public Dictionary<(Vector2Int, Vector2Int), MShootable> BreakableWalls;  // must contain both orders
    public Dictionary<MShootable, (Vector2Int, Vector2Int)> BreakableWallsReverse; // value can be in either order

    // camera
    private float CameraOffsetX;
    private float CameraOffsetZ;

    // Variable for other scripts
    public int CurrentSceneIndex;

    // Player state
    public GameState CurrentState;
    public enum GameState
    {
        EnemyTurn,
        Move,
        Attack,

    }


    private void Start()
    {
        (Grid, Walls, BreakableWalls, BreakableWallsReverse, Players, Enemies) = GetComponent<MGenerateGrid>().GenerateGrid();
        foreach (var v in Players) { v.SetStatsManager(); }
        foreach (var v in Enemies) { v.SetStatsManager(); }
        ChangePlayer(0);

        // camera
        CameraOffsetX = CurrentActor.actor.transform.position.x - Camera.main.transform.position.x;
        CameraOffsetZ = CurrentActor.actor.transform.position.z - Camera.main.transform.position.z;

        CurrentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        

    }

    public void Update()
    {
        if (MGameLoop.Instance.Enemies.Count == 0 || MGameLoop.Instance.Players.Count == 0)  // Game over, run once
        {
            if (GameOver)
            {
                return;
            }
            MGameLoop.Instance.UnlightTiles();
            MGameLoop.Instance.UnlightTargets();
            Time.timeScale = 0;
            if (MGameLoop.Instance.Players.Count == 0)
            {
                // LOSE CONDITION CODE
                Debug.Log("YOU LOSE");
                // Go to Gameover Scene
                SceneManager.LoadScene("GameOver", LoadSceneMode.Single);

            }
            else
            {
                // WIN CONDITION CODE
                Debug.Log("YOU WIN");

                int x = 0;
                foreach (MActor p in Players)
                {
                    //SaveObject<MActor>(p, "player" + x);
                    SaveObject<CStats>(p.statsManager.SaveObject(), "stats" + x);
                    x++;
                }


                SaveObject<Integer>(new Integer(x), "num");
<<<<<<< Updated upstream
                SceneManager.LoadScene("V2_Level_1", LoadSceneMode.Single);
=======

                SceneManager.LoadScene("LevelSelect", LoadSceneMode.Single);
<<<<<<< Updated upstream
>>>>>>> Stashed changes
=======
>>>>>>> Stashed changes
            }
            GameOver = true;
            return;
        }

        // set camera
        Camera.main.transform.position = new Vector3(CurrentActor.actor.transform.position.x - CameraOffsetX, Camera.main.transform.position.y, CurrentActor.actor.transform.position.z - CameraOffsetZ);

        if (CurrentAction != null) // an action is already executing
        {
            CurrentAction.Progress();
        }
        else if (CurrentActor.actor.Type == MActor.ActorType.Enemy) // enemy ai turn
        {
            // current action is null, call MBot.DecideAction
            CurrentBot.DecideAction();
        }
        // AWAIT INPUT ON PLAYER CHARACTER
        // Change Character
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            int index = CurrentActor.index + 1 >= Players.Count ? 0 : CurrentActor.index + 1;
            ChangePlayer(index);
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            int index = CurrentActor.index - 1 < 0 ? Players.Count - 1 : CurrentActor.index - 1;
            ChangePlayer(index);
        }
        // End turn
        else if (Input.GetKeyDown(KeyCode.Return))
        {
            foreach (var p in Players)
            {
                p.GetStats().ResetActionUnits();
                p.GetStats().ResetStamina();
            }
            CurrentActor = (0, Enemies[0]);
            UnlightTiles();
            UnlightTargets();

        }
        // Item interact
        else if (Input.GetKeyDown(KeyCode.P))
        {

        }
        else if (Input.GetKeyDown(KeyCode.D))
        { 
            
        }
    }


    private void SaveObject<T>(T obj, string filename)
    {
        string data = JsonUtility.ToJson(obj, true);
        string file = Application.persistentDataPath + "/"+ filename + ".json";

        if (!File.Exists(file)) File.CreateText(file).Dispose();
        

        File.WriteAllText(file, data);

    }


    public void ChangePlayer(int index)
    {
        if (index == CurrentActor.index)
        {
            return;
        }
        CurrentActor = (index, Players[index]);
        UnlightTiles();
        UnlightTargets();
        CurrentState = GameState.Move;

        if (CurrentState == GameState.Move)
        {
            LightTiles();
        } 
        else if (CurrentState == GameState.Attack)
        {
            LightTargets();
        }   
    }

    public void StartAction(MAction action)
    {
        if (CurrentAction == null && CurrentActor.actor.GetStats().GetActionUnits() >= action.GetCost() && action.IsValid())
        {
            CurrentActor.actor.GetStats().ModifyActionUnits(-action.GetCost());
            CurrentAction = action;
            action.Begin();
        }
    }

    public void EndAction()
    {
        CurrentAction.End();
        CurrentAction = null;

        if (CurrentActor.actor.Type == MActor.ActorType.Player)
        {
            UnlightTiles();
            UnlightTargets();

            if (CurrentState == GameState.Move)
            {
                LightTiles();
            }
            else if (CurrentState == GameState.Attack)
            {
                LightTiles();
                LightTargets();
            }
        }
    }

    // FIND AND SHOW POSSIBLE ACTIONS ==========================
    // Highlight Moves
    public void LightTiles()
    {



        // Sets potential moves based on if the player is attacking or moving
        if (CurrentState == GameState.Move)
        {
            if (CurrentActor.actor.Type == MActor.ActorType.Player && (((CurrentActor.actor.GetStats().GetActionUnits() == 0 && CurrentActor.actor.GetStats().GetStamina() == CurrentActor.actor.GetStats().maxStamina)) || CurrentActor.actor.GetStats().GetStamina() == 0))  // player with no actions
            {
                PotentialMoves = ValidMoves(CurrentActor.actor.Position, 0, Grid, Walls, true);
                PotentialMoves[CurrentActor.actor.Position].cell.Light();
                return;
            }

            PotentialMoves = ValidMoves(CurrentActor.actor.Position, CurrentActor.actor.GetStats().GetStamina(), Grid, Walls, true);
        }
        else if (CurrentState == GameState.Attack)
        {
            if (CurrentActor.actor.Type == MActor.ActorType.Player && (CurrentActor.actor.GetStats().GetActionUnits() == 0))
            { 
                PotentialMoves = ValidMoves(CurrentActor.actor.Position, 0, Grid, Walls, true);
                PotentialMoves[CurrentActor.actor.Position].cell.Light();
                return;
            }

            PotentialMoves = ValidMoves(CurrentActor.actor.Position, CurrentActor.actor.Weapon.Range, Grid, Walls, true);
        }

        // light all movable tiles
        
        foreach (var p in PotentialMoves)
        {
            p.Value.cell.Light();
        }
    }

    public void UnlightTiles()
    {
        // unlight tiles
        if (PotentialMoves == null)
        {
            return;
        }
        foreach (var p in PotentialMoves)
        {
            p.Value.cell.Unlight();
        }
        PotentialMoves = null;
    }

    public void LightTargets()
    {
        if (CurrentActor.actor.Type == MActor.ActorType.Player && CurrentActor.actor.GetStats().GetActionUnits() == 0)  // player with no actions
        {
            return;
        }

        // for every enemy/breakWall, if enemy is within range, light up
        CurrentRange = CurrentActor.actor.Weapon.Range;  // set
        if (CurrentRange <= 0)
        {
            return;
        }
        var rangeTiles = ValidMoves(CurrentActor.actor.Position, CurrentRange, Grid, new(), false);
        
        foreach (var e in Enemies)
        {
            if (rangeTiles.ContainsKey(e.Position))
            {
                e.GetComponent<MShootable>().Light();
            }
        }

        foreach (var e in BreakableWalls)
        {
            if (rangeTiles.ContainsKey(e.Key.Item1))
            {
                e.Value.Light();
            }
        }
    }

    public void UnlightTargets()
    {
        // for every enemy/breakWall, if enemy is lit, unlight
        CurrentRange = -1;

        foreach (var e in Enemies)
        {
            e.GetComponent<MShootable>().Unlight();
        }

        foreach (var e in BreakableWalls)
        {
            e.Value.Unlight();
        }
    }

    // BFS ==========================
    // Get list of tiles we could move to and a simple BFS pathfind to those tiles
    public static Dictionary<Vector2Int, (MCell, List<Vector2Int>)> ValidMoves(Vector2Int pos, int range, MCell[,] grid, HashSet<(Vector2Int, Vector2Int)> walls, bool checkObstacles)
    {
        var visited = new Dictionary<Vector2Int, (MCell cell, List<Vector2Int> path)>();
        visited.Add(pos, (grid[pos.x, pos.y], new()));

        var current = new List<Vector2Int>();
        current.Add(pos);

        var next = new List<Vector2Int>();
        int remainingRange = range;

        while (remainingRange > 0)
        {
            foreach (Vector2Int move in current)
            {
                foreach (Vector2Int neighbor in new Vector2Int[] { new(move.x + 1, move.y), new(move.x - 1, move.y), new(move.x, move.y + 1), new(move.x, move.y - 1) })
                {
                    if (visited.ContainsKey(neighbor))
                        continue; // already visited
                    if (neighbor.x >= grid.GetLength(0) || neighbor.y >= grid.GetLength(1) || neighbor.x < 0 || neighbor.y < 0)
                        continue; // out of bounds
                    if (checkObstacles && grid[neighbor.x, neighbor.y] == null)
                        continue; // cell does not exist
                    if (checkObstacles && walls.Contains((move, neighbor))) // not necessary to include checkObstacles
                        continue; // wall present
                    if (checkObstacles && grid[neighbor.x, neighbor.y].HasPlayer && MGameLoop.Instance.CurrentState != GameState.Attack)
                        continue; // player in the way (only checks when in move mode)

                    var movesToNeighbor = new List<Vector2Int>(visited[move].path);
                    movesToNeighbor.Add(neighbor);
                    visited.Add(neighbor, (grid[neighbor.x, neighbor.y], movesToNeighbor));

                    next.Add(neighbor);
                }
            }

            current = next;
            next = new();
            remainingRange--;
        }

        return visited;
    }

    public MAction GetCurrentAction()
    {
        return MGameLoop.Instance.CurrentAction;
    }
}
