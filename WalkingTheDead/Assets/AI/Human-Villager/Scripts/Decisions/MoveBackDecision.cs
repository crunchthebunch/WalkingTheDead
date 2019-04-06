using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "HumanAI/Decision/MoveBack")]
public class MoveBackDecision : Decision
{
    public override bool MakeDecision(StateController controller)
    {
        return HasReachedDestination(controller);
    }

    private bool HasReachedDestination(StateController controller)
    {
        // Check whether the object has arrived to it's original location
        if (Vector3.Distance(controller.Owner.transform.position, controller.Owner.MoveBackBehaviour.NavigationCenter)
            < 1.0f)
        {
            // controller.Test();
            return true;
        }
        // controller.TestOpposite();
        return false;
    }
}
