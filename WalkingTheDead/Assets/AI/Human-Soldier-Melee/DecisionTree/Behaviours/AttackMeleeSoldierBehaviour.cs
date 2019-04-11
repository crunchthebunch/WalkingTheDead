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
    bool isReadyToAttack;

    public bool IsReadyToAttack { get => isReadyToAttack; }

    public override void DoBehaviour()
    {
        if (owner.ZombieScanner.ObjectsInRange.Count > 0)
        {
            StopCoroutine(AttackClosestEnemy());
            StartCoroutine(AttackClosestEnemy());
        }

        if (!isReadyToAttack)
        {
            print("Can't attack");
        }
    }

    private void Awake()
    {
        owner = GetComponent<MeleeSoldier>();
        agent = owner.Agent;
        settings = owner.Settings;
        zombieScanner = owner.ZombieScanner;
        timeTillNextAttack = 0.0f;
        isReadyToAttack = true;
    }

    IEnumerator AttackClosestEnemy()
    {
        GameObject closestZombie = zombieScanner.GetClosestTargetInRange();

        // If there are any zombies in range
        if (closestZombie)
        {
            Vector3 zombiePosition = closestZombie.transform.position;

            // If the closest Zombie is in range
            while (Vector3.Distance(zombiePosition, transform.position) > settings.AttackDistance)
            {
                yield return null;
            }

            // Attack if ready to attack
            if (isReadyToAttack)
            {
                 // Kill the zombie
                 KillEnemy(closestZombie);
            }

            yield return null;
        }
    }

    IEnumerator WaitForNextAttack()
    {
        isReadyToAttack = false;
        

        // Dont reset until the destination is reached
        while (timeTillNextAttack > 0.0f)
        {
            timeTillNextAttack -= Time.deltaTime;
            yield return null;
        }

        // yield return new WaitForSeconds(settings.AttackDelay);
        timeTillNextAttack = settings.AttackDelay;

        isReadyToAttack = true;
        yield return null;
    }

    private void KillEnemy(GameObject closestEnemy)
    {
        Zombie potentiallyZombie;

        if (closestEnemy)
        {
            potentiallyZombie = closestEnemy.GetComponent<Zombie>();

            // See if this is a zombie
            if (potentiallyZombie)
            {
                Destroy(closestEnemy.gameObject); // TODO Remove destruction now
                print("Killing zombie!");
                owner.GameManager.numberOFZombies++;
            }
            // Else its a necromancer
            else
            {
                PlayerMovement necroMancer = closestEnemy.GetComponent<PlayerMovement>();

                if (necroMancer)
                {
                    print("Killing player!");
                    owner.GameManager.playerHealth -= 10.0f;
                }
            }

            
        }

        // Play Attack Animation
        // If the sword attack hits --> Kill Zombie
        // Start countback for delay --> Kill Enemy

        StopCoroutine(WaitForNextAttack());
        StartCoroutine(WaitForNextAttack());

    }
}
