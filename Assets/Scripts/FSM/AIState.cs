using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AIState
{
    protected AiStateAgent agent;

    public AIState(AiStateAgent agent)
    {
        this.agent = agent;
    }

    public List<AIStateTransition> transitions { get; protected set; } = new();

    public string name { get { return GetType().Name; } }

    public abstract void OnEnter();
    public abstract void OnUpdate();
    public abstract void OnExit();
}
