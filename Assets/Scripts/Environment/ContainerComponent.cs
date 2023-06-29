using UnityEngine;

public abstract class ContainerComponent : EntityComponent
{
    [SerializeField]
    protected int capacity;

    public override int ID => ComponentIDs.CONTAINER;

    public abstract void PutIn(SmartObject obj);

    public abstract SmartObject TakeOut();

    public abstract bool Empty { get; }
    public abstract int Count { get; }

    public abstract void Clear();
    public abstract bool CanTake(SmartObject item);

    public override ActionComponent[] GetComponentActions()
    {
        return new ActionComponent[]
        {
            new ActionComponent()
            {
                ActionID = ActionID.PUT_IN,
                MaxContainerSize = 1,
                StartContainerSize = 1
            },
            new ActionComponent()
            {
                ActionID = ActionID.TAKE_OUT,
                MaxContainerSize = 1,
                StartContainerSize = 1
            }
        };
    }
}
