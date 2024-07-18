using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public GameObject[] spawnPoints;
    public GameObject[] prefabsToSpawn;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    void Update()
    {
        if (RoomTemplates.instance != null && RoomTemplates.instance.Spawning)
        {
            SpawnPickUps();
            RoomTemplates.instance.Spawning = false;
        }
    }

    public void SpawnPrefabAtPoint(GameObject prefab, Vector3 position)
    {
        Instantiate(prefab, position, Quaternion.identity);
    }

    private void SpawnPickUps()
    {
        GameObject[] pickUpPoints = GameObject.FindGameObjectsWithTag("PickUps");

        foreach (GameObject point in pickUpPoints)
        {
            SpawnPrefabAtPoint(prefabsToSpawn[Random.Range(0, prefabsToSpawn.Length)], point.transform.position);
        }
    }

}
