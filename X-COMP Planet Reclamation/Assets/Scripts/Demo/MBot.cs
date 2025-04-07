using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MBot
{
    // Constructor for MBot, called on the first turn
    public MBot()
    {
        // Initialization logic can go here if needed
    }

    // Method to decide the bot's action during its turn 
    public void DecideAction()
    {
        /* // Set the current weapon range for the bot
        MGameLoop.Instance.CurrentRange = MGameLoop.Instance.CurrentActor.actor.Weapon.Range;

        // Calculate all valid moves for the bot based on its position, stamina, and obstacles
        MGameLoop.Instance.PotentialMoves = MGameLoop.ValidMoves(
            MGameLoop.Instance.CurrentActor.actor.Position,
            MGameLoop.Instance.CurrentActor.actor.GetStats().GetStamina(),
            MGameLoop.Instance.Grid,
            MGameLoop.Instance.Walls,
            true
        );

        // Remove the bot's current position from the list of potential moves
        MGameLoop.Instance.PotentialMoves.Remove(MGameLoop.Instance.CurrentActor.actor.Position);

        // Check if the bot is out of action points
        if (MGameLoop.Instance.CurrentActor.actor.GetStats().GetActionUnits() <= 0)
        {
            int index = MGameLoop.Instance.CurrentActor.index;

            // If this is the last enemy in the list, reset all enemies' action points and switch to players
            if (index == MGameLoop.Instance.Enemies.Count - 1)
            {
                foreach (var p in MGameLoop.Instance.Enemies)
                {
                    p.GetStats().ResetActionUnits();
                }

                // Switch to the first player and update the game state
                MGameLoop.Instance.CurrentActor = (0, MGameLoop.Instance.Players[0]);
                MGameLoop.Instance.LightTiles();
                MGameLoop.Instance.LightTargets();
            }
            else
            {
                // Otherwise, move to the next enemy in the list
                MGameLoop.Instance.CurrentActor = (index + 1, MGameLoop.Instance.Enemies[index + 1]);
            }

            // End the bot's turn
            return;
        }
        // If the bot has odd action points and an opponent is in range, attempt to shoot
        else if (MGameLoop.Instance.CurrentActor.actor.GetStats().GetActionUnits() % 2 == 1)
        {
            // Get all tiles within the bot's weapon range
            Dictionary<Vector2Int, (MCell, List<Vector2Int>)> targetTiles = MGameLoop.ValidMoves(
                MGameLoop.Instance.CurrentActor.actor.Position,
                MGameLoop.Instance.CurrentRange,
                MGameLoop.Instance.Grid,
                null,
                false
            );

            // Find all players within range
            List<MActor> inRange = new List<MActor>();
            foreach (var p in MGameLoop.Instance.Players)
            {
                if (targetTiles.ContainsKey(p.Position))
                {
                    inRange.Add(p);
                }
            }

            // If there are players in range, shoot at a random player
            if (inRange.Count > 0)
            {
                MActor player = inRange[UnityEngine.Random.Range(0, inRange.Count)];
                MGameLoop.Instance.StartAction(new MShoot(player.transform));
                return;
            }
        }

        // If no other action is taken, make a random move
        var tiles = MGameLoop.Instance.PotentialMoves.ToList();
        var move = tiles[UnityEngine.Random.Range(0, tiles.Count)].Key;
        MGameLoop.Instance.StartAction(new MMove(move)); */

        //----------------------------------------------------------------------------------------
        
        // Set the current weapon range for the bot
        MGameLoop.Instance.CurrentRange = MGameLoop.Instance.CurrentActor.actor.Weapon.Range;

        // Calculate all valid moves for the bot based on its position, stamina, and obstacles
        MGameLoop.Instance.PotentialMoves = MGameLoop.ValidMoves(
            MGameLoop.Instance.CurrentActor.actor.Position,
            MGameLoop.Instance.CurrentActor.actor.GetStats().GetStamina(),
            MGameLoop.Instance.Grid,
            MGameLoop.Instance.Walls,
            true
        );

        // Remove the bot's current position from the list of potential moves
        MGameLoop.Instance.PotentialMoves.Remove(MGameLoop.Instance.CurrentActor.actor.Position);

        // Check if the bot is out of action points
        if (MGameLoop.Instance.CurrentActor.actor.GetStats().GetActionUnits() <= 0)
        {
            EndTurn();
            return;
        }

        // Get the bot's health and action points
        int health = MGameLoop.Instance.CurrentActor.actor.GetStats().GetHealth();
        int maxHealth = MGameLoop.Instance.CurrentActor.actor.GetStats().maxHealth;
        int actionPoints = MGameLoop.Instance.CurrentActor.actor.GetStats().GetActionUnits();

        // Check if any enemies are in range
        Dictionary<Vector2Int, (MCell, List<Vector2Int>)> targetTiles = MGameLoop.ValidMoves(
            MGameLoop.Instance.CurrentActor.actor.Position,
            MGameLoop.Instance.CurrentRange,
            MGameLoop.Instance.Grid,
            MGameLoop.Instance.Walls,
            false
        );

        List<MActor> enemiesInRange = new List<MActor>();
        foreach (var enemy in MGameLoop.Instance.Players)
        {
            if (targetTiles.ContainsKey(enemy.Position))
            {
                float distance = Vector2Int.Distance(MGameLoop.Instance.CurrentActor.actor.Position, enemy.Position);
                // Check for clear sight using a raycast
                if (HasClearSight(MGameLoop.Instance.CurrentActor.actor.Position, enemy.Position))
                {
                    Debug.Log("AI has clear sight");
                    // Check if the enemy is within the bot's weapon range
                    if (distance <= MGameLoop.Instance.CurrentRange)
                    {
                        // Add the enemy to the list if it's within range and has clear sight
                        enemiesInRange.Add(enemy);
                    }
                }
                
            }
        }

        // If enemies are in range
        if (enemiesInRange.Count > 0)
        {
            MActor target = enemiesInRange[UnityEngine.Random.Range(0, enemiesInRange.Count)];

            // Decide to flee or shoot
            if (actionPoints == 1 && target.GetStats().GetHealth() < target.GetStats().maxHealth * 0.3)
            {
                MGameLoop.Instance.StartAction(new MShoot(target.transform));
                return;
            }
            else if (actionPoints == 1)
            {
                MoveToCover();
                return;
            }
            else
            {
                MGameLoop.Instance.StartAction(new MShoot(target.transform));
                return;
            }
        }

        // If no enemies are in range, decide on movement
        DecideMovement(health, maxHealth, actionPoints);
    }

    private void DecideMovement(int health, int maxHealth, int actionPoints)
    {
        // If last action point, move defensively
        if (actionPoints == 1)
        {
            MoveToCover();
            return;
        }

        // Find the closest enemy
        MActor closestEnemy = FindClosestActor(MGameLoop.Instance.Players);

        // If an enemy is present, move toward it with randomness 50%
        if (closestEnemy != null && UnityEngine.Random.Range(0, 3) != 0)
        {
            MoveTowardsEnemy(closestEnemy);
            return;
        }

        // Find the closest friendly
        MActor closestFriendly = FindClosestActor(MGameLoop.Instance.Enemies);

        // If a friendly is present, move toward it
        if (closestFriendly != null)
        {
            MoveTowardsGroup(new List<MActor> { closestFriendly });
            return;
        }

        // If no enemies or friendlies are found, move randomly
        MoveRandomly();
    }

    private void EndTurn()
    {
        int index = MGameLoop.Instance.CurrentActor.index;

        // If this is the last enemy in the list, reset all enemies' action points and switch to players
        if (index >= MGameLoop.Instance.Enemies.Count - 1)
        {
            foreach (var p in MGameLoop.Instance.Enemies)
            {
                p.GetStats().ResetActionUnits();
                p.GetStats().ResetStamina();
            }

            // Switch to the first player and update the game state
            MGameLoop.Instance.CurrentActor = (0, MGameLoop.Instance.Players[0]);
            MGameLoop.Instance.CurrentState = MGameLoop.GameState.Move;
            MGameLoop.Instance.LightTiles();
        }
        else
        {
            // Otherwise, move to the next enemy in the list
            MGameLoop.Instance.CurrentActor = (index + 1, MGameLoop.Instance.Enemies[index + 1]);
        }
    }

    private bool HasClearSight(Vector2Int from2D, Vector2Int to2D)
    {
        // Convert 2D positions to 3D
        Vector3 from = ConvertTo3D(from2D, 0.6f); // Set y to 1f for the bot's height
        Vector3 to = ConvertTo3D(to2D, 0.6f);     // Set y to 1f for the target's height

        // Calculate the direction from the source to the target
        Vector3 direction = to - from;

        Debug.DrawLine(from, to, Color.red, 5f); // Draw a line for debugging

        // Perform a raycast to check for obstacles between the bot and the target
        if (Physics.Raycast(from, direction, out RaycastHit hit, direction.magnitude))
        {
            // Check if the hit object is not the target (e.g., a wall is in the way)
            return false;
        }

        // If no obstacles were hit, there is a clear line of sight
        return true;
    }

    private void MoveToCover()
    {
        // Get all valid tiles the bot can move to
        var potentialMoves = MGameLoop.Instance.PotentialMoves;

        // List to store tiles that are out of sight
        List<Vector2Int> outOfSightTiles = new List<Vector2Int>();

        foreach (var move in potentialMoves)
        {
            Vector2Int tile = move.Key;
            bool isOutOfSight = true;

            // Check if any player can see this tile
            foreach (var player in MGameLoop.Instance.Players)
            {
                if (HasClearSight(tile, player.Position))
                {
                    isOutOfSight = false;
                    break;
                }
            }

            // If no player can see this tile, add it to the list
            if (isOutOfSight)
            {
                outOfSightTiles.Add(tile);
            }
        }

        // If there are out-of-sight tiles, pick one randomly
        if (outOfSightTiles.Count > 0)
        {
            Vector2Int targetTile = outOfSightTiles[UnityEngine.Random.Range(0, outOfSightTiles.Count)];
            MGameLoop.Instance.StartAction(new MMove(targetTile));
            return;
        }

        // If no out-of-sight tiles are available, attempt to shoot again
        MActor closestEnemy = FindClosestActor(MGameLoop.Instance.Players);
        MActor closestFriendly = FindClosestActor(MGameLoop.Instance.Enemies);
        if (closestEnemy != null)
        {
            float distance = Vector2Int.Distance(MGameLoop.Instance.CurrentActor.actor.Position, closestEnemy.Position);
            // Check for clear sight using a raycast
            if (HasClearSight(MGameLoop.Instance.CurrentActor.actor.Position, closestEnemy.Position))
            {
                // Check if the enemy is within the bot's weapon range
                if (distance <= MGameLoop.Instance.CurrentRange)
                {
                    // Shoot at the enemy
                    MGameLoop.Instance.StartAction(new MShoot(closestEnemy.transform));
                }
                else if (closestFriendly != null)
                {
                    MoveTowardsGroup(new List<MActor> { closestFriendly });
                    return;
                }
                else
                {
                    MoveRandomly();
                    return;
                }
            }
            else if (closestFriendly != null)
            {
                MoveTowardsGroup(new List<MActor> { closestFriendly });
                return;
            }
            else
            {
                MoveRandomly();
                return;
            }
            
        }
        else if (closestFriendly != null)
        {
            MoveTowardsGroup(new List<MActor> { closestFriendly });
            return;
        }
        else
        {
            // If no enemies are found, move randomly as a fallback
            MoveRandomly();
        }
    }

    private void MoveTowardsGroup(List<MActor> group)
    {
        // If no friendlies are present, move randomly
        if (group.Count == 0)
        {
            //MoveRandomly();
            return;
        }

        // Pick a random friendly bot from the group
        MActor targetFriendly = group[UnityEngine.Random.Range(0, group.Count)];

        // Get all valid tiles the bot can move to
        var potentialMoves = MGameLoop.Instance.PotentialMoves;

        // Find the tile closest to the target friendly within 3-5 tiles
        Vector2Int bestTile = Vector2Int.zero;
        float bestDistance = float.MaxValue;

        foreach (var move in potentialMoves)
        {
            Vector2Int tile = move.Key;
            float distance = Vector2Int.Distance(tile, targetFriendly.Position);

            // Check if the tile is within the desired range (3-5 tiles)
            if (distance >= 3 && distance <= 5 && distance < bestDistance)
            {
                bestDistance = distance;
                bestTile = tile;
            }
        }

        // If a valid tile is found, move to it
        if (bestDistance < float.MaxValue)
        {
            MGameLoop.Instance.StartAction(new MMove(bestTile));
        }
        else
        {
            // If no valid tile is found, move randomly
            MoveRandomly();
        }
    }

    private void MoveTowardsEnemy(MActor enemy)
    {
        // Get all valid tiles the bot can move to
        var potentialMoves = MGameLoop.Instance.PotentialMoves;

        // Get the bot's weapon range
        int weaponRange = MGameLoop.Instance.CurrentRange;

        // List to store tiles within the desired range
        List<Vector2Int> tilesInRange = new List<Vector2Int>();

        // Find tiles within the bot's range from the enemy
        foreach (var move in potentialMoves)
        {
            Vector2Int tile = move.Key;
            float distanceToEnemy = Vector2Int.Distance(tile, enemy.Position);

            // Check if the tile is within the bot's weapon range
            if (distanceToEnemy <= weaponRange)
            {
                tilesInRange.Add(tile);
            }
        }

        // If there are tiles within range, pick one randomly
        if (tilesInRange.Count > 0)
        {
            Vector2Int targetTile = tilesInRange[UnityEngine.Random.Range(0, tilesInRange.Count)];
            MGameLoop.Instance.StartAction(new MMove(targetTile));
            return;
        }

        // If no tiles are within range, move closer to the enemy
        Vector2Int bestTile = Vector2Int.zero;
        float shortestDistance = float.MaxValue;
        foreach (var move in potentialMoves)
        {
            Vector2Int tile = move.Key;
            float distanceToEnemy = Vector2Int.Distance(tile, enemy.Position);

            // Keep track of the tile that gets the bot closest to the enemy
            if (distanceToEnemy < shortestDistance)
            {
                shortestDistance = distanceToEnemy;
                bestTile = tile;
            }
        }

        // If a valid tile is found, move to it
        if (shortestDistance < float.MaxValue)
        {
            MGameLoop.Instance.StartAction(new MMove(bestTile));
        }
        else
        {
            // If no valid tile is found, move randomly
            MoveRandomly();
        }
    }

    private void MoveRandomly()
    {
        // Move to a random valid tile
        var tiles = MGameLoop.Instance.PotentialMoves.ToList();
        if (tiles.Count == 0)
        {
            EndTurn();
            return;
        }
        var move = tiles[UnityEngine.Random.Range(0, tiles.Count)].Key;
        MGameLoop.Instance.StartAction(new MMove(move));
    }

    private MActor FindClosestActor(List<MActor> actors)
    {
        Vector2Int botPosition = MGameLoop.Instance.CurrentActor.actor.Position;
        MActor closestActor = null;
        float shortestDistance = float.MaxValue;

        foreach (var actor in actors)
        {
            float distance = Vector2Int.Distance(botPosition, actor.Position);
            if (distance < shortestDistance && distance <= 20)
            {
                shortestDistance = distance;
                closestActor = actor;
            }
        }

        return closestActor;
    }

    private Vector3 ConvertTo3D(Vector2Int position2D, float y = 0f)
    {
        return new Vector3(position2D.x, y, position2D.y);
    }
    
}


