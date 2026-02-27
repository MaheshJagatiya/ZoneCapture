using System.Collections;
using UnityEngine;
using static Cells;

//Enemy Movement Controll
//Enemy Collider detection
//Enemy Speed Manage
public class EnemyController : MonoBehaviour
{
    public float speed = 3f;
    float originalSpeed;
    Vector2 dir;

    void Start()
    {
        originalSpeed = speed;
        dir = Random.insideUnitCircle.normalized;
        EnemyManager.Instance.Register(this);
       
        GameEvents.OnPowerUpCollected += OnPowerUp;
    }

    void OnDestroy()
    {
        GameEvents.OnPowerUpCollected -= OnPowerUp;
    }

    void Update()
    {
        var g = GridManager.Instance;

        Vector3 nextPos = transform.position + (Vector3)(dir * speed * Time.deltaTime);

        Vector2Int gridPos = g.WorldToGrid(nextPos);

      
        if (!g.IsValid(gridPos) ||
            g.grid[gridPos.x, gridPos.y].State == CellState.Wall ||
            g.grid[gridPos.x, gridPos.y].State == CellState.Captured)
        {
            dir = -dir; // simple bounce
            return;
        }

       
        transform.position = nextPos;
    }
    void OnDisable()
    {
        GameEvents.OnPowerUpCollected -= OnPowerUp;
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        dir = Vector2.Reflect(dir, col.contacts[0].normal);
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Trail") && gameObject.activeInHierarchy)
        {
            GameEvents.OnPlayerDied?.Invoke();
        }
    }

    void OnPowerUp()
    {
        StopAllCoroutines();
        StartCoroutine(Slow());
    }

    IEnumerator Slow()
    {
        speed = originalSpeed * 0.4f;
        yield return new WaitForSeconds(3f);
        speed = originalSpeed;
    }
}
