public abstract class AbstractBTNode
{
    public abstract BTStatus Tick();
    public abstract void CleanUp();

    public enum BTStatus
    {
        FAILURE,
        SUCCESS,
        RUNNING
    }
}

public class BTNode : AbstractBTNode
{
    private readonly System.Func<BTStatus> TickMethod;

    public BTNode(System.Func<BTStatus> tickMethod)
    {
        TickMethod = tickMethod;
    }

    public override void CleanUp() { }

    public override BTStatus Tick() => TickMethod();
}