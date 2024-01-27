using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public Transform connectedDoor; // Assign this in the Unity Editor
    public GameObject teleportDestination; // GameObject to teleport to
    public Color usedColor = Color.green; // Color to indicate the door has been used

    public void Teleport(GameObject player)
    {
        if (teleportDestination != null)
        {
            player.transform.position = teleportDestination.transform.position; // Teleport the player
            ChangeColor(this.gameObject, usedColor); // Change color of this door
            if (connectedDoor != null)
            {
                ChangeColor(connectedDoor.gameObject, usedColor); // Change color of the connected door
            }
        }
    }

    private void ChangeColor(GameObject door, Color color)
    {
        SpriteRenderer spriteRenderer = door.GetComponent<SpriteRenderer>();
        if (spriteRenderer != null)
        {
            spriteRenderer.color = color; // Directly change the color of the SpriteRenderer
        }
        else
        {
            Debug.LogError("SpriteRenderer not found on " + door.name);
        }
    }
}
