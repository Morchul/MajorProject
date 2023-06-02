using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestAI : AI
{
    private Human agent;

    private void Awake()
    {
        agent = GetComponent<Human>();

        DecisionTransaction hungry = new DecisionTransaction(this, () => agent.Food < 20); //Will trigger every frame if food under 20 and not eat. Maybe if triggered wait until 15

        IPlan eatPlan = new BTRoot();

        Decision eatDecision = new Decision(eatPlan, () => 1 - (agent.Food / 100)); //Linear utility, depending on how much food left

        State idleState = new State();
        idleState.AddTransaction(hungry);

        idleState.AddDecision(eatDecision);

        StateMachine stateMachine = new StateMachine(this);

        stateMachine.SetState(idleState);

        sensor = stateMachine;
    }
}
