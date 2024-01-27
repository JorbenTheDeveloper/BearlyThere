using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FearEffect : MonoBehaviour
{
    public float fleeDuration = 3f;  // Duration for how long enemies should flee
    public float pushForce = 3f;  // The force with which enemies are pushed back

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            // Apply a push-back force
            Rigidbody2D enemyRb = other.GetComponent<Rigidbody2D>();
            if (enemyRb != null)
            {
                Vector2 pushDirection = other.transform.position - transform.position;
                pushDirection.Normalize(); // Ensure the force is applied uniformly
                enemyRb.AddForce(pushDirection * pushForce, ForceMode2D.Impulse);
            }

            // Check for BeeMovement component and trigger flee
            BeeMovement bee = other.GetComponent<BeeMovement>();
            if (bee != null)
            {
                bee.Flee(fleeDuration);
                return; // Exit the method after handling the bee
            }

            // Check for EnemyPatrol component and trigger flee
            EnemyPatrol patrol = other.GetComponent<EnemyPatrol>();
            if (patrol != null)
            {
                patrol.Flee(fleeDuration);
                // No need to return here if you want to allow for multiple component checks
            }

            // Add additional checks here for other enemy types
        }
    }
}
