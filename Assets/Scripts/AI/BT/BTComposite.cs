public abstract class BTComposite : AbstractBTNode
{
    protected readonly AbstractBTNode[] children;

    public BTComposite(params AbstractBTNode[] children)
    {
        this.children = children;
    }
}
