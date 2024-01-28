using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    public enum EnemyState { Patrolling, Chasing, Fleeing, Attacking }
    private EnemyState currentState = EnemyState.Patrolling;

    [Header("Patrol Settings")]
    public Transform[] movementPoints;
    private int currentPointIndex = 0;
    private float patrolSpeed = 2f;

    [Header("Chase Settings")]
    private float chaseSpeed = 3f;
    private float fleeSpeed = 4f; // Fleeing speed, faster than patrol and chase
    public Transform player;
    public float detectionRange = 5f;
    public float attackDistance = 1f; // Distance at which enemy will stop to attack
    public float attackCooldown = 1f; // Time between attacks

    private bool isAttacking = false;
    private float lastAttackTime = 0f;

    public AudioSource duckSounds;
    public float audioRange = 10f;
    public float maxVolume = 0.5f;

    

    private void Start()
    {
        SetNextPatrolPoint();
       // duckSounds.Play();
    }

    private void Update()
    {



        float distanceToPlayer = Vector2.Distance(transform.position, player.position);


        



        if (currentState == EnemyState.Fleeing)
        {
            // Fleeing logic is handled in the FleeRoutine coroutine
            return;
        }

        if (!isAttacking && distanceToPlayer <= attackDistance && Time.time >= lastAttackTime + attackCooldown)
        {
            StartCoroutine(AttackPlayer());
        }
        else if (distanceToPlayer <= detectionRange)
        {
            currentState = EnemyState.Chasing;
            ChasePlayer();
        }
        else
        {
            currentState = EnemyState.Patrolling;
            Patrol();
        }

        if (currentState == EnemyState.Chasing || currentState == EnemyState.Attacking)
        {
            HandleFacingDirection();
        }
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
    }

    void MoveTowards(Vector2 target, float speed)
    {
        transform.position = Vector2.MoveTowards(transform.position, target, Time.deltaTime * speed);
       
    }

    void SetNextPatrolPoint()
    {
        currentPointIndex = (currentPointIndex + 1) % movementPoints.Length;
    }

    IEnumerator AttackPlayer()
    {
        isAttacking = true;
        lastAttackTime = Time.time;

        PlayerMovement playerMovement = player.GetComponent<PlayerMovement>();
        if (playerMovement != null)
        {
            playerMovement.LoseStamina(5);
        }

        yield return new WaitForSeconds(attackCooldown);

        isAttacking = false;
    }

    public void Flee(float duration)
    {
        if (currentState != EnemyState.Fleeing)
        {
            StartCoroutine(FleeRoutine(duration));
        }
    }

    private IEnumerator FleeRoutine(float duration)
    {
        currentState = EnemyState.Fleeing;
        Vector3 fleeDirection = ChooseFleeDirection();
        float endTime = Time.time + duration;

        while (Time.time < endTime)
        {
            transform.position += fleeDirection * fleeSpeed * Time.deltaTime;
            yield return null;
        }

        currentState = EnemyState.Patrolling; // Return to patrolling after fleeing
    }

    private Vector3 ChooseFleeDirection()
    {
        // Implement logic to choose a flee direction.
        return Random.insideUnitCircle.normalized;
    }

    void HandleFacingDirection()
    {
        if (player != null)
        {
            // Determine the direction of the player relative to the enemy
            float direction = player.position.x - transform.position.x;

            // Flip the sprite based on the direction
            if (direction > 0)
            {
                // Player is to the right, face right
                transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
            }
            else if (direction < 0)
            {
                // Player is to the left, face left (original orientation)
                transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
            }
        }
    }
}
