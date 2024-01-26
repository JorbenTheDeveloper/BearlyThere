using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bear : MonoBehaviour
{
    public Vector3 maxScale; // Maximum scale the bear will grow to
    public GameObject fartEffectPrefab; // Assign the fart effect prefab in the editor
    public float minGrowDuration = 3f; // Minimum duration to reach max scale
    public float maxGrowDuration = 7f; // Maximum duration to reach max scale
    public float shrinkDuration = 5f; // Time taken to shrink back to original size

    private Vector3 originalScale; // Store the original scale of the bear
    public float detectionRadius = 5f; // Radius within which the bear detects the player
    private bool playerInRange = false;

    private void Start()
    {
        originalScale = transform.localScale; // Store the original scale
        StartCoroutine(BearRoutine()); // Start the bear behavior routine
    }

    private void Update()
    {
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(transform.position, detectionRadius);
        playerInRange = false;

        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.CompareTag("PlayerCharacter"))
            {
                playerInRange = true;
                break;
            }
        }
    }

    IEnumerator BearRoutine()
    {
        while (true)
        {
            if (playerInRange)
            {
                // Generate a random duration for growing
                float randomGrowDuration = Random.Range(minGrowDuration, maxGrowDuration);

                // Scale up
                yield return ScaleOverTime(maxScale, randomGrowDuration);

                // Fart
                GameObject fart = Instantiate(fartEffectPrefab, transform.position, Quaternion.identity);
                Destroy(fart, 2f); // Destroy the fart effect after a short duration

                // Scale down
                yield return ScaleOverTime(originalScale, shrinkDuration);
            }
            else
            {
                yield return null; // Wait for the next frame before checking again
            }
        }
    }

    IEnumerator ScaleOverTime(Vector3 targetScale, float duration)
    {
        float time = 0;
        Vector3 startScale = transform.localScale;

        while (time < duration)
        {
            transform.localScale = Vector3.Lerp(startScale, targetScale, time / duration);
            time += Time.deltaTime;
            yield return null;
        }

        transform.localScale = targetScale; // Ensure the final scale is set accurately
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }
}
