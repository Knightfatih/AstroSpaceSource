using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{
    public static ObjectPooler Instance;
    [SerializeField] GameObject[] objectsToPool;
    [SerializeField] int poolSize = 10;
    private List<GameObject> pooledObjects;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        pooledObjects = new List<GameObject>();
        for (int i = 0; i < poolSize; i++)
        {
            foreach (GameObject obj in objectsToPool)
            {
                GameObject newObj = Instantiate(obj);
                newObj.SetActive(false);
                pooledObjects.Add(newObj);
            }
        }
    }

    public GameObject GetPooledObject(int index)
    {
        foreach (GameObject obj in pooledObjects)
        {
            if (!obj.activeInHierarchy && obj.name.StartsWith(objectsToPool[index].name))
            {
                return obj;
            }
        }

        return null;

        // If no inactive object is found instantiate a new one
        //GameObject newObj = Instantiate(objectsToPool[index]);
        //newObj.SetActive(false);
        //pooledObjects.Add(newObj);
        //return newObj;
    }
}
