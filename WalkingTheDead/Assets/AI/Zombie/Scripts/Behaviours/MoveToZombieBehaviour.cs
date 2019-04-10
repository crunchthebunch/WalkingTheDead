using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MoveToZombieBehaviour : Behaviour
{
    ZombieSettings settings;
    Zombie owner;

    NavMeshAgent agent;

    public override void DoBehaviour()
    {
        MoveTo();
    }

    public override void SetupComponent(AISettings settings)
    {
        this.settings = settings as ZombieSettings;
    }

    private void Awake()
    {
        owner = GetComponent<Zombie>();
        agent = owner.Agent;
    }
    void MoveTo()
    {
        agent.SetDestination(owner.DesiredPosition);
        agent.speed = settings.WalkingSpeed;
        agent.isStopped = false;
        if (Mathf.Abs(Vector3.Distance(transform.position, owner.DesiredPosition)) < settings.WalkRadius)
        {
            owner.CommandGiven = false;
        }
    }
}
