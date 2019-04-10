using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WanderZombieBehaviour : Behaviour
{
    ZombieSettings settings;
    Zombie owner;

    NavMeshAgent agent;
    bool startedWandering;
    
    public override void DoBehaviour()
    {
        if(!startedWandering)
        {
            Vector3 wanderPosition = owner.DesiredPosition + Random.onUnitSphere * settings.WalkRadius;
            wanderPosition = owner.DesiredPosition + Random.onUnitSphere * settings.WalkRadius;
            agent.SetDestination(wanderPosition);
            startedWandering = true;
        }

        if (Random.value < settings.WanderChance)
        {
            Wander();
        }
    }

    public override void SetupComponent(AISettings settings)
    {
        this.settings = settings as ZombieSettings;
    }

    private void Awake()
    {
        owner = GetComponent<Zombie>();
        agent = owner.Agent;
        startedWandering = false;

    }

    void Wander()
    {
        Vector3 wanderPosition = owner.DesiredPosition + Random.insideUnitSphere * settings.WalkRadius;
        agent.SetDestination(wanderPosition);
    }
}
