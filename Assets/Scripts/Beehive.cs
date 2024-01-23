using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Beehive : MonoBehaviour
{
    public GameObject beePrefab;
    public Transform[] beeSpawnPoints; // Array of spawn points for bees
    public Transform player; // Reference to the player

    private void Start()
    {
        SpawnBees();
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
}
