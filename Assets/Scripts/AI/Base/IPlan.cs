public interface IPlan
{
    public void Update();
    public PlanState CurrentState { get; }
}

public enum PlanState
{
    RUNNING = 0,
    SUCCESSFUL = 1,
    FAILURE = 2
}
