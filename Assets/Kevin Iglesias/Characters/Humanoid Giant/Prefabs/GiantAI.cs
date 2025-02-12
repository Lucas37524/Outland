using UnityEngine;
using UnityEngine.AI;

public class GiantAI : MonoBehaviour
{
    public Transform player;            // Reference to the player’s transform
    private NavMeshAgent navMeshAgent;  // The NavMeshAgent component
    private Animator animator;          // The Animator for controlling animations
    private Rigidbody rb;               // The Rigidbody component

    public float wanderRange = 20f;     // Range in which the giant will wander
    public float chaseRange = 10f;      // Range in which the giant will start chasing
    public float attackRange = 2f;      // Range in which the giant will stop chasing and attack
    public float wanderTime = 5f;       // Time to wander before idling
    public float idleTime = 3f;         // Time to idle before wandering again
    public float wanderSpeed = 3f;      // Walking speed when wandering
    public float chaseSpeed = 6f;       // Running speed when chasing

    private Vector3 wanderTarget;       // Target position for wandering
    private float timeToWander;         // Time to wait before wandering again
    private float timeToIdle;           // Time to wait before starting to wander again

    private enum State { Idle, Wandering, Chasing, Attacking }
    private State currentState = State.Idle;

    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();

        // Set up the initial settings
        navMeshAgent.isStopped = true;  // Start idle, not moving
        navMeshAgent.speed = wanderSpeed;  // Use walking speed initially
        timeToWander = wanderTime;
        timeToIdle = idleTime;
    }

    void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        // Switch behavior based on the distance to the player
        if (currentState == State.Idle || currentState == State.Wandering)
        {
            if (distanceToPlayer < chaseRange)
            {
                currentState = State.Chasing;  // Switch to chasing if player is in range
            }
            else
            {
                WanderAround();
            }
        }
        else if (currentState == State.Chasing)
        {
            if (distanceToPlayer < attackRange)
            {
                currentState = State.Attacking;  // Switch to attacking if close enough
            }
            else
            {
                ChasePlayer();
            }
        }
        else if (currentState == State.Attacking)
        {
            if (distanceToPlayer > attackRange)
            {
                currentState = State.Chasing;  // Switch back to chasing if the player moves away
            }
        }
    }

    void WanderAround()
    {
        if (timeToWander <= 0)
        {
            // Pick a new random wander target within the wander range
            Vector3 randomDirection = Random.insideUnitSphere * wanderRange;
            randomDirection += transform.position;

            // Ensure the target is on the NavMesh
            NavMeshHit hit;
            if (NavMesh.SamplePosition(randomDirection, out hit, wanderRange, NavMesh.AllAreas))
            {
                wanderTarget = hit.position;
                navMeshAgent.SetDestination(wanderTarget);
                navMeshAgent.isStopped = false; // Start moving
                animator.SetBool("IsWalking", true);
            }

            timeToWander = wanderTime;  // Reset the timer for wandering
            timeToIdle = idleTime;  // Reset the idle timer
            currentState = State.Wandering;  // Set the state to wandering
        }
        else
        {
            timeToWander -= Time.deltaTime;  // Decrease the time until wandering finishes
        }

        if (timeToIdle <= 0 && currentState == State.Wandering)
        {
            // After wandering finishes, idle for a while
            navMeshAgent.isStopped = true; // Stop moving
            animator.SetBool("IsWalking", false);  // Stop walking animation
            currentState = State.Idle;  // Set the state to idle

            timeToIdle = idleTime;  // Reset idle time
        }
        else if (currentState == State.Idle)
        {
            timeToIdle -= Time.deltaTime;  // Decrease the time until idling finishes
        }
    }

    void ChasePlayer()
    {
        navMeshAgent.SetDestination(player.position);
        navMeshAgent.isStopped = false;  // Ensure the agent is not stopped while chasing
        navMeshAgent.speed = chaseSpeed;  // Increase speed while chasing
        animator.SetBool("IsRunning", true);
    }

    void AttackPlayer()
    {
        navMeshAgent.isStopped = true;  // Stop the agent once we are close enough to attack
        animator.SetBool("IsRunning", false);
        animator.SetTrigger("Attack");  // Trigger attack animation (replace with your attack animation trigger)

        // You can add more logic here for actual damage to the player or event triggers
    }

    // Animation or other behaviors based on State
    private void PlayMovementAnimations()
    {
        if (currentState == State.Wandering || currentState == State.Chasing)
        {
            animator.SetBool("IsWalking", true);
            animator.SetBool("IsRunning", currentState == State.Chasing);
        }
        else
        {
            animator.SetBool("IsWalking", false);
            animator.SetBool("IsRunning", false);
        }
    }
}