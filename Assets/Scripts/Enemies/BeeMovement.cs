using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeeMovement : MonoBehaviour
{
    public enum BeeState { Idle, Chasing, Fleeing }
    private BeeState currentState = BeeState.Idle;
    private BeeState previousState;
    private Vector3 startPosition;
    private float moveSpeed = 2f;

    public Transform player;
    public float detectionRange = 5f;
    public Transform beehive;

    

    private void Start()
    {
        startPosition = transform.position; // Store the starting position (or set this to beehive.position if you want them to return to the beehive)
    }

    private void Update()
    {
        switch (currentState)
        {
            case BeeState.Idle:
                IdleBehavior();
                break;
            case BeeState.Chasing:
                ChasePlayer();
                break;
        }

        CheckForPlayer();
    }

    void IdleBehavior()
    {
        if (Vector2.Distance(transform.position, startPosition) > 1f)
        {
            transform.position = Vector2.MoveTowards(transform.position, startPosition, Time.deltaTime * moveSpeed);
        }
    }

    void ChasePlayer()
    {
        if (player != null && Vector2.Distance(transform.position, player.position) <= detectionRange)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.position, Time.deltaTime * moveSpeed);
        }
        else
        {
            currentState = BeeState.Idle;
        }
    }

    void CheckForPlayer()
    {
        if (player != null && Vector2.Distance(transform.position, player.position) <= detectionRange)
        {
            currentState = BeeState.Chasing;
        }
        else if (currentState == BeeState.Chasing)
        {
            currentState = BeeState.Idle;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRange);
    }

    public void Flee(Vector3 fearSource, float duration)
    {
        if (currentState != BeeState.Fleeing)
        {
            previousState = currentState;
            StartCoroutine(FleeRoutine(fearSource, duration));
        }
    }

    private IEnumerator FleeRoutine(Vector3 fearSource, float duration)
    {
        currentState = BeeState.Fleeing;
        Vector3 direction = (transform.position - fearSource).normalized;
        float endTime = Time.time + duration;

        while (Time.time < endTime)
        {
            transform.position += direction * moveSpeed * Time.deltaTime;
            yield return null;
        }

        currentState = previousState; // Return to the previous state
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("PlayerCharacter"))
        {
            PlayerMovement player = other.GetComponent<PlayerMovement>();
            if (player != null)
            {
                player.LoseStamina(5); // Assuming LoseStamina method exists in PlayerMovement
            }
        }
    }
}
