using UnityEngine;

public abstract class ContainerComponent : SmartObjectComponent
{
    [SerializeField]
    protected int capacity;

    public override int ID => ComponentIDs.CONTAINER;

    public abstract void PutIn(ISmartObject obj);

    public abstract ISmartObject TakeOut();

    public abstract bool Empty { get; }

    public abstract bool CanTake(ISmartObject item);

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
