public abstract class AbstractStateTransaction : ITransaction
{
    protected readonly State otherState;
    protected readonly StateMachine stateMachine;

    public AbstractStateTransaction(StateMachine stateMachine, State otherState)
    {
        this.otherState = otherState;
        this.stateMachine = stateMachine;
    }

    public virtual void Update()
    {
        if (Check())
            stateMachine.SetState(otherState);
    }

    protected abstract bool Check();
}

public class StateTransaction : AbstractStateTransaction
{
    private readonly System.Func<bool> CheckMethod;

    public StateTransaction(StateMachine stateMachine, State otherState, System.Func<bool> check) : base(stateMachine, otherState)
    {
        this.CheckMethod = check;
    }

    protected override bool Check() => CheckMethod();
}
