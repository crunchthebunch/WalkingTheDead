using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "AI/Human/MeleeSoldier/Decision/MoveBack")]
public class MeleeSoldierPatrolDecision : Decision
{
    public override bool MakeDecision(StateController controller)
    {
        return IsZombieTooFarAway(controller);
    }

    private bool IsZombieTooFarAway(StateController controller)
    {
        MeleeSoldierStateController soldierController = controller as MeleeSoldierStateController;

        // If there are any soldiers around, check if their position is too far away
        // If there are no soldiers around at the last location, go back

        // Last seen Zombie - TODO see if the amount of zombies have to be checked
        if (soldierController.Owner.ZombieScanner.ObjectsInRange.Count > 0)
        {
            Vector3 closestZombie = soldierController.Owner.ZombieScanner.GetClosestTargetInRange().transform.position;

            // Distance for how long the  soldier can chase for
            float chaseDistance = soldierController.Owner.Settings.ChaseDistance;

            // Check whether it's Distance is bigger than the distance between all of it's patrol goals
            foreach (Vector3 patrolPoint in soldierController.Owner.PatrolBehaviour.PatrolPositions)
            {
                // If one of the areas is close enough to his patrol Point
                if (Vector3.Distance(patrolPoint, closestZombie) < chaseDistance)
                {
                    // Keep chasing
                    return false;
                }
            }
        }

        // If all of them are out of range, keep chasing
        return true;
    }
}
