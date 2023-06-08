public abstract class AbstractBTNode
{
    public abstract BTStatus Tick();

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

    public override BTStatus Tick() => TickMethod();
}