using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName = "PluggableAI/Decisions/Idle")]
public class IdleDecision : Decision {
	public override bool Decide(StateController controller)
	{
		return ShouldBeIdle(controller);
	}

	private bool ShouldBeIdle(StateController controller)
	{
		if (!controller.agent.pathPending && controller.agent.remainingDistance <= 0.1f)
		{
			controller.currentTimer = 0;
			controller.agent.isStopped = true;
			controller.anim.SetBool("IsIdle", true);
			controller.anim.SetBool("IsPatrolling", false);
			return true;
		}

		return false;
	}
}
