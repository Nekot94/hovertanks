using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName = "AI/Actions/PatrolAction")]
public class PatrolAction : Action {

	public override void Act(StateController controller)
    {
        Patrol(controller);
    }

    private void Patrol(StateController controller)
    {
        controller.navMeshAgent.destination = controller.wayPoints[controller.nextWayPoint].position;
        controller.navMeshAgent.Resume();

        if (controller.navMeshAgent.remainingDistance <= controller.navMeshAgent.stoppingDistance &&
            !controller.navMeshAgent.pathPending)
        {
            controller.nextWayPoint = (controller.nextWayPoint + 1) % controller.wayPoints.Count;
        }
    }

}
