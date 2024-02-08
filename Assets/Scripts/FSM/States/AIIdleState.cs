using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class AIIdleState : AIState
{
    private ValueRef<bool> dance = new ValueRef<bool>();
    public AIIdleState(AiStateAgent agent) : base(agent)
    {
        agent.timer.value = Random.Range(1f, 2f);

        AIStateTransition transition = new AIStateTransition(nameof(AIPatrolState));
        transition.AddCondition(new FloatCondition(agent.timer, Condition.Predicate.LESS, 0));
        transitions.Add(transition);

        transition = new AIStateTransition(nameof(AIGroupDance));
        transition.AddCondition(new IntCondition(agent.friendliesSeen, Condition.Predicate.GREATER, 3));
        transitions.Add(transition);

        transition = new AIStateTransition(nameof(AISoloDance));
        transition.AddCondition(new BoolCondition(dance));
        transitions.Add(transition);

        transition = new AIStateTransition(nameof(AIChaseState));
        transition.AddCondition(new BoolCondition(agent.enemySeen));
        transitions.Add(transition);
    }

    public override void OnEnter()
    {
        int rand = Random.Range(0, 10);
        if (rand >= 8) dance.value = true;

        agent.timer.value = Random.Range(1f, 2f);

        agent.movement.Stop();
        agent.movement.Velocity = Vector3.zero;

        Debug.Log(agent.timer.value);
    }

    public override void OnUpdate()
    {
        //if (transition.ToTransition()) agent.stateMachine.SetState(transition.nextState);

        agent.animator.SetInteger("FriendsNearby", agent.friendliesSeen);
    }

    public override void OnExit()
    {
        dance.value = false;
        agent.movement.Resume();
    }
}
