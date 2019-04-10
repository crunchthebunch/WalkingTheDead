using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class AttackMeleeSoldierBehaviour : Behaviour
{
    MeleeSoldierSettings settings;
    MeleeSoldier owner;
    NavMeshAgent agent;
    Scanner zombieScanner;
    float timeTillNextAttack;

    public override void DoBehaviour()
    {
        if (timeTillNextAttack < 0.0f)
        {
            AttackClosestZombie();
            timeTillNextAttack = settings.AttackDelay;
        }
        else
        {
            timeTillNextAttack -= Time.deltaTime;
        }
    }

    private void Awake()
    {
        owner = GetComponent<MeleeSoldier>();
        agent = owner.Agent;
        settings = owner.Settings;
        zombieScanner = owner.ZombieScanner;
        timeTillNextAttack = 0.0f;
    }

    void AttackClosestZombie()
    {
        GameObject closestZombie = zombieScanner.GetClosestTargetInRange();

        // If there are any zombies in range
        if (closestZombie)
        {
            // If the closest Zombie is in range
            if (Vector3.Distance(closestZombie.transform.position, transform.position) < settings.AttackDistance)
            {
                // Play Attack Animation
                // If the sword attack hits --> Kill Zombie
                // TODO Remove destruction now
                Destroy(closestZombie);
            }
        }
    }
}
