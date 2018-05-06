using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[CreateAssetMenu (menuName = "PluggableAI/Actions/Chase")]
public class ChaseAction : Action
{
    public override void Act(StateController controller)
    {
        if (!controller.justAttacked)
        {
            Chase(controller);
            Attack(controller);
        }
        else
        {
            Attack(controller);
        }
    }

    private void Chase(StateController controller)
    {
        controller.agent.SetDestination(controller.target.position);
    }

    private void Attack(StateController controller)
    {
        if (controller.agent.remainingDistance <= controller.enemyStats.attackTheshold && !controller.agent.pathPending && !controller.justAttacked)
        {
            Debug.Log("Attack");
            controller.target.gameObject.GetComponent<PlayerStats>().AttackPlayer();
            controller.anim.SetTrigger("Attack");
            controller.audio.clip = controller.attackClip;
            controller.audio.Play();
            controller.justAttacked = true;
        }
    }
}
