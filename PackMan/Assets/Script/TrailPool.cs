using System.Collections.Generic;
using UnityEngine;

public class TrailPool : MonoBehaviour
{
    public static TrailPool Instance;
    Transform PoolParent;
    private Queue<GameObject> pool = new Queue<GameObject>();

    void Awake()
    {
        Instance = this;       
    }
    public void StartInitialize(int startInitializeCount,Transform poolParent)
    {
        PoolParent = poolParent;
        for (int i = 0; i < startInitializeCount; i++)
        {
            GameObject obj = CreateObjectNew();
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

        // fallback (rare)
        var newObj = CreateObjectNew();

        return newObj;
    }
    GameObject CreateObjectNew()
    {
        GameObject obj = new GameObject("TrailCell");
        obj.transform.SetParent(PoolParent);
        BoxCollider2D col = obj.AddComponent<BoxCollider2D>();
        col.isTrigger = true;
        obj.tag = "Trail";
        return obj;
    }

    public void Return(GameObject obj)
    {
        obj.SetActive(false);
        pool.Enqueue(obj);
    }
}
