using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AttackZombieBehaviour : Behaviour
{
    ZombieSettings settings;
    Zombie owner;
    Scanner ownerScanner;

    NavMeshAgent agent;

    public override void DoBehaviour()
    {
        AttackClosestVillager();
    }

    private void Awake()
    {
        owner = GetComponent<Zombie>();
        agent = owner.Agent;
        settings = owner.Settings;
        ownerScanner = owner.HumanScanner;
    }

    void AttackClosestVillager()
    {
        GameObject ToKill = owner.HumanScanner.GetClosestTargetInRange();
        

        if (ToKill != null && Vector3.Distance(owner.transform.position, ToKill.transform.position) < settings.AttackRange/1.5f)
        {
            owner.Anim.SetTrigger("Attack");
            agent.velocity = agent.velocity/2.0f;
        }
    }
}
