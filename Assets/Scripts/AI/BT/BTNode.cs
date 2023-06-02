public abstract class BTNode
{
    public abstract BTStatus Tick();

    public enum BTStatus
    {
        FAILURE,
        SUCCESS,
        RUNNING
    }
}
