using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Villager : MonoBehaviour
{

    [SerializeField] float movementSpeed = 2.0f;
    [SerializeField] float walkRadius = 10f;
    [SerializeField] float wanderDelay = 2.0f;

    [SerializeField] float fleeSpeed = 5.0f;
    bool isFleeing;

    bool isAlive;
    
    Vector3 navigationCenter;
    NavMeshAgent agent;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.speed = movementSpeed;

        navigationCenter = transform.position;

        isFleeing = false;
        isAlive = true;
    }

    private void Start()
    {
        StartCoroutine(WanderAround());
        // Scan around for zombies continuously
        // If there are no zombies, wander around
        // If there are zombies, flee
        // If out of zone AND not fleeing, go back to standard zone
    }

    private void Update()
    {
    }

    IEnumerator WanderAround()
    {
        print("Destination Set.");
        // Set a random destination for wandering
        agent.SetDestination(GetRandomLocationInRadius());

        // Reset the wander time when due
        StopCoroutine(WaitForNextWander());
        StartCoroutine(WaitForNextWander());

        print("Coroutine Ended.");
        yield return null;
    }

    IEnumerator WaitForNextWander()
    {
        // Dont reset until the destination is reached
        while (Vector3.Distance(transform.position, agent.destination) > 1.0f)
        {
            print("Agent not Stopped");
            yield return null;
        }

        // Wait for the wander delay to be reset
        print("Destination Reached.");
        yield return new WaitForSeconds(wanderDelay);

        // Reset the time
        print("Time Reset.");

        StartCoroutine(WanderAround());
    }

    // Move to Target
    void MoveToWanderLocation()
    {
        agent.SetDestination(GetRandomLocationInRadius());
    }

    void MoveBackToOriginalPosition()
    {
        agent.SetDestination(navigationCenter);
    }

    // IEnumerator Flee
    // IEnumerator wander
    // Return to Navigation zone

    // Select a new location
    Vector3 GetRandomLocationInRadius()
    {
        // Get a random direction within radius of the navigation Field
        Vector3 randomDirection = Random.insideUnitSphere * Random.Range(walkRadius / 4f, walkRadius);
        randomDirection += navigationCenter;

        NavMeshHit hit;

        Vector3 walkableTarget = Vector3.zero;

        // Check whether  there is anything where he can travel to
        if (NavMesh.SamplePosition(randomDirection, out hit, walkRadius, 1))
        {
            walkableTarget = hit.position;
        }

        return walkableTarget;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(Vector3.zero, walkRadius);
    }
}
