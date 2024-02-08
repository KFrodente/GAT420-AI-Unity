using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIAttackState : AIState
{
    public AIAttackState(AiStateAgent agent) : base(agent)
    {
        AIStateTransition transition = new AIStateTransition(nameof(AIChaseState));
        transition.AddCondition(new FloatCondition(agent.timer, Condition.Predicate.LESS, 0));
        transitions.Add(transition);
    }

    public override void OnEnter()
    {
        agent.movement.Stop();
        agent.animator?.SetTrigger("Attack");
        agent.movement.Resume();

        agent.timer.value = Time.time + 2;
    }

    public override void OnUpdate()
    {
    }

    public override void OnExit()
    {
        agent.movement.Resume();

    }

}
