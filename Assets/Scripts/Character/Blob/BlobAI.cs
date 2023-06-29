using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlobAI : CharacterBaseAI
{
    private Blob agent;

    protected override void Start()
    {
        agent = GetComponent<Blob>();

        BTMoveTo moveTo = new BTMoveTo(agent, this);

        BTFindTarget findFood = new BTFindTarget(this, "Food", agent.transform, 10);

        //BTSequence sequence;
        IPlan eatFoodPlan = new BTRoot(null, this);
        Decision eatFood = new Decision(eatFoodPlan, (_) => 1f);

        State idleState = new State();

        StateMachine stateMachine = new StateMachine(this);

        sensor = stateMachine;

        stateMachine.SetState(idleState);
    }
}
