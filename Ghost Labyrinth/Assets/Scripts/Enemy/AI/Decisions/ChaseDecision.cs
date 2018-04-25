using System.Collections;
using System.Collections.Generic;
using System.Timers;
using UnityEngine;

[CreateAssetMenu (menuName = "PluggableAI/Decisions/Chase")]
public class ChaseDecision : Decision
{
    public override bool Decide(StateController controller)
    {

        if (isWithinFOV(controller))
        {
            RaycastHit hit;
            Vector3 rayDirection = controller.target.position - controller.transform.position;
            
            if (Physics.Raycast(controller.transform.position, rayDirection, out hit, controller.enemyStats.visionDistanceThreshold))
            {
                if (hit.collider.CompareTag("Player"))
                {
                    Debug.Log("Player Found.");
                    return true;
                }
            }
        }
        
        return false;
    }

    private bool isWithinFOV(StateController controller)
    {
        if (Vector3.Angle(controller.eyes.forward, controller.target.position - controller.transform.position) <= controller.enemyStats.visionAngleThreshold)
        {
            Vector3 direction = controller.target.position - controller.transform.position;
           // Debug.DrawLine(controller.transform.position, controller.target.position, Color.red);
            Debug.DrawRay(controller.transform.position, direction, Color.red);
            return true;
        }

        return false;
    }
}
