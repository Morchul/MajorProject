using System;
using UnityEngine;

public abstract class BaseAction : IEntityAction
{
    public abstract int Layer { get; }
    public abstract string Name { get; }
    public abstract ActionID ID { get; }

    public virtual int ActionUsage => 0;
    public event Action OnInactive;

    public BaseAction()
    {
        status = ActionState.INACTIVE;
    }

    public virtual void Init(Entity entity){}
    public virtual void Update() { }
    public virtual void Execute(Entity entity) { }

    private ActionState status;

    public ActionState Status
    { 
        get => status;
        set
        {
            Debug.Log("Set status of action " + Name + " to: " + value);
            status = value;
            if (status == ActionState.INACTIVE) OnInactive?.Invoke();
        }
    }

    public virtual void Interrupt()
    {
        if(Status == ActionState.ACTIVE || Status == ActionState.WAITING)
            Status = ActionState.INTERRUPTED;
    }

    public void OnReturn()
    {
        OnInactive = null;
    }

    protected void ActionFinished()
    {
        Status = ActionState.FINISHED;
    }

    public bool CanBeExecutedBy(Entity entity)
    {
        int[] compIDs = GetMandatoryComponentIDs();
        for(int i = 0; i < compIDs.Length; ++i)
        {
            if (!entity.HasComponent(compIDs[i])) return false;
        }
        return true;
    }

    protected abstract int[] GetMandatoryComponentIDs();
}
