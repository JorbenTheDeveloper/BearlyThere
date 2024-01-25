using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FartEffect : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("PlayerCharacter"))
        {
            PlayerMovement player = collision.GetComponent<PlayerMovement>();
            if (player != null)
            {
                player.LoseStamina(5); // Lose 5 stamina
            }
        }
    }
}
