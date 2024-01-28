using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Beehive : MonoBehaviour
{
    public GameObject beePrefab;
    public Transform[] beeSpawnPoints; // Array of spawn points for bees
    public Transform player; // Reference to the player
    public float spawnRange = 10f; // Range within which bees will spawn

    private bool beesSpawned = false; // To check if bees have already been spawned

    public AudioSource beeSound;
    public float audioRange = 20f;
    public float maxVolume = 1.0f;

    private void Update()
    {
        // Check the distance between the player and the beehive
        if (!beesSpawned && player != null && Vector3.Distance(transform.position, player.position) <= spawnRange)
        {
            SpawnBees();
            beeSound.Play();
            beesSpawned = true; // Prevents bees from spawning repeatedly
        }

        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        // Adjust the volume based on distance within the audio range
        if (distanceToPlayer <= audioRange)
        {
            float volume = 1.0f - (distanceToPlayer / audioRange); // Calculate volume based on distance
            beeSound.volume = maxVolume * volume; // Set the volume of the bee sound
        }
        else
        {
            beeSound.volume = 0f; // Player is outside the audio range, set volume to 0
        }

    }

    void SpawnBees()
    {
        for (int i = 0; i < beeSpawnPoints.Length; i++)
        {
            if (i < beeSpawnPoints.Length && beeSpawnPoints[i] != null)
            {
                GameObject bee = Instantiate(beePrefab, beeSpawnPoints[i].position, Quaternion.identity);
                BeeMovement beeMovement = bee.GetComponent<BeeMovement>();

                if (beeMovement != null)
                {
                    beeMovement.player = player;
                    beeMovement.beehive = transform;
                }
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow; // Set the color for the Gizmos
        Gizmos.DrawWireSphere(transform.position, spawnRange); // Draw a wire sphere representing the spawn range
    }
}
