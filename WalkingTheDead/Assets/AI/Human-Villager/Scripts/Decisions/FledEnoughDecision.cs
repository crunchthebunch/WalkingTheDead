using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "HumanAI/Decision/FledEnough")]
public class FledEnoughDecision : Decision
{
    public override bool MakeDecision(StateController controller)
    {
        return HasFledEnough(controller);
    }

    private bool HasFledEnough(StateController controller)
    {
        // Check whether we travelled enough
        if (Vector3.Distance(controller.Owner.transform.position, controller.Owner.ZombieScanner.LastKnownObjectLocation) 
            > controller.Stats.FleeDistance - 2.0f)
        {
            return true;
        }
        return false;
    }
}
