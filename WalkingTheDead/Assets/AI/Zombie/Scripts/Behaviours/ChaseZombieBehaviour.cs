using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ChaseZombieBehaviour : Behaviour
{
    ZombieSettings settings;
    Zombie owner;
    NavMeshAgent agent;
    Scanner humanScanner;

    // Start is called before the first frame update
    void Start()
    {
        owner = GetComponent<Zombie>();
        agent = owner.Agent;
        humanScanner = owner.HumanScanner;
    }

    // Customise the Wander Component by the settings you give to it
    public override void SetupComponent(AISettings settings)
    {
        this.settings = settings as ZombieSettings;
    }

    public override void DoBehaviour()
    {
        // Keep Calculating New Flee routes until there are zombies around
        if (owner.HumanScanner.ObjectsInRange.Count > 0)
        {
            StopCoroutine(ChaseClosestHuman()); // See if this needs to be a Coroutine
            StartCoroutine(ChaseClosestHuman());
        }
    }

    IEnumerator ChaseClosestHuman()
    {
        // Find the closest Human
        GameObject closestHuman = humanScanner.GetClosestTargetInRange();
        

        // If there is a human in range
        if (closestHuman)
        {
            Vector3 lastSeenPosition = closestHuman.transform.position;

            // Draw line between closest human and the zombie
            Debug.DrawLine(transform.position, lastSeenPosition, Color.red);

            // If the human changed position, follow it
            if (agent.destination != lastSeenPosition)
            {
                agent.destination = lastSeenPosition;
                agent.speed = settings.ChaseSpeed;
                agent.isStopped = false;
            }
        }

        yield return null;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
