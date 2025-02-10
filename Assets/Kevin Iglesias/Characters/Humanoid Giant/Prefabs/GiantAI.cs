using UnityEngine;

public class GiantAI : MonoBehaviour
{
    public Transform player;  // Reference to the player's position
    public float chaseRange = 10f;  // The range at which the giant starts chasing
    public float walkSpeed = 2f;  // Walking speed
    public float idleTime = 2f;   // Time spent idling before moving

    private Animator animator;
    private UnityEngine.AI.NavMeshAgent navMeshAgent;

    void Start()
    {
        animator = GetComponent<Animator>();
        navMeshAgent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        navMeshAgent.speed = walkSpeed;
    }

    void Update()
    {
        // Check the distance to the player
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (distanceToPlayer <= chaseRange)
        {
            // Player is within chase range, start walking towards them
            StartWalkingTowardsPlayer();
        }
        else
        {
            // Player is out of range, stop walking
            StopWalking();
        }
    }

    void StartWalkingTowardsPlayer()
    {
        // Set the NavMeshAgent destination to the player's position
        navMeshAgent.SetDestination(player.position);

        // Trigger walking animation
        animator.SetTrigger("StartWalking");
    }

    void StopWalking()
    {
        // Stop the NavMeshAgent
        navMeshAgent.SetDestination(transform.position);  // Stop moving

        // Trigger idle animation
        animator.SetTrigger("StopWalking");
    }
}