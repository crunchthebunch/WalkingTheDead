using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FleeBehaviour : MonoBehaviour
{
    HumanSettings settings;
    Villager owner;
    NavMeshAgent agent;
    Scanner zombieScanner;

    private void Awake()
    {
        owner = GetComponent<Villager>();
        agent = owner.Agent;
        zombieScanner = owner.ZombieScanner;
    }

    // Customise the Wander Component by the settings you give to it
    public void SetupComponent(HumanSettings settings)
    {
        this.settings = settings;
    }

    public void Flee()
    {
        if (owner.ZombieScanner.ObjectsInRange.Count > 0)
        {
            StopCoroutine(FleeFromClosestEnemy());
            StartCoroutine(FleeFromClosestEnemy());
        }
    }

    Vector3 GetFleeDirection()
    {
        // Get the closest one's location
        Vector3 fleeDirection = transform.forward;
        GameObject closestZombie = zombieScanner.GetClosestTargetInRange();

        if (closestZombie)
        {
            // Get a vector pointing towards you
            fleeDirection = transform.position - closestZombie.transform.position;
            fleeDirection.y = 0.0f;
            fleeDirection.Normalize();
        }

        return fleeDirection;
    }

    IEnumerator FleeFromClosestEnemy()
    {
        // Get the target position for the flee
        Vector3 fleePosition = transform.position + (GetFleeDirection() * settings.FleeDistance); // TODO Use a flee distance

        Debug.DrawLine(transform.position, fleePosition, Color.red);

        agent.destination = fleePosition;
        agent.speed = settings.FleeSpeed;
        agent.isStopped = false;

        // Only change directions every 1 seconds
        yield return null;
    }
}
