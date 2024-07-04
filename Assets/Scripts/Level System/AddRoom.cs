using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddRoom : MonoBehaviour
{
    private RoomTemplates templates;

    void Start()
    {
        GameObject roomsObject = GameObject.FindGameObjectWithTag("Rooms");
        if (roomsObject != null)
        {
            templates = roomsObject.GetComponent<RoomTemplates>();
            if (templates != null)
            {
                templates.rooms.Add(this.gameObject);
            }
            else
            {
                Debug.LogError("RoomTemplates component not found on Rooms object.");
            }
        }
        else
        {
            Debug.LogError("Rooms object not found.");
        }
    }
}
