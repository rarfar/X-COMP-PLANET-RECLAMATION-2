using UnityEngine;

public class EnvironmentTesting : MonoBehaviour
{
    [SerializeField] private int height = 20;
    [SerializeField] private int width = 10;
    [SerializeField] private int cellSize = 1;
    
    private Grid grid;
    public GameObject TilePrefab;
    public GameObject WallPrefab;
    public GameObject OtherPrefab;

    private GameObject previewObject;
    private PlacementMode currentMode = PlacementMode.Tiles;

    private enum PlacementMode { Tiles, Walls, Other, None }

    void Start()
    {
        grid = new Grid(width, height, cellSize);
        UpdatePreviewObject();
    }

    void Update()
    {
        HandleModeSwitch();
        UpdatePreviewPosition();

        if (Input.GetMouseButtonDown(0) && previewObject != null)
        {
            PlaceObject();
        }
    }

    private void HandleModeSwitch()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            currentMode = PlacementMode.Tiles;
            Debug.Log("Switched to Tile Placement Mode");
            UpdatePreviewObject();
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            currentMode = PlacementMode.Walls;
            Debug.Log("Switched to Wall Placement Mode");
            UpdatePreviewObject();
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            currentMode = PlacementMode.Other;
            Debug.Log("Switched to Other Object Placement Mode");
            UpdatePreviewObject();
        }
    }

    private void UpdatePreviewObject()
    {
        if (previewObject != null)
        {
            Destroy(previewObject);
        }

        switch (currentMode)
        {
            case PlacementMode.Tiles:
                previewObject = Instantiate(TilePrefab);
                break;
            case PlacementMode.Walls:
                previewObject = Instantiate(WallPrefab);
                break;
            case PlacementMode.Other:
                previewObject = Instantiate(OtherPrefab);
                break;
            default:
                previewObject = null;
                break;
        }

        if (previewObject != null)
        {
            SetPreviewMaterial(previewObject);
        }
    }

    private void UpdatePreviewPosition()
    {
        if (previewObject == null) return;

        Plane plane = new Plane(Vector3.up, Vector3.zero);
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (plane.Raycast(ray, out float distance))
        {
            Vector3 worldPosition = ray.GetPoint(distance);
            Vector3 cellPosition = grid.getCellPosition(worldPosition);

            if (worldPosition.x < 0 || worldPosition.x > width * cellSize || worldPosition.z < 0 || worldPosition.z > height * cellSize) 
            {
                previewObject.SetActive(false);
                return;
            }
            previewObject.SetActive(true);
            
            if (currentMode == PlacementMode.Walls)
            {
                bool isNorthWall = (worldPosition.z - Mathf.FloorToInt(worldPosition.z)) > 
                                (worldPosition.x - Mathf.FloorToInt(worldPosition.x));

                // Use the correct wall positioning logic
                Vector3 wallPosition = new Vector3(cellPosition.x, 0, cellPosition.z) * cellSize + 
                                    new Vector3(cellSize, 0, cellSize) * 0.5f;
                Quaternion rotation = Quaternion.identity;

                if (isNorthWall)
                {
                    wallPosition += new Vector3(0, 0, cellSize) * 0.5f;
                }
                else
                {
                    wallPosition += new Vector3(cellSize, 0, 0) * 0.5f;
                    rotation = Quaternion.Euler(0, 90, 0);
                }

                previewObject.transform.position = wallPosition;
                previewObject.transform.rotation = rotation;
            }
            else
            {
                // Regular tiles and other objects
                previewObject.transform.position = cellPosition;
            }
        }
    }

    private void PlaceObject()
    {
        Plane plane = new Plane(Vector3.up, Vector3.zero);
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (plane.Raycast(ray, out float distance))
        {
            Vector3 worldPosition = ray.GetPoint(distance);

            // Check boundaries
            if (worldPosition.x >= 0 && worldPosition.x <= width * cellSize &&
                worldPosition.z >= 0 && worldPosition.z <= height * cellSize)
            {
                switch (currentMode)
                {
                    case PlacementMode.Tiles:
                        grid.getCellPosition(worldPosition);
                        grid.placeTile(worldPosition, TilePrefab);
                        break;

                    case PlacementMode.Walls:
                        bool isNorthWall = (worldPosition.z - Mathf.FloorToInt(worldPosition.z)) > 
                                           (worldPosition.x - Mathf.FloorToInt(worldPosition.x));
                        placeWall(worldPosition, isNorthWall);
                        break;

                    case PlacementMode.Other:
                        grid.placeTile(worldPosition, OtherPrefab);
                        break;
                }
            }
        }
    }

    private void SetPreviewMaterial(GameObject obj)
    {
        if (obj.TryGetComponent<Renderer>(out Renderer renderer))
        {
            Material previewMaterial = new Material(renderer.sharedMaterial);
            previewMaterial.color = new Color(1, 1, 1, 0.5f); // Semi-transparent
            renderer.material = previewMaterial;
        }
    }

    private void placeWall(Vector3 worldPosition, bool isNorthWall)
    {
        Vector3 cellPosition = grid.getCellPosition(worldPosition);
        Vector3 wallPosition = new Vector3(cellPosition.x, 0, cellPosition.z) * cellSize + 
                               new Vector3(cellSize, 0, cellSize) * 0.5f;
        Quaternion rotation = Quaternion.identity;

        if (isNorthWall)
        {
            wallPosition += new Vector3(0, 0, cellSize) * 0.5f;
        }
        else
        {
            wallPosition += new Vector3(cellSize, 0, 0) * 0.5f;
            rotation = Quaternion.Euler(0, 90, 0);
        }

        Instantiate(WallPrefab, wallPosition, rotation);
    }

}
