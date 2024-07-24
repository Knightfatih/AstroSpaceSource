using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomTemplates : MonoBehaviour
{
    public static RoomTemplates instance;

    public GameObject[] bottomRooms;
    public GameObject[] topRooms;
    public GameObject[] leftRooms;
    public GameObject[] rightRooms;

    public GameObject closedRoom;

    public List<GameObject> rooms;

    public float waitTime = 1f;
    private bool spawnedBoss;
    public GameObject boss;

    private bool _spawning = false;

    public Transform roomContainer;

    public bool Spawning
    {
        get { return _spawning; }
        set { _spawning = value; }
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        if (waitTime <= 0 && !spawnedBoss)
        {
            if (rooms.Count > 0)
            {
                Instantiate(boss, rooms[rooms.Count - 1].transform.position, Quaternion.identity);
                spawnedBoss = true;
                Spawning = true;
            }
        }
        else
        {
            waitTime -= Time.deltaTime;
        }
    }
}
