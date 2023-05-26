using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarSpawn : MonoBehaviour
{
    [SerializeField] GameObject[] stars;

    float spawnTimer = 0f;
    float spawnDelay = 0.1f;
    float spawnYMin = 5f;
    float spawnYMax = -5f;
    float spawnHeight = 3f;
    float starSpeedMax = 20f;
    float starSpeedMin = 5f;

    // Update is called once per frame
    void Update()
    {
        spawnTimer += Time.deltaTime;
        if (spawnTimer > spawnDelay)
        {
            SpawnStar();
            spawnTimer = 0f;
        }
    }
    
    void SpawnStar()
    {
        int starToSpawn = Random.Range(0, stars.Length);
        float spawnYoffset = (Random.Range(spawnYMin, spawnYMax) * spawnHeight);
        Vector3 spawnPoint = new Vector3(transform.position.x, transform.position.y + spawnYoffset);
        GameObject newStar = Instantiate(stars[starToSpawn], spawnPoint, transform.rotation);
        newStar.GetComponent<Rigidbody2D>().velocity = new Vector3(-Random.Range(starSpeedMin, starSpeedMax), 0f);
    }
}
