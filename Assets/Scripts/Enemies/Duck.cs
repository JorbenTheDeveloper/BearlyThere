using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Duck : MonoBehaviour
{
    [Header("Patrol Settings")]
   // public Transform[] movementPoints;
    // Fleeing speed, faster than patrol and chase
    public Transform player;
    public float detectionRange = 5f;
    public float attackDistance = 1f; // Distance at which enemy will stop to attack
    public float attackCooldown = 1f; // Time between attacks

    

    public AudioSource duckSounds;
    public float audioRange = 10f;
    public float maxVolume = 0.5f;


    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("PlayerCharacter").transform;
        duckSounds = GetComponent<AudioSource>();
        duckSounds.Play();
    }

    // Update is called once per frame
    void Update()
    {


        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        // Adjust the volume based on distance within the audio range
        if (distanceToPlayer <= audioRange)
        {
            float volume = 1.0f - (distanceToPlayer / audioRange); // Calculate volume based on distance
            duckSounds.volume = maxVolume * volume; // Set the volume of the bee sound
        }
        else
        {
            duckSounds.volume = 0f; // Player is outside the audio range, set volume to 0
        }

    }
}
