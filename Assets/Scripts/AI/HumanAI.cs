using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanAI : AI
{
    private Human agent;

    private void Awake()
    {
        agent = GetComponent<Human>();

        DecisionTransaction hungry = new DecisionTransaction(this, () => agent.Food < 20, () => agent.Food > 20);

        BTNode eatAction = new BTEat(agent);
        IPlan eatPlan = new BTRoot(eatAction, this);

        Decision eatDecision = new Decision(eatPlan, () => 1 - (agent.Food / 100)); //Linear utility, depending on how much food left

        Decision doNothing = new Decision(null, () => 0.5f);

        State idleState = new State();
        idleState.AddTransaction(hungry);

        idleState.AddDecision(eatDecision);
        idleState.AddDecision(doNothing);

        StateMachine stateMachine = new StateMachine(this);

        sensor = stateMachine;

        stateMachine.SetState(idleState);
    }
}
