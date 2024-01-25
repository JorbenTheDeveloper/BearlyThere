using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 10.5f;
    private float originalSpeed;
    private float boostedSpeed;
    public GameObject effectPrefab;
    public float effectDuration = 5f;
    public GameObject effectSpawnPoint;

    public float stamina = 300f;
    private float maxStamina = 300f;
    public Slider staminaBar;
    public TextMeshProUGUI staminaText;

    public float abilityCooldown = 5f;
    private float abilityTimer;
    public Image abilityActiveImage; // Shown when ability is ready
    public Image abilityCooldownImage; // Shown when ability is on cooldown
    public TextMeshProUGUI cooldownText;

    [SerializeField] private GameObject _crow;

    private void Start()
    {
        originalSpeed = speed;
        boostedSpeed = speed * 2f;

        staminaBar.maxValue = maxStamina;
        staminaBar.value = stamina;

        abilityTimer = abilityCooldown;
    }

    void Update()
    {
        HandleMovement();
        HandleAbilityUse();
        UpdateUI();
        DrainStamina(1 * Time.deltaTime);  // Stamina drain of 1 per second
    }

    private void HandleMovement()
    {
        if (Input.GetMouseButton(0) && stamina > 0)
        {
            speed = boostedSpeed;
            DrainStamina(2 * Time.deltaTime);
        }
        else
        {
            speed = originalSpeed;
        }

        Vector3 pos = transform.position;
        if (Input.GetKey("w")) { pos.y += speed * Time.deltaTime; }
        if (Input.GetKey("s")) { pos.y -= speed * Time.deltaTime; }
        if (Input.GetKey("d")) { pos.x += speed * Time.deltaTime; }
        if (Input.GetKey("a")) { pos.x -= speed * Time.deltaTime; }

        transform.position = pos;
    }

    private void HandleAbilityUse()
    {
        if (abilityTimer < abilityCooldown)
        {
            abilityTimer += Time.deltaTime;
            abilityActiveImage.gameObject.SetActive(false);
            abilityCooldownImage.gameObject.SetActive(true);

            // Update cooldown text to show remaining time
            cooldownText.text = "" + Mathf.RoundToInt(abilityCooldown - abilityTimer).ToString();
        }
        else
        {
            abilityActiveImage.gameObject.SetActive(true);
            abilityCooldownImage.gameObject.SetActive(false);
            cooldownText.text = ""; // Clear cooldown text when ability is ready
        }

        if (Input.GetKeyDown(KeyCode.Space) && stamina >= 10 && abilityTimer >= abilityCooldown)
        {
            TriggerAbility();
            DrainStamina(10);
            abilityTimer = 0f;
        }
    }

    private void UpdateUI()
    {
        staminaBar.value = stamina;
        staminaText.text = "" + Mathf.RoundToInt(stamina).ToString();
    }

    void TriggerAbility()
    {
        GameObject effect = Instantiate(effectPrefab, effectSpawnPoint.transform.position, Quaternion.identity);
        Destroy(effect, effectDuration);
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
            door.Teleport(gameObject);
        }

        if (other.name == "Crow")
        {
            _crow.SetActive(true);
        }
    }
}

