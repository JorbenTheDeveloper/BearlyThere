using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FearEffect : MonoBehaviour
{
    public float fleeDuration = 3f;  // Duration for how long enemies should flee

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            // Check for BeeMovement component
            BeeMovement bee = other.GetComponent<BeeMovement>();
            if (bee != null)
            {
                bee.Flee(transform.position, fleeDuration);
                return; // Exit the method after finding a component
            }

            // Check for EnemyPatrol component
            EnemyPatrol patrol = other.GetComponent<EnemyPatrol>();
            if (patrol != null)
            {
                patrol.Flee(transform.position, fleeDuration);
                return; // Exit the method after finding a component
            }

            // Add additional checks here for other enemy types
        }
    }
}
