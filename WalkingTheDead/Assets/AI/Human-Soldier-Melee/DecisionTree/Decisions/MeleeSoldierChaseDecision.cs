using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "AI/Human/MeleeSoldier/Decision/Chase")]
public class MeleeSoldierChaseDecision : Decision
{
    public override bool MakeDecision(StateController controller)
    {
        return AreZombiesInRange(controller);
    }

    private bool AreZombiesInRange(StateController controller)
    {
        MeleeSoldierStateController soldierController = controller as MeleeSoldierStateController;

        if (soldierController.Owner.ZombieScanner.ObjectsInRange.Count > 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
