using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{
    public static ObjectPooler Instance;

    [System.Serializable]
    public class Pool
    {
        public GameObject prefab;
        public int size;
    }

    public List<Pool> pools;
    public Dictionary<int, Queue<GameObject>> poolDictionary;
    private Transform poolParent;

    void Awake()
    {
        Instance = this;
        poolParent = this.transform;
    }

    void Start()
    {
        poolDictionary = new Dictionary<int, Queue<GameObject>>();

        for (int i = 0; i < pools.Count; i++)
        {
            Queue<GameObject> objectPool = new Queue<GameObject>();

            for (int j = 0; j < pools[i].size; j++)
            {
                GameObject obj = Instantiate(pools[i].prefab);
                obj.SetActive(false);
                obj.transform.SetParent(poolParent);
                objectPool.Enqueue(obj);
            }

            poolDictionary.Add(i, objectPool);
        }
    }

    public GameObject GetPooledObject(int index)
    {
        if (!poolDictionary.ContainsKey(index))
        {
            Debug.LogWarning("Pool with index " + index + " doesn't exist.");
            return null;
        }

        GameObject objectToReuse = poolDictionary[index].Dequeue();
        poolDictionary[index].Enqueue(objectToReuse);
        return objectToReuse;
    }

    public void ReturnToPool(GameObject obj)
    {
        obj.SetActive(false);
        obj.transform.SetParent(poolParent);
    }
}
