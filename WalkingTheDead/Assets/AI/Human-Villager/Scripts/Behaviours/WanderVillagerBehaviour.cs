using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WanderVillagerBehaviour : Behaviour
{
    Villager owner;
    NavMeshAgent agent;
    VillagerSettings settings;
    bool isReadyToWander;
    Vector3 navigationCenter = Vector3.down;

    public bool IsReadyToWander { get => isReadyToWander; }

    private void Awake()
    {
        owner = GetComponent<Villager>();
        agent = owner.Agent;

        navigationCenter = transform.position;
        isReadyToWander = true;
    }

    public override void SetupComponent(AISettings settings)
    {
        this.settings = settings as VillagerSettings;
    }

    public override void DoBehaviour()
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
        yield return new WaitForSeconds(settings.WanderDelay);

        // Reset the time
        isReadyToWander = true;
    }

    Vector3 GetRandomLocationInRadius()
    {
        // Get a random direction within radius of the navigation Field
        Vector3 randomDirection = Random.insideUnitSphere * Random.Range(settings.WalkRadius / 4f, settings.WalkRadius);
        randomDirection += navigationCenter;

        NavMeshHit hit;

        Vector3 walkableTarget = Vector3.zero;

        // Check whether  there is anything where he can travel to
        if (NavMesh.SamplePosition(randomDirection, out hit, settings.WalkRadius, 1))
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
            Gizmos.DrawWireSphere(transform.position, settings.WalkRadius);
        }
        else
        {
            Gizmos.color = Color.black;
            Gizmos.DrawWireSphere(navigationCenter, settings.WalkRadius);
        }

    }
}
