public interface IPlan
{
    public void Update();
    public PlanState GetLastRunState();
}

public enum PlanState
{
    NONE = 0,
    SUCCESSFUL = 1,
    FAILURE = -1
}
