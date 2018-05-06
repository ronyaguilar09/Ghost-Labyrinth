using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[CreateAssetMenu (menuName = "PluggableAI/Decisions/Idle")]
public class IdleDecision : Decision {
	public override bool Decide(StateController controller)
	{
		return ShouldBeIdle(controller);
	}

	private bool ShouldBeIdle(StateController controller)
	{
		if (controller.justAttacked && !(controller.anim.GetCurrentAnimatorStateInfo(0).normalizedTime <
		                                 controller.anim.GetCurrentAnimatorStateInfo(0).length))
		{
			controller.currentTimer = 0;
			controller.justAttacked = false;
			controller.anim.SetTrigger("DoneAttacking");
			//controller.anim.SetBool("IsIdle", true);
			//controller.anim.SetBool("IsPatrolling", false);
			return true;
		}
		else if (!controller.agent.pathPending && (controller.agent.remainingDistance <= 0.1f) && !controller.justAttacked){
			Debug.Log("Just Attacked is False");
			controller.currentTimer = 0;
			controller.agent.isStopped = true;
			controller.anim.SetTrigger("DonePatrolling");
			//controller.anim.SetBool("IsIdle", true);
			//controller.anim.SetBool("IsPatrolling", false);
			return true;
		} else if (controller.agent.pathStatus == NavMeshPathStatus.PathInvalid)
		{
			controller.currentTimer = 0;
			controller.justAttacked = false;
			controller.anim.SetTrigger("DonePatrolling");
		}

		return false;
	}
}
