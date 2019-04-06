using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "HumanAI/Decision/Flee")]
public class FleeDecision : Decision
{
    public override bool MakeDecision(StateController controller)
    {
        return AreZombiesAround(controller);
    }

    private bool AreZombiesAround(StateController controller)
    {
        // Check whether any zombies are in range
        if (controller.Owner.ZombieScanner.ObjectsInRange.Count > 0)
        {
            // controller.Test();
            return true;
        }
        // controller.TestOpposite();
        return false;
    }
}
