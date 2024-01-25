using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    public enum EnemyState { Patrolling, Chasing, Fleeing }
    private EnemyState currentState = EnemyState.Patrolling;
    private EnemyState previousState;

    [Header("Patrol Settings")]
    public Transform[] movementPoints;
    private int currentPointIndex = 0;
    private float patrolSpeed = 2f;

    [Header("Chase Settings")]
    private float chaseSpeed = 3f;
    private float fleeSpeed = 4f; // Fleeing speed, faster than patrol and chase
    public Transform player;
    public float detectionRange = 5f;

    private Vector3 tempScale;

    private void Start()
    {
        SetNextPatrolPoint();
    }

    private void Update()
    {
        switch (currentState)
        {
            case EnemyState.Patrolling:
                Patrol();
                break;
            case EnemyState.Chasing:
                ChasePlayer();
                break;
            case EnemyState.Fleeing:
                // Fleeing behavior is handled in the coroutine
                break;
        }

        CheckForPlayer();
        HandleFacingDirection();
    }

    void Patrol()
    {
        if (Vector2.Distance(transform.position, movementPoints[currentPointIndex].position) < 0.1f)
        {
            SetNextPatrolPoint();
        }

        MoveTowards(movementPoints[currentPointIndex].position, patrolSpeed);
    }

    void ChasePlayer()
    {
        if (player != null)
        {
            MoveTowards(player.position, chaseSpeed);
        }
        else
        {
            currentState = EnemyState.Patrolling;
        }
    }

    void MoveTowards(Vector2 target, float speed)
    {
        transform.position = Vector2.MoveTowards(transform.position, target, Time.deltaTime * speed);
    }

    void SetNextPatrolPoint()
    {
        currentPointIndex = (currentPointIndex + 1) % movementPoints.Length;
    }

    void CheckForPlayer()
    {
        if (player != null && Vector2.Distance(transform.position, player.position) <= detectionRange)
        {
            currentState = EnemyState.Chasing;
        }
        else
        {
            currentState = EnemyState.Patrolling;
        }
    }

    void HandleFacingDirection()
    {
        tempScale = transform.localScale;

        if (transform.position.x > movementPoints[currentPointIndex].position.x)
        {
            tempScale.x = Mathf.Abs(tempScale.x);
        }
        else if (transform.position.x < movementPoints[currentPointIndex].position.x)
        {
            tempScale.x = -Mathf.Abs(tempScale.x);
        }

        transform.localScale = tempScale;
    }

    // Optional: Draw Gizmos for detection range
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRange);
    }

    public void Flee(Vector3 fearSource, float duration)
    {
        if (currentState != EnemyState.Fleeing)
        {
            previousState = currentState;
            StartCoroutine(FleeRoutine(fearSource, duration));
        }
    }

    private IEnumerator FleeRoutine(Vector3 fearSource, float duration)
    {
        currentState = EnemyState.Fleeing;
        Vector3 direction = (transform.position - fearSource).normalized;
        float endTime = Time.time + duration;

        while (Time.time < endTime)
        {
            transform.position += direction * fleeSpeed * Time.deltaTime;
            yield return null;
        }

        if (Vector2.Distance(transform.position, player.position) <= detectionRange)
            currentState = EnemyState.Chasing;
        else
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
