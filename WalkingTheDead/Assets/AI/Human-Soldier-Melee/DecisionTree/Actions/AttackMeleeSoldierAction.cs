using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackMeleeSoldierAction : Action
{
    public override void Act(StateController controller)
    {
        Attack(controller);
    }

    private void Attack(StateController controller)
    {
        MeleeSoldierStateController soldierController = controller as MeleeSoldierStateController;
    }
}
