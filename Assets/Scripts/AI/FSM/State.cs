using System.Collections.Generic;
using UnityEngine;

public class State
{
    private readonly List<IContinuesTransaction> transactions;
    private readonly List<ITriggerTransaction> triggers;
    private readonly List<IDecision> decisions;

    public readonly string Name;

    public State(string name)
    {
        Name = name;
        transactions = new List<IContinuesTransaction>();
        triggers = new List<ITriggerTransaction>();
        decisions = new List<IDecision>();
    }

    //public void Stop();
    //public void Start();

    public void Update()
    {
        foreach (IContinuesTransaction transaction in transactions)
            transaction.Update();
    }

    public void ResetDecisions()
    {
        foreach (Decision decision in decisions) decision.Reset();
    }

    public void Trigger(int type)
    {
        Debug.Log("Go through triggers");
        foreach(ITriggerTransaction trigger in triggers)
        {
            if(type == trigger.Type)
                trigger.Trigger();
        }
    }

    public void AddDecision(IDecision decision) => decisions.Add(decision);
    public List<IDecision> GetDecisions() => decisions;

    public void AddTransaction(IContinuesTransaction transaction) => transactions.Add(transaction);
    public void AddTransaction(ITriggerTransaction transaction) => triggers.Add(transaction);
}
