public abstract class AbstractDecisionTransaction : ITransaction
{
    protected readonly AI ai;

    public AbstractDecisionTransaction(AI ai)
    {
        this.ai = ai;
    }

    public virtual void Update()
    {
        if (Check())
            ai.MakeNewDecision();
    }

    public abstract bool Check();
}

public class DecisionTransaction : AbstractDecisionTransaction
{
    private readonly System.Func<bool> CheckMethod;

    public DecisionTransaction(AI ai, System.Func<bool> check) : base(ai)
    {
        this.CheckMethod = check;
    }

    public override bool Check() => CheckMethod();
}
