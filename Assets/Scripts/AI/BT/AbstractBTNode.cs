public abstract class AbstractBTNode
{
    public abstract BTStatus Tick();
    public virtual void CleanUp() { }

    public string Name { get; protected set; }

    public enum BTStatus
    {
        //Values adapted from PlanState
        FAILURE = 2,
        SUCCESS = 1,
        RUNNING = 0
    }
}

public class BTNode : AbstractBTNode
{
    private readonly System.Func<BTStatus> TickMethod;

    public BTNode(string name, System.Func<BTStatus> tickMethod)
    {
        TickMethod = tickMethod;
        Name = name;
    }

    public override void CleanUp() { }

    public override BTStatus Tick() => TickMethod();
}