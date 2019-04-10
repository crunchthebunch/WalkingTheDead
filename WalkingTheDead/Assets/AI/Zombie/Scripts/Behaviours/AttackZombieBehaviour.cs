using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AttackZombieBehaviour : Behaviour
{
    ZombieSettings settings;
    Zombie owner;

    NavMeshAgent agent;

    public override void DoBehaviour()
    {
        AttackClosestVillager();
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

    void AttackClosestVillager()
    {
        owner.ToKill = owner.HumanScanner.GetClosestTargetInRange();

        if (owner.ToKill != null)
        {
            owner.ToKill = owner.HumanScanner.GetClosestTargetInRange();
            owner.HumanScanner.ObjectsInRange.Remove(owner.ToKill);
            GameObject.Destroy(owner.ToKill);
            owner.ToKill = null;
        }
    }
}
