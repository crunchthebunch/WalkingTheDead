﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "HumanAI/Decision/Flee")]
public class ZombiesAroundDecision : Decision
{
    public override bool MakeDecision(StateController controller)
    {
        return AreZombiesAround(controller);
    }

    private bool AreZombiesAround(StateController controller)
    {
        // Access Villager State Controller functions
        VillagerStateController villagerController = controller as VillagerStateController;

        // Check whether any zombies are in range
        if (villagerController.Owner.ZombieScanner.ObjectsInRange.Count > 0)
        {
            // controller.Test();
            return true;
        }
        // controller.TestOpposite();
        return false;
    }
}
