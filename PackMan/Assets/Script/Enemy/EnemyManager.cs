using System.Collections.Generic;
using UnityEngine;
using static Cells;

//Level wise enemy creation and chanage speed
//enemy spawn on empty area and border in area
public class EnemyManager : MonoBehaviour
{
    public static EnemyManager Instance;
    [SerializeField] EnemyController enemyPrefab;
    [SerializeField] Transform enemyParent;

    List<EnemyController> enemies = new();
    void Awake() => Instance = this;

    void Start()
    {
        SpawnEnemies();
    }

    public void Register(EnemyController e) => enemies.Add(e);
    public List<EnemyController> GetEnemies() => enemies;

    void SpawnEnemies()
    {
        int count = RoundManager.Instance.GetEnemyCount();

        for (int i = 0; i < count; i++)
        {
            SpawnEnemy(i+1);
        }
    }

    void SpawnEnemy(int increaseSpeed)
    {
        var g = GridManager.Instance;
        int x = 0;
        int y = 0;      
        int tries = 0;

        do
        {
            x = Random.Range(1, g.width - 1);
            y = Random.Range(1, g.height - 1);
            tries++;

            if (tries > 50)
                break;
        }
        while (g.grid[x, y].State != CellState.Empty);

        Vector3 pos = g.GridToWorld(x, y);

        EnemyController obj = Instantiate(enemyPrefab, pos, Quaternion.identity);
        var newSpeed = obj.speed + increaseSpeed;
        obj.speed = newSpeed;
        if (enemyParent != null)
            obj.transform.SetParent(enemyParent);
    }
}
