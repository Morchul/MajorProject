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
    private readonly System.Func<bool> ResetMethod;

    private bool triggered;

    public DecisionTransaction(AI ai, System.Func<bool> check, System.Func<bool> reset) : base(ai)
    {
        this.CheckMethod = check;
        this.ResetMethod = reset;
        triggered = false;
    }

    public override bool Check() => CheckMethod();

    public override void Update()
    {
        if(triggered)
        {
            if (ResetMethod())
                triggered = false;
        }
        else
        {
            if (Check())
            {
                triggered = true;
                ai.MakeNewDecision();
            }
        } 
    }
}
