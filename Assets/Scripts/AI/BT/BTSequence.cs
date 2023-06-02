public class BTSequence : BTComposite
{
    public BTSequence(params BTNode[] children) : base(children)
    {}

    public override BTStatus Tick()
    {
        BTStatus retState = BTStatus.SUCCESS;
        foreach(BTNode node in children)
        {
            BTStatus state = node.Tick();
            if (state == BTStatus.FAILURE) return state;
            if (state == BTStatus.RUNNING) retState = BTStatus.RUNNING;
        }
        return retState;
    }
}
