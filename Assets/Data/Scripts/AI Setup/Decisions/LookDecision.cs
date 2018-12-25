using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "FiniteStateAI/Decision/Look")]
public class LookDecision : Decision {

	public override bool Decide(StateController controller)
    {
        return Look(controller);
    }


    bool Look(StateController controller)
    {
        
        List<Transform> visibleTargets = controller.fov.FindVisibleTargets();
        if (visibleTargets.Count > 0)
        {
            controller.unit.target = GameManager.instance.player.transform;
            return true;
        }
        else return false;
    }
}
