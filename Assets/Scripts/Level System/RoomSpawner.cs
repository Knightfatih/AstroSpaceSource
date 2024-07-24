using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomSpawner : MonoBehaviour
{
    public int openingDirection;
    // 1--> need bottom door
    // 2--> need top door
    // 3--> need left door
    // 4--> need right door

    private RoomTemplates templates;
    public bool spawned = false;

    public float waitTime = 5f;

    void Start()
    {
        templates = GameObject.FindGameObjectWithTag("Rooms").GetComponent<RoomTemplates>();
        if (templates != null)
        {
            Invoke(nameof(Spawn), 0.1f);
        }
        else
        {
            Debug.LogError("RoomTemplates component not found.");
        }
        Destroy(gameObject, waitTime);
    }

    void Spawn()
    {
        if (!spawned)
        {
            GameObject[] roomsArray = null;

            switch (openingDirection)
            {
                case 1:
                    roomsArray = templates.bottomRooms;
                    break;
                case 2:
                    roomsArray = templates.topRooms;
                    break;
                case 3:
                    roomsArray = templates.leftRooms;
                    break;
                case 4:
                    roomsArray = templates.rightRooms;
                    break;
            }

            if (roomsArray != null && roomsArray.Length > 0)
            {
                int rand = Random.Range(0, roomsArray.Length);
                GameObject spawnedRoom = Instantiate(roomsArray[rand], transform.position, roomsArray[rand].transform.rotation);

                // Set the parent of the spawned room to the roomContainer
                spawnedRoom.transform.SetParent(templates.roomContainer, false);

                // Add the spawned room to the rooms list in RoomTemplates
                templates.rooms.Add(spawnedRoom);

                spawned = true;
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("SpawnPoint"))
        {
            var otherSpawner = other.GetComponent<RoomSpawner>();
            if (otherSpawner != null && !otherSpawner.spawned && !spawned)
            {
                GameObject closedRoomInstance = Instantiate(templates.closedRoom, transform.position, Quaternion.identity);

                // Set the parent of the closed room to the roomContainer
                closedRoomInstance.transform.SetParent(templates.roomContainer, false);

                Destroy(gameObject);
            }
            spawned = true;
        }
    }
}
