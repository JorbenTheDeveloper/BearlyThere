using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 10.5f;
    private float originalSpeed; // To store the original speed
    private float boostedSpeed; // Speed when boosted
    public GameObject effectPrefab; // Assign the prefab in the editor
    public float effectDuration = 5f; // Duration of the fear effect object
    public GameObject effectSpawnPoint; // Assign the child GameObject here for fear effect

    public float stamina = 300f;
    private float maxStamina = 300f;
    public Slider staminaBar; // Assign this in the Unity Editor

    private void Start()
    {
        originalSpeed = speed; // Store the original speed
        boostedSpeed = speed * 2f; // Define boosted speed as double the original speed

        // Initialize stamina bar
        staminaBar.maxValue = maxStamina;
        staminaBar.value = stamina;
    }

    void Update()
    {
        // Check for left mouse button
        if (Input.GetMouseButton(0) && stamina > 0)
        {
            speed = boostedSpeed; // Boost the speed
            DrainStamina(2 * Time.deltaTime); // Increased stamina drain when speed is boosted
        }
        else
        {
            speed = originalSpeed; // Revert to original speed
        }

        // Movement code
        Vector3 pos = transform.position;

        if (Input.GetKey("w"))
        {
            pos.y += speed * Time.deltaTime;
        }
        if (Input.GetKey("s"))
        {
            pos.y -= speed * Time.deltaTime;
        }
        if (Input.GetKey("d"))
        {
            pos.x += speed * Time.deltaTime;
        }
        if (Input.GetKey("a"))
        {
            pos.x -= speed * Time.deltaTime;
        }

        transform.position = pos;

        // Ability trigger
        if (Input.GetKeyDown(KeyCode.Space) && stamina >= 10)
        {
            TriggerAbility();
            DrainStamina(10); // Drain stamina when ability is used
        }

        // Normal stamina drain
        DrainStamina(1 * Time.deltaTime);

        // Update the stamina bar
        staminaBar.value = stamina;

        if (stamina == 0)
        {

        }
    }

    void TriggerAbility()
    {
        // Create the effect object at the spawn point's position
        GameObject effect = Instantiate(effectPrefab, effectSpawnPoint.transform.position, Quaternion.identity);
        Destroy(effect, effectDuration); // Destroy the effect object after the ability duration ends
    }

    private void DrainStamina(float amount)
    {
        stamina -= amount;
        stamina = Mathf.Clamp(stamina, 0, maxStamina);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Door door = other.GetComponent<Door>();
        if (door != null)
        {
            door.Teleport(gameObject); // Teleport the player using the door's Teleport method
        }
    }
}

