using System.Collections.Generic;
using UnityEngine;

public class StateMachine : ISensor
{
    private State currentState;

    private readonly AI ai;

    private float deltaTimeSinceLastDecision;

    public StateMachine(AI ai)
    {
        this.ai = ai;
    }

    public void SetState(State state)
    {
        if(currentState != null)
            currentState.ResetDecisions();
        deltaTimeSinceLastDecision = 0;
        currentState = state;
        
        ai.MakeNewDecision();
    }

    public IDecision MakeDecision()
    {
        //Selects the decision with the highest utility. Absolute Utility approach
        List<IDecision> decisions = currentState.GetDecisions();

        int maxIndex = 0;
        float highestUtiltiy = 0;

        for (int i = 0; i < decisions.Count; ++i)
        {
            float decisionUtility = decisions[i].CalculateUtility(deltaTimeSinceLastDecision);
            if (decisionUtility > highestUtiltiy)
            {
                highestUtiltiy = decisionUtility;
                maxIndex = i;
            }
        }

        deltaTimeSinceLastDecision = 0;
        return decisions[maxIndex];
    }

    public void Update()
    {
        deltaTimeSinceLastDecision += Time.deltaTime;
        currentState.Update();
    }
}
