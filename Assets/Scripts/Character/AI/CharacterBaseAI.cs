using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CharacterBaseAI : AI
{
    [Header("Blackboard")]
    public SmartObject TargetObject;
    public Entity TargetEntity;

    //public Vector3 MoveTarget;

    protected Agent agent;

    protected BTExecuteAction putInAction;
    protected BTExecuteAction buildAction;
    protected BTExecuteAction pickUpAction;
    protected BTExecuteAction eatAction;
    protected BTExecuteAction takeOutAction;

    protected BTExecuteAgentAction dropAction;

    protected BTMoveTo moveTo;
    protected BTSequence moveToTargetObject;
    protected BTSequence moveToTargetEntity;

    protected StateMachine stateMachine;

    public Vector3 MoveTarget
    {
        get => moveTo.MoveTarget;
        set => moveTo.SetTarget(value);
    }

    protected override void Start()
    {
        stateMachine = new StateMachine(this);
        Sensor = stateMachine;

        agent = GetComponent<Agent>();

        moveTo = new BTMoveTo(agent);
        moveTo.MaxDistanceSqrt = 2;


        BTNode setTargetObjectAsMoveTarget = new BTNode("set target obj as move target", () =>
        {
            Debug.Log("TARGET OBJECT IS: " + TargetObject.name);
            moveTo.SetTarget(TargetObject.transform.position);
            return AbstractBTNode.BTStatus.SUCCESS;
        });

        BTNode setTargetEntityAsMoveTarget = new BTNode("set target obj as move target", () =>
        {
            moveTo.SetTarget(TargetEntity.transform.position);
            return AbstractBTNode.BTStatus.SUCCESS;
        });

        moveToTargetObject = new BTSequence("move to target obj", setTargetObjectAsMoveTarget, moveTo);
        moveToTargetEntity = new BTSequence("move to target obj", setTargetEntityAsMoveTarget, moveTo);

        putInAction = new BTExecuteAction(this, agent, ActionID.PUT_IN);
        buildAction = new BTExecuteAction(this, agent, ActionID.BUILD);
        pickUpAction = new BTExecuteAction(this, agent, ActionID.PICK_UP);
        eatAction = new BTExecuteAction(this, agent, ActionID.EAT);
        takeOutAction = new BTExecuteAction(this, agent, ActionID.TAKE_OUT);

        dropAction = new BTExecuteAgentAction(agent, ActionID.DROP);
    }
}
