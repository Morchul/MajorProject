using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanAI : AI
{
    private Human agent;

    public Food TargetFood;

    public Vector3 MoveTarget;

    private void Awake()
    {
        agent = GetComponent<Human>();

        DecisionTransaction hungry = new DecisionTransaction(this, () => agent.Food < 40, () => agent.Food > 40);

        BTMoveTo moveTo = new BTMoveTo(agent, this);
        moveTo.MaxDistanceSqrt = 4;

        AbstractBTNode eatAction = new BTEat(agent, this);
        BTLocateFood locateFood = new BTLocateFood(this);
        BTSequence eatSequence = new BTSequence(locateFood, moveTo, eatAction);
        IPlan eatPlan = new BTRoot(eatSequence, this);

        Decision eatDecision = new Decision(eatPlan, () => 1 - (agent.Food / 100)); //Linear utility, depending on how much food left

        AbstractBTNode pickRandomCloseSpot = new BTPickRandomSpot(this);
        AbstractBTNode removeMoveTo = new BTNode(() => { MoveTarget = Vector3.zero; return AbstractBTNode.BTStatus.SUCCESS; });

        BTSequence moveAroundSequence = new BTSequence(pickRandomCloseSpot, moveTo, removeMoveTo);

        IPlan moveAroundPlan = new BTRoot(moveAroundSequence, this);

        Decision moveAround = new Decision(moveAroundPlan, () => 0.5f);

        State idleState = new State();
        idleState.AddTransaction(hungry);

        idleState.AddDecision(eatDecision);
        idleState.AddDecision(moveAround);

        StateMachine stateMachine = new StateMachine(this);

        sensor = stateMachine;

        stateMachine.SetState(idleState);
    }
}
