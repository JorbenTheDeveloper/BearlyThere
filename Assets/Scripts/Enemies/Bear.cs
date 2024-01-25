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

    private void Start()
    {
        originalScale = transform.localScale; // Store the original scale
        StartCoroutine(BearRoutine()); // Start the bear behavior routine
    }

    IEnumerator BearRoutine()
    {
        while (true) // Infinite loop to repeat the behavior
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
}
