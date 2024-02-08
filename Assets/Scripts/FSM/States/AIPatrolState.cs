using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIPatrolState : AIState
{
    public AIPatrolState(AiStateAgent agent) : base(agent)
    {

        AIStateTransition transition = new AIStateTransition(nameof(AIIdleState));
        transition.AddCondition(new FloatCondition(agent.destinationDistance, Condition.Predicate.LESS, 1));
        transitions.Add(transition);

        transition = new AIStateTransition(nameof(AIChaseState));

        transition.AddCondition(new BoolCondition(agent.enemySeen));

        transitions.Add(transition);
    }

    public override void OnEnter()
    {
        var navNode = AINavNode.GetRandomAINavNode();
        agent.movement.Destination = navNode.transform.position;
        Debug.Log(agent.movement.Destination);
    }

    public override void OnUpdate()
    {
        agent.movement.MoveTowards(agent.movement.Destination);
    }

    public override void OnExit()
    {
        agent.movement.Resume();

    }

}
