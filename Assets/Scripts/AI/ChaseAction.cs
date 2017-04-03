using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "AI/Actions/ChaseAction")]
public class ChaceAction : Action
{

    public override void Act(StateController controller)
    {

    }
    private void Chase (StateController controller)
    {
        controller.navMeshAgent.destination = controller.chaseTarget.position;
        controller.navMeshAgent.Resume();


    }


}
