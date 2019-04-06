using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "HumanAI/Actions/MoveBack")]
public class MoveBackAction : Action
{
    public override void Act(StateController controller)
    {
        MoveBackToNavigationCenter(controller);
    }

    private void MoveBackToNavigationCenter(StateController controller)
    {
        controller.Owner.MoveBackBehaviour.DoBehaviour();
    }
}
