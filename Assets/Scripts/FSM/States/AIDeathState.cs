using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIDeathState : AIState
{

    public AIDeathState(AiStateAgent agent) : base(agent)
    {
    }

    public override void OnEnter()
    {
        agent.movement.Stop();
        agent.movement.Velocity = Vector3.zero;
        agent.animator?.SetTrigger("Death");
        agent.timer.value += 5;
    }
    public override void OnUpdate()
    {
        if (agent.timer.value <= 0)
        {
            GameObject.Destroy(agent.gameObject);
        }
    }

    public override void OnExit()
    {
        throw new System.NotImplementedException();
    }

}
