public class BTSelector : BTComposite
{
    public BTSelector(params AbstractBTNode[] children) : base(children)
    {}

    public override BTStatus Tick()
    {
        foreach (AbstractBTNode node in children)
        {
            BTStatus state = node.Tick();
            if (state == BTStatus.SUCCESS || state == BTStatus.RUNNING) return state;
        }
        return BTStatus.FAILURE;
    }
}
