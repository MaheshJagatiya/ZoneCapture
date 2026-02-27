using System.Collections.Generic;
using UnityEngine;
using static Cells;
public class TrailController : MonoBehaviour
{
    [SerializeField] Transform trailParent;
    List<Vector2Int> trail = new();
    bool drawing;
    Vector2Int last;
    AreaCaptureService service;
    
    void Start()
    {
        var g = GridManager.Instance;
        service = new AreaCaptureService(g.grid, g.width, g.height);
        TrailPool.Instance.StartInitialize(60, trailParent);
        //service.CalculateTotalPlayable();
    }
    void Update()
    {
        var g = GridManager.Instance;

       
        Vector2Int pos = g.WorldToGrid(transform.position);

        
        if (pos.x < 0 || pos.x >= g.width ||
            pos.y < 0 || pos.y >= g.height)
        {
            return;
        }

        if (pos == last)
            return;

        last = pos;

        var grid = g.grid;
        CellState state = grid[pos.x, pos.y].State;

        if (drawing && state == CellState.Trail)
        {
            GameEvents.OnPlayerDied?.Invoke();
            return;
        }

    
        if (state == CellState.Empty)
        {
            if (!drawing)
                drawing = true;

            grid[pos.x, pos.y].State = CellState.Trail;
            GameEvents.OnGridUpdated?.Invoke();///
            trail.Add(pos);

      
            Vector3 worldPos = g.GridToWorld(pos.x, pos.y);         
            GameObject trailObj = TrailPool.Instance.Get();

            trailObj.transform.SetParent(trailParent);
            trailObj.transform.position = worldPos;          
        }
        else if (state == CellState.Wall && drawing)
        {
            drawing = false;

            service.Capture(trail, EnemyManager.Instance.GetEnemies());
            GameEvents.OnGridUpdated?.Invoke();/////
            ClearTrailObjects();
            trail.Clear();
        }
    }
    void ClearTrailObjects()
    {
        for (int i = trailParent.childCount - 1; i >= 0; i--)
        {
            var child = trailParent.GetChild(i).gameObject;
            TrailPool.Instance.Return(child);
        }      
    }
    public void ResetTrail()
    {
        foreach (var p in trail)
        {
            GridManager.Instance.grid[p.x, p.y].State = CellState.Empty;
        }
        for (int i = trailParent.childCount - 1; i >= 0; i--)
        {
            var child = trailParent.GetChild(i).gameObject;
            TrailPool.Instance.Return(child);
        }

        trail.Clear();
        drawing = false;
    }

}
