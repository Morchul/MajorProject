using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractBTDecorator : AbstractBTNode
{
    protected readonly AbstractBTNode child;

    public AbstractBTDecorator(AbstractBTNode child)
    {
        this.child = child;
    }
}

public class BTDecorator : AbstractBTDecorator
{
    private readonly System.Func<BTStatus, BTStatus> OnTickMethod;

    public BTDecorator(AbstractBTNode child, System.Func<BTStatus, BTStatus> OnTick) : base(child)
    {
        OnTickMethod = OnTick;
    }

    public override BTStatus Tick()
    {
        return OnTickMethod(child.Tick());
    }
}
