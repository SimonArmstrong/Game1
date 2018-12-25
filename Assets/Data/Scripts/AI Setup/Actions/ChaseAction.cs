using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "FiniteStateAI/Action/Chase")]
public class ChaseAction : Action {
    
    public override void Act(StateController controller)
    {
        Chase(controller);
    }


    void Chase(StateController controller)
    {
        controller.unit.RequestPath();
    }
}
