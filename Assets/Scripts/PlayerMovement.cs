using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 10.5f;
    public GameObject effectPrefab; // Assign the prefab for the effect in the editor
    public float effectDuration = 5f;
    public GameObject effectSpawnPoint;

    [SerializeField] private GameObject _crow;

    void Update()
    {
        // Movement logic
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
        if (Input.GetKeyDown(KeyCode.Space))
        {
            TriggerAbility();
        }
    }

    void TriggerAbility()
    {
        GameObject effect = Instantiate(effectPrefab, transform.position - new Vector3(1, 0, 0), Quaternion.identity);
        Destroy(effect, effectDuration);  // Destroy the effect object after 'effectDuration' seconds
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.name == "Crow")
        {
            _crow.SetActive(true);
        }
    }
}
