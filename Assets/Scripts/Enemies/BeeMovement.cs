using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeeMovement : MonoBehaviour
{
    public enum BeeState { Idle, Chasing, Fleeing }
    private BeeState currentState = BeeState.Idle;
    private Vector3 startPosition;
    private float moveSpeed = 2f;

    public Transform player;
    public float detectionRange = 5f;
    public Transform beehive;

    private void Start()
    {
        startPosition = (beehive != null) ? beehive.position : transform.position;
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

    public void Flee(float duration)
    {
        if (currentState != BeeState.Fleeing)
        {
            StartCoroutine(FleeRoutine(duration));
        }
    }

    private IEnumerator FleeRoutine(float duration)
    {
        currentState = BeeState.Fleeing;

        float endTime = Time.time + duration;
        while (Time.time < endTime)
        {
            // Move the bee towards the beehive
            if (beehive != null)
            {
                transform.position = Vector2.MoveTowards(transform.position, beehive.position, moveSpeed * Time.deltaTime);
            }
            yield return null;
        }

        currentState = BeeState.Idle; // Return to Idle after fleeing
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("PlayerCharacter"))
        {
            PlayerMovement player = collision.gameObject.GetComponent<PlayerMovement>();
            if (player != null)
            {
                player.LoseStamina(5); // Reduce player stamina by 5
            }
        }
    }
}
