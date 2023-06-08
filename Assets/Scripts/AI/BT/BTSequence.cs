public class BTSequence : BTComposite
{
    public BTSequence(params AbstractBTNode[] children) : base(children)
    {}

    public override BTStatus Tick()
    {
        foreach(AbstractBTNode node in children)
        {
            BTStatus state = node.Tick();
            if (state == BTStatus.FAILURE || state == BTStatus.RUNNING) return state;
        }

        return BTStatus.SUCCESS;
    }
}
