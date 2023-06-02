using System.Collections.Generic;

public class State
{
    private readonly List<ITransaction> transactions;
    private readonly List<IDecision> decisions;

    public State()
    {
        transactions = new List<ITransaction>();
        decisions = new List<IDecision>();
    }

    //public void Stop();
    //public void Start();

    public void Update()
    {
        foreach (ITransaction transaction in transactions)
            transaction.Update();
    }

    public void AddDecision(IDecision decision) => decisions.Add(decision);
    public List<IDecision> GetDecisions() => decisions;

    public void AddTransaction(ITransaction transaction) => transactions.Add(transaction);
}
