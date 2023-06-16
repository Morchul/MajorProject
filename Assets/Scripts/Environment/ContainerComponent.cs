using UnityEngine;

public abstract class ContainerComponent : EntityComponent
{
    [SerializeField]
    protected int capacity;

    public override int ID => ComponentIDs.CONTAINER;

    public abstract void PutIn(ISmartObject obj);

    public abstract ISmartObject TakeOut();

    public abstract bool Empty { get; }

    public abstract bool CanTake(ISmartObject item);
}
