using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieAnimationScript : MonoBehaviour
{
    AttackZombieBehaviour attackBehaviour;
    Scanner enemyScanner;
    GameObject closestEnemy = null;
    PlayerResources gameManager;

    private void Awake()
    {
        enemyScanner = GetComponent<Scanner>();
        gameManager = FindObjectOfType<PlayerResources>();
    }

    private void Start()
    {
        attackBehaviour = GetComponentInParent<AttackZombieBehaviour>();
    }

    public void SetClosestEnemy(GameObject closestEnemy)
    {
        this.closestEnemy = closestEnemy;
    }

    public void KillEnemy()
    {

        if (closestEnemy)
        {
            MeleeSoldier zombie = closestEnemy.GetComponent<MeleeSoldier>();

            // See if this is a zombie
            if (zombie)
            {
                zombie.Die(); // TODO Add delayed animation trigger event
            }
            // Else its a necromancer
            else
            {
                PlayerMovement necroMancer = closestEnemy.GetComponent<PlayerMovement>();

                if (necroMancer)
                {
                    gameManager.DecreaseHealth();
                }
            }
        }

        // Have attack cooldown - if its alive
        if (attackBehaviour)
            attackBehaviour.AttackCoolDown();
    }

}
