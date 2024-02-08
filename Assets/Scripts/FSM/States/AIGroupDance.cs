using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIGroupDance : AIState
{
    public AIGroupDance(AiStateAgent agent) : base(agent)
    {

        AIStateTransition transition = new AIStateTransition(nameof(AIIdleState));
        transition.AddCondition(new IntCondition(agent.friendliesSeen, Condition.Predicate.LESS, 3));
        transitions.Add(transition);
    }

    public override void OnEnter()
    {

    }

    public override void OnExit()
    {
    }

    public override void OnUpdate()
    {
        agent.animator.SetInteger("FriendsNearby", agent.friendliesSeen);

    }
}
