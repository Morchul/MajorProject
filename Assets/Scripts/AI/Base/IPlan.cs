public interface IPlan
{
    public PlanState Update();
}

public enum PlanState
{
    RUNNING = 1, //It is running, tendency to keep continue
    SUCCESSFUL = 0, //Task completed successfully
    FAILURE = -1 //Task failed tend to do something else
}
