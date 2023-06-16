public interface IPlan
{
    public void Update();
    //public PlanState GetLastRunState();
}

public enum PlanState
{
    NONE,
    SUCCESSFUL,
    FAILURE
}
