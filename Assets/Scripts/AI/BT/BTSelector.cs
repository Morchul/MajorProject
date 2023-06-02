public class BTSelector : BTComposite
{
    public BTSelector(params BTNode[] children) : base(children)
    {}

    public override BTStatus Tick()
    {
        foreach (BTNode node in children)
        {
            BTStatus state = node.Tick();
            if (state == BTStatus.SUCCESS || state == BTStatus.RUNNING) return state;
        }
        return BTStatus.FAILURE;
    }
}
