using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractTriggerTransaction : ITriggerTransaction
{
    private State otherState;
    private StateMachine stateMachine;

    public int Type { get; private set; }

    public AbstractTriggerTransaction(int type, State otherState, StateMachine stateMachine)
    {
        Type = type;
        this.otherState = otherState;
        this.stateMachine = stateMachine;
    }

    protected void ChangeState()
    {
        stateMachine.SetState(otherState);
    }

    public abstract void Trigger();
}

public class TriggerTransaction : AbstractTriggerTransaction
{
    private readonly System.Func<bool> Check;

    public TriggerTransaction(int type, State otherState, StateMachine stateMachine, System.Func<bool> check) : base(type, otherState, stateMachine)
    {
        Check = check;
    }

    public override void Trigger()
    {
        Debug.Log("TriggerTransaction Trigger()");
        if (Check())
            ChangeState();
    }
}
