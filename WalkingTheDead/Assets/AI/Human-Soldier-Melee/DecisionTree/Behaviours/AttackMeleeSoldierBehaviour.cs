﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class AttackMeleeSoldierBehaviour : Behaviour
{
    MeleeSoldierSettings settings;
    MeleeSoldier owner;

    NavMeshAgent agent;
    Scanner zombieScanner;
    Animator animator;
    HitAnimationEvent animationEvent;

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
    }

    private void Awake()
    {
        owner = GetComponent<MeleeSoldier>();
        agent = owner.Agent;
        settings = owner.Settings;
        zombieScanner = owner.ZombieScanner;
        animator = owner.Animator;
        animationEvent = GetComponentInChildren<HitAnimationEvent>(); // Create this instead of finding it

        timeTillNextAttack = 0.0f;
        isReadyToAttack = true;
    }

    IEnumerator AttackClosestEnemy()
    {
        GameObject closestEnemy = zombieScanner.GetClosestTargetInRange();

        // If there are any zombies in range
        if (closestEnemy)
        {
            Vector3 zombiePosition = closestEnemy.transform.position;

            // If the closest Zombie is in range
            while (Vector3.Distance(zombiePosition, transform.position) > settings.AttackDistance)
            {
                yield return null;
            }

            // Attack if ready to attack
            if (isReadyToAttack)
            {
                // Kill the zombie
                animationEvent.SetClosestEnemy(closestEnemy);
                animator.SetTrigger("attack");
            }

            yield return null;
        }
    }

    // To call from event script
    public void AttackCoolDown()
    {
        StopCoroutine(WaitForNextAttack());
        StartCoroutine(WaitForNextAttack());
    }

    IEnumerator WaitForNextAttack()
    {
        isReadyToAttack = false;

        yield return new WaitForSeconds(settings.AttackDelay);

        isReadyToAttack = true;
    }

    //private void KillEnemy(GameObject closestEnemy)
    //{
    //    // Zombie zombie;
        
    //    if (closestEnemy)
    //    {
    //        Zombie zombie = closestEnemy.GetComponent<Zombie>();

    //        // See if this is a zombie
    //        if (zombie)
    //        {
    //            animator.SetTrigger(owner.AnimationIDs.attackID);
    //            zombie.Die(); // TODO Add delayed animation trigger event
                
    //        }
    //        // Else its a necromancer
    //        else
    //        {
    //            PlayerMovement necroMancer = closestEnemy.GetComponent<PlayerMovement>();

    //            if (necroMancer)
    //            {
    //                animator.SetTrigger(owner.AnimationIDs.attackID); // TODO Add delayed animation trigger event
    //                owner.GameManager.DecreaseHealth();
    //            }
    //        }

    //        StopCoroutine(WaitForNextAttack());
    //        StartCoroutine(WaitForNextAttack());

    //    }
    //}
}
