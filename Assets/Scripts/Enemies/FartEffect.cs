using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FartEffect : MonoBehaviour
{
    public float pushForce = 5f;  // The force with which enemies are pushed back

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
        else if (collision.CompareTag("Enemy")) // Assuming enemies have the tag "Enemy"
        {
            Rigidbody2D enemyRb = collision.GetComponent<Rigidbody2D>();
            if (enemyRb != null)
            {
                // Calculate direction away from the fart effect
                Vector2 pushDirection = collision.transform.position - transform.position;
                pushDirection.Normalize();
                enemyRb.AddForce(pushDirection * pushForce, ForceMode2D.Impulse);
            }
        }
    }
}
