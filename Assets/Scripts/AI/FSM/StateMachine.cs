using System.Collections.Generic;

public class StateMachine : ISensor
{
    private State currentState;

    private readonly AI ai;

    public StateMachine(AI ai)
    {
        this.ai = ai;
    }

    public void SetState(State state)
    {
        currentState = state;
        ai.MakeNewDecision();
    }

    public IPlan MakeDecision()
    {
        //Selects the decision with the highest utility. Absolute Utility approach
        List<IDecision> decisions = currentState.GetDecisions();

        int maxIndex = 0;
        float highestUtiltiy = 0;

        for (int i = 0; i < decisions.Count; ++i)
        {
            float decisionUtility = decisions[i].CalculateUtility();
            if (decisionUtility > highestUtiltiy)
            {
                highestUtiltiy = decisionUtility;
                maxIndex = i;
            }
        }

        return decisions[maxIndex].GetPlan();
    }

    public void Update()
    {
        currentState.Update();
    }
}
