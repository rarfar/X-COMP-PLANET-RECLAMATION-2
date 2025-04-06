using UnityEngine;

public class Grid
{
    private int height;
    private int width;
    private int[,] gridArray;
    private int cellSize;

    public Grid(int width, int height, int cellSize)
    {
        this.height = height;
        this.width = width;
        this.cellSize = cellSize;
        gridArray = new int[width, height];

        Debug.Log("Grid created with width: " + width + " and height: " + height);

        for (int x = 0; x < gridArray.GetLength(0); x++)
        {
            for (int z = 0; z < gridArray.GetLength(1); z++)
            {
                // create world text (put coordinates on the grid)
                /* TextMesh textObject = new GameObject().AddComponent<TextMesh>();
                textObject.transform.position = new Vector3(x, 0, z) * cellSize + new Vector3(cellSize, 0, cellSize) * 0.5f;
                textObject.transform.Rotate(90, 0, 0);
                textObject.text = x + "," + z;
                textObject.characterSize = 0.1f;
                textObject.fontSize = 50;
                textObject.anchor = TextAnchor.MiddleCenter;
                textObject.color = Color.white; */

                // Draw outlines
                Debug.DrawLine(new Vector3(x, 0, z) * cellSize, new Vector3(x + 1, 0, z) * cellSize, Color.white, 100f);
                Debug.DrawLine(new Vector3(x, 0, z) * cellSize, new Vector3(x, 0, z + 1) * cellSize, Color.white, 100f);

            }
        }
            // Finish outlines
            Debug.DrawLine(new Vector3(0, 0, height) * cellSize, new Vector3(width, 0, height) * cellSize, Color.white, 100f);
            Debug.DrawLine(new Vector3(width, 0, 0) * cellSize, new Vector3(width, 0, height) * cellSize, Color.white, 100f);

    }

    // World coordinates to grid coordinates
    public Vector3 getCellPosition(Vector3 worldPosition)
    {
        int x = Mathf.FloorToInt(worldPosition.x / cellSize);
        int z = Mathf.FloorToInt(worldPosition.z / cellSize);

        Debug.Log("World position: " + worldPosition + " Cell position: " + x + "," + z);

        return new Vector3(x, 0, z);
    }

    public void placeTile(Vector3 worldPosition, GameObject tilePrefab)
    {
        // Get grid coordinates
        Vector3 cellPosition = getCellPosition(worldPosition);

        // Check if cell is empty
        if (gridArray[(int)cellPosition.x, (int)cellPosition.z] == 0)
        {
            // Place tile
            gridArray[(int)cellPosition.x, (int)cellPosition.z] = 1;
            Object.Instantiate(tilePrefab, cellPosition * cellSize, Quaternion.identity);
            Debug.Log("Tile placed at: " + cellPosition.x + "," + cellPosition.z);
        }
        else
        {
            Debug.Log("Cell is not empty");
        }
    }


}
