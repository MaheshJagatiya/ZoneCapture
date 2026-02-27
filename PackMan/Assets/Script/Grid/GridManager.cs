using UnityEngine;
using static Cells;

//calculate screen are and calculate number of cell
//create grid with width and hight cell count
// we can do fix grid also just change in SetupGridSize function

public class GridManager : MonoBehaviour
{
    public static GridManager Instance;

    public int width = 30;
    public int height = 22;

    public Cell[,] grid;
    [SerializeField] float cellSize = 0.9f;

    private void Awake()
    {
        Instance = this;
        SetupGridSize();
        SetWall();
    }
  
    void SetupGridSize()
    {
        Camera cam = Camera.main;

      //  Debug.Log(Screen.width  +"       "+ Screen.height);
        float screenHeight = cam.orthographicSize * 1.85f;
        float screenWidth = screenHeight * Screen.width / Screen.height;

        width = Mathf.FloorToInt(screenWidth / cellSize);
        height = Mathf.FloorToInt(screenHeight / cellSize);
        
        if (width % 2 == 0) width--;
        if (height % 2 == 0) height--;

        Debug.Log($"Grid Size: {width} x {height}");
    }

    void SetWall()
    {
        grid = new Cell[width, height];
        // boundary walls
        for (int x = 0; x < width; x++)
        {
            grid[x, 0].State = CellState.Wall;
            grid[x, height - 1].State = CellState.Wall;
        }
        for (int y = 0; y < height; y++)
        {
            grid[0, y].State = CellState.Wall;
            grid[width - 1, y].State = CellState.Wall;
        }

    }

    public Vector2Int WorldToGrid(Vector3 pos)
    {
        int x = Mathf.FloorToInt(pos.x + width / 2f);
        int y = Mathf.FloorToInt(pos.y + height / 2f);

        return new Vector2Int(x, y);
    }

    public Vector3 GridToWorld(int x, int y)
    {
        float worldX = x - width / 2f + 0.5f;
        float worldY = y - height / 2f + 0.5f;

        return new Vector3(worldX, worldY, 0f);
    }

    public bool IsValid(Vector2Int pos)
    {
        return pos.x >= 0 && pos.x < width &&
               pos.y >= 0 && pos.y < height;
    }


}
