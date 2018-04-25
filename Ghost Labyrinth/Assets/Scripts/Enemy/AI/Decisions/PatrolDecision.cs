using System.Collections;
using System.Collections.Generic;
using System.Timers;
using UnityEngine;

[CreateAssetMenu (menuName = "PluggableAI/Decisions/Patrol")]
public class PatrolDecision : Decision {
    public override bool Decide(StateController controller)
    {
        return ShouldPatrol(controller);
    }

    private bool ShouldPatrol(StateController controller)
    {
        if (controller.currentTimer >= controller.waitTime)
        {
            controller.anim.SetBool("IsIdle", false);
            controller.anim.SetBool("IsPatrolling", true);
            return true;
        }

        return false;
    }
}
