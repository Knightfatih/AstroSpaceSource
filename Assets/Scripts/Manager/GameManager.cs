using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public GameObject[] spawnPoints;
    public GameObject[] prefabsToSpawn;

    public GameObject[] enemySpawnPoints;
    public GameObject[] enemyPrefabs;

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
            SpawnEnemies();
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

    private void SpawnEnemies()
    {
        GameObject[] enemyPoints = GameObject.FindGameObjectsWithTag("EnemySpawn");

        foreach (GameObject spawnPoint in enemyPoints)
        {
            GameObject enemyPrefab = enemyPrefabs[Random.Range(0, enemyPrefabs.Length)];
            GameObject enemy = Instantiate(enemyPrefab, spawnPoint.transform.position, Quaternion.identity);

            EnemyAI enemyAI = enemy.GetComponent<EnemyAI>();
            if (enemyAI != null)
            {
                enemyAI.SetPatrolPoints();
            }
        }
    }

}
