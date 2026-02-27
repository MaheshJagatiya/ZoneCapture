using UnityEngine;

public class PowerUpController : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            GameEvents.OnPowerUpCollected?.Invoke();
            PowerUpPool.Instance.Return(gameObject);
          
        }
    }
}
