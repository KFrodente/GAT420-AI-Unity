using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AISoloDance : AIState
{
    public AISoloDance(AiStateAgent agent) : base(agent)
    {

        AIStateTransition transition = new AIStateTransition(nameof(AIIdleState));
        transition.AddCondition(new FloatCondition(agent.timer, Condition.Predicate.LESS, 3));
        transitions.Add(transition);
    }

    public override void OnEnter()
    {
        agent.timer.value = Random.Range(1f, 2f);
        agent.animator.SetTrigger("Dance");
    }

    public override void OnExit()
    {
        agent.animator.SetTrigger("BackToIdle");
    }

    public override void OnUpdate()
    {
    }
}
