using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[CreateAssetMenu (menuName = "PluggableAI/Actions/Patrol")]
public class PatrolAction : Action
{
    public override void Act(StateController controller)
    {
        if (!controller.agent.hasPath && !controller.agent.pathPending )
        {
            controller.agent.SetDestination(getRandomDestination(controller));
            controller.agent.isStopped = false;
        }
    }

    private Vector3 getRandomDestination(StateController controller)
    {
        return controller.spaces[Random.Range(0, controller.spaces.Count)];
    }
}
