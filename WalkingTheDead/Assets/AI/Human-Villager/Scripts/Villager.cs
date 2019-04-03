using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Villager : MonoBehaviour
{
    [SerializeField] HumanSettings stats;

    bool isReadyToWander;
    int zombieLayerMask;

    Scanner zombieScanner;
    
    Vector3 navigationCenter = Vector3.down;
    NavMeshAgent agent;

    public Vector3 NavigationCenter { get => navigationCenter; }
    public NavMeshAgent Agent { get => agent; set => agent = value; }
    public HumanSettings Stats { get => stats; set => stats = value; }
    public bool CanTravel { get => isReadyToWander; }
    public Scanner ZombieScanner { get => zombieScanner; }

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.speed = stats.MovementSpeed;

        // Create the scanner
        SetupZombieScanner();

        navigationCenter = transform.position;

        isReadyToWander = true;
    }

    private void SetupZombieScanner()
    {
        zombieScanner = GetComponent<Scanner>();
        zombieScanner.SetupScanner("Zombie", stats.Vision);
    }

    private void Start()
    {
        // Scan around for zombies continuously
        // If there are no zombies, wander around
        // If there are zombies, flee
        // If out of zone AND not fleeing, go back to standard zone
        MoveBackToOriginalPosition();
    }

    private void Update()
    {
        print(zombieScanner.ObjectsInRange.Count);
    }

    public void MoveBackToOriginalPosition()
    {
        agent.SetDestination(navigationCenter);
    }

    public void Wander()
    {
        if (isReadyToWander)
        {
            StopCoroutine(WanderAround());
            StartCoroutine(WanderAround());
        }
        
    }

    IEnumerator WanderAround()
    {
        isReadyToWander = false;
        // Set a random destination for wandering
        agent.SetDestination(GetRandomLocationInRadius());

        // Reset the wander time when due
        StopCoroutine(WaitForNextWander());
        StartCoroutine(WaitForNextWander());

        yield return null;
    }

    IEnumerator WaitForNextWander()
    {
        // Dont reset until the destination is reached
        while (Vector3.Distance(transform.position, agent.destination) > 1.0f)
        {
            yield return null;
        }

        // Wait for the wander delay to be reset
        yield return new WaitForSeconds(stats.WanderDelay);

        // Reset the time
        isReadyToWander = true;
    }


    

    // IEnumerator Flee
    // IEnumerator wander
    // Return to Navigation zone

    Vector3 GetRandomLocationInRadius()
    {
        print("Getting Random Location");
        // Get a random direction within radius of the navigation Field
        Vector3 randomDirection = Random.insideUnitSphere * Random.Range(stats.WalkRadius / 4f, stats.WalkRadius);
        randomDirection += navigationCenter;

        NavMeshHit hit;

        Vector3 walkableTarget = Vector3.zero;

        // Check whether  there is anything where he can travel to
        if (NavMesh.SamplePosition(randomDirection, out hit, stats.WalkRadius, 1))
        {
            walkableTarget = hit.position;
        }

        return walkableTarget;
    }


    private void OnDrawGizmos()
    {
        if (navigationCenter == Vector3.down)
        {
            Gizmos.color = Color.black;
            Gizmos.DrawWireSphere(transform.position, stats.WalkRadius);
        }
        else
        {
            Gizmos.color = Color.black;
            Gizmos.DrawWireSphere(navigationCenter, stats.WalkRadius);
        }
        
    }
}
