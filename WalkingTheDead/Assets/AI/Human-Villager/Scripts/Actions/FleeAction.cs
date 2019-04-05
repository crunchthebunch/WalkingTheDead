using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "HumanAI/Actions/Flee")]
public class FleeAction : Action
{
    public override void Act(StateController controller)
    {
        Flee(controller);
    }

    private void Flee(StateController controller)
    {
        controller.Owner.FleeBehaviour.Flee();
    }
}
