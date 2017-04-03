using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName = "AI/State")]
public class State : ScriptableObject
{
    public Action[] actions;
    public Color sceneGizmoColor = Color.grey;
    // public Transition[] transitions;

    public void UpdateState(StateController controller)
    {
        DoActions(controller);
        // CheckTransitions(controller);
    }

    public void DoActions(StateController controller)
    {
        for (int i = 0; i < actions.Length; i++)
        {
            actions[i].Act(controller);
        }
    }
}
