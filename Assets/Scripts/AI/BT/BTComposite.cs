public abstract class BTComposite : AbstractBTNode
{
    protected readonly AbstractBTNode[] children;

    public BTComposite(string name, params AbstractBTNode[] children)
    {
        this.children = children;
        Name = name;
    }

    public override void CleanUp()
    {
        foreach (AbstractBTNode child in children)
            child.CleanUp();
    }
}
