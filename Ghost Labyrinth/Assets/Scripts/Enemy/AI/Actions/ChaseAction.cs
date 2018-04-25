using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[CreateAssetMenu (menuName = "PluggableAI/Actions/Chase")]
public class ChaseAction : Action
{
    public override void Act(StateController controller)
    {
        Chase(controller);
        Attack(controller);
    }

    private void Chase(StateController controller)
    {
        controller.agent.SetDestination(controller.target.position);
       // controller.agent.isStopped = false;
    }

    private void Attack(StateController controller)
    {
        if (controller.agent.remainingDistance <= controller.enemyStats.attackTheshold && !controller.agent.pathPending)
        {
            Debug.Log("Player Attacked!");
           // controller.anim.SetTrigger("Attack");
        }
    }
}
