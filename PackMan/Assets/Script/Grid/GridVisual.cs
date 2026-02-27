using UnityEngine;
using static Cells;

//grid diaply and as per grid cell state color assign
//start function create pre display cell as per width and hight
public class GridVisual : MonoBehaviour
{
    [SerializeField] GameObject cellPrefab;
    private GameObject[,] visuals;

    void Start()
    {
        var g = GridManager.Instance;
        visuals = new GameObject[g.width, g.height];
       

        for (int x = 0; x < g.width; x++)
            for (int y = 0; y < g.height; y++)
            {              
                var pos = GridManager.Instance.GridToWorld(x, y);
                visuals[x, y] = Instantiate(cellPrefab, pos, Quaternion.identity, transform);
            }
        UpdateVisual();
    }
    void OnEnable()
    {
        GameEvents.OnGridUpdated += UpdateVisual;
    }

    void OnDisable()
    {
        GameEvents.OnGridUpdated -= UpdateVisual;
    }
    void UpdateVisual()
    {
        var grid = GridManager.Instance.grid;

        for (int x = 0; x < grid.GetLength(0); x++)
        {
            for (int y = 0; y < grid.GetLength(1); y++)
            {
                var sr = visuals[x, y].GetComponent<SpriteRenderer>();

                sr.color = grid[x, y].State switch
                {
                    CellState.Empty => new Color(0.08f, 0.08f, 0.1f),
                    CellState.Wall => Color.blue,
                    CellState.Trail => Color.green,
                    CellState.Captured => Color.cyan,
                    _ => Color.white
                };
            }
        }
    }
}
