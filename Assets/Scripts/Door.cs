using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public Transform connectedDoor; // Assign this in the Unity Editor
    public GameObject teleportDestination; // GameObject to teleport to, set this in the Unity Editor

    public void Teleport(GameObject player)
    {
        if (teleportDestination != null)
        {
            player.transform.position = teleportDestination.transform.position; // Teleport the player to the designated GameObject's position
        }
    }
}
