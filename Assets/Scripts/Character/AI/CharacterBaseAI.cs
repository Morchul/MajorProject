using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CharacterBaseAI : AI
{
    [Header("Blackboard")]
    public SmartObject TargetObject;
    public Entity TargetEntity;

    public Vector3 MoveTarget;

    protected ActionEntity agent;

    protected BTExecuteAction putInAction;
    protected BTExecuteAction buildAction;
    protected BTExecuteAction pickUpAction;
    protected BTExecuteAction eatAction;
    protected BTExecuteAction takeOutAction;

    protected BTExecuteAgentAction dropAction;

    protected StateMachine stateMachine;

    protected override void Start()
    {
        stateMachine = new StateMachine(this);
        Sensor = stateMachine;

        agent = GetComponent<ActionEntity>();

        putInAction = new BTExecuteAction(this, agent, ActionID.PUT_IN);
        buildAction = new BTExecuteAction(this, agent, ActionID.BUILD);
        pickUpAction = new BTExecuteAction(this, agent, ActionID.PICK_UP);
        eatAction = new BTExecuteAction(this, agent, ActionID.EAT);
        takeOutAction = new BTExecuteAction(this, agent, ActionID.TAKE_OUT);

        dropAction = new BTExecuteAgentAction(agent, ActionID.DROP);
    }
}
