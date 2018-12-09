using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "FiniteStateAI/Decision/WaitAndSeek")]
public class WaitDecision : Decision {

	public override bool Decide(StateController controller)
    {
        return Wait(controller);
    }


    bool Wait(StateController controller)
    {
        if (controller.CheckIfCountDownElapsed(controller.enemyStats.WaitAndSeekTime))
        {
            
            return true;
        }
        else
        {
        return false;
        }
    }
}
