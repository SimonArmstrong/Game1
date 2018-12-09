using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "FiniteStateAI/Action/StandStill")]
public class StandStillAction : Action {

    public override void Act(StateController controller)
    {
        //throw new System.NotImplementedException();

        StandStill(controller);
    }

    void StandStill(StateController controller)
    {
        controller.unit.StopMovement();
        controller.moveVec = Vector2.zero;
    }
}
