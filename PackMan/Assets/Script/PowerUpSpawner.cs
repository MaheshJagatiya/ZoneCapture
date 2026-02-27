using System.Collections;
using UnityEngine;
using static Cells;

public class PowerUpSpawner : MonoBehaviour
{
    [SerializeField] float interval = 10f;
    [SerializeField] Transform powerUpParent;

    void Start()
    {
        StartCoroutine(SpawnRoutine());
    }

    IEnumerator SpawnRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(interval);
            Spawn();
        }
    }
    private void OnDisable()
    {
        StopAllCoroutines();
    }

    void Spawn()
    {
        var g = GridManager.Instance;

        int x = 0;
        int y = 0;

        int maxTry = 50; 
        int count = 0;

        do
        {
            x = Random.Range(1, g.width - 1);
            y = Random.Range(1, g.height - 1);

            count++;

            if (count > maxTry)
            {
                Debug.Log("No valid empty cell found for PowerUp");
                return;
            }

        } while (g.grid[x, y].State != CellState.Empty);

       
        Vector3 worldPos = g.GridToWorld(x, y);       
        var PowerUpObj = PowerUpPool.Instance.Get();
        PowerUpObj.transform.position = worldPos;
        PowerUpObj.transform.SetParent(powerUpParent);
    }
}
