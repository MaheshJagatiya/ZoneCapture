using System.Collections.Generic;
using UnityEngine;

//Object pool for Poerup create and remove

public class PowerUpPool : MonoBehaviour
{
    public static PowerUpPool Instance;
   
    [SerializeField] GameObject prefabPowerUp;
    Transform PoolParent;
    Queue<GameObject> pool = new Queue<GameObject>();

    void Awake()
    {
        Instance = this;       
    }
    public void StartInitialize(int startInitializeCount, Transform poolParent)
    {
        PoolParent = poolParent;
       
        for (int i = 0; i < startInitializeCount; i++)
        {
            var obj = Instantiate(prefabPowerUp, PoolParent);
            obj.SetActive(false);
           
            pool.Enqueue(obj);
        }
    }

    public GameObject Get()
    {
        if (pool.Count > 0)
        {
            var obj = pool.Dequeue();
            obj.SetActive(true);
            return obj;
        }

        return Instantiate(prefabPowerUp, PoolParent);
    }

    public void Return(GameObject obj)
    {
        obj.SetActive(false);
        pool.Enqueue(obj);
    }
}
