using System.Collections.Generic;
using UnityEngine;
using static Cells;

public class AreaCaptureService
{
    private Cell[,] grid;
    private int w, h;
    private bool[,] visited;

    public AreaCaptureService(Cell[,] grid, int w, int h)
    {
        this.grid = grid;
        this.w = w;
        this.h = h;
        visited = new bool[w, h];
    }

    public float Capture(List<Vector2Int> trail, List<EnemyController> enemies)
    {
        for (int x = 0; x < w; x++)
        {
            for (int y = 0; y < h; y++)
            {
                visited[x, y] = false;
            }
        }

        foreach (var p in trail)
            grid[p.x, p.y].State = CellState.Wall;

        ClearVisited();

        foreach (var e in enemies)
            Flood(ToGrid(e.transform.position));

        int captured = 0;

        for (int x = 0; x < w; x++)
        {
            for (int y = 0; y < h; y++)
            {
                if (grid[x, y].State == CellState.Empty && !visited[x, y])
                {
                    grid[x, y].State = CellState.Captured;
                    captured++;
                }
            }
        }
        // enemy kill
        foreach (var e in enemies)
        {
            var pos = ToGrid(e.transform.position);
            if (pos.x >= 0 && pos.x < w && pos.y >= 0 && pos.y < h)
            {
                if (grid[pos.x, pos.y].State == CellState.Captured)
                {
                    e.gameObject.SetActive(false);
                }
            }
        }

        float percent = CalculateArePercentage();
        GameEvents.OnAreaCaptured?.Invoke(percent);

        return percent;
    }


    float CalculateArePercentage()
    {
        int totalPlayable = 0;
        int filledCount = 0;

       
        for (int x = 1; x < w - 1; x++)
        {
            for (int y = 1; y < h - 1; y++)
            {
                var state = grid[x, y].State;
                totalPlayable++;               
                if (state == CellState.Captured || state == CellState.Wall)
                {
                    filledCount++;
                }
            }
        }
        float percent = (filledCount * 100f) / totalPlayable;
        //Debug.Log($"Filled: {filledCount}, Total: {totalPlayable}, Percent: {percent}");
        return percent;
    }
    void Flood(Vector2Int start)
    {
        Queue<Vector2Int> q = new Queue<Vector2Int>();
        q.Enqueue(start);

        visited[start.x, start.y] = true;

        Vector2Int[] dirs = new Vector2Int[]
        {
        Vector2Int.up,
        Vector2Int.down,
        Vector2Int.left,
        Vector2Int.right
        };

        while (q.Count > 0)
        {
            var p = q.Dequeue();

            foreach (var d in dirs)
            {
                var n = p + d;

                if (n.x < 0 || n.x >= w || n.y < 0 || n.y >= h)
                    continue;

                if (visited[n.x, n.y])
                    continue;

                if (grid[n.x, n.y].State == CellState.Wall || grid[n.x, n.y].State == CellState.Captured)
                    continue;

                visited[n.x, n.y] = true;
                q.Enqueue(n);
            }
        }
    }
    void ClearVisited()
    {
        for (int x = 0; x < w; x++)
            for (int y = 0; y < h; y++)
                visited[x, y] = false;
    }
    Vector2Int ToGrid(Vector3 pos)
    {
        return GridManager.Instance.WorldToGrid(pos);
    }  
}
