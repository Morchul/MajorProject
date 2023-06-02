public abstract class BTComposite : BTNode
{
    protected readonly BTNode[] children;

    public BTComposite(params BTNode[] children)
    {
        this.children = children;
    }
}
