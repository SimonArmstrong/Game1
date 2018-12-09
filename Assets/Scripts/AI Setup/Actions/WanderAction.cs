using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "FiniteStateAI/Action/Wander")]
public class WanderAction : Action {

    public override void Act(StateController controller)
    {
        //throw new System.NotImplementedException();

        Wander(controller);
    }

    void Wander(StateController controller)
    {

        if(Vector2.Distance(controller.transform.position, controller.wayPointList[controller.nextWayPoint].transform.position) <= 0.3)controller.nextWayPoint = (controller.nextWayPoint + 1) % controller.wayPointList.Count;
        controller.unit.target = controller.wayPointList[controller.nextWayPoint].transform;
        controller.unit.RequestPath();

    }
}
