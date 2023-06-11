using UnityEngine;

public abstract class BaseAction : IAction
{
    public abstract int Layer { get; }
    public abstract string Name { get; }
    public abstract int ID { get; }

    public virtual int ActionUsage => 0;

    public BaseAction()
    {
        status = ActionState.INACTIVE;
    }

    private ActionState status;
    public ActionState Status
    { 
        get => status;
        set
        {
            Debug.Log("Set status of action " + Name + " to: " + value);
            status = value;
        }
    }

    public virtual void Interrupt()
    {
        if(Status == ActionState.ACTIVE || Status == ActionState.WAITING)
            Status = ActionState.INTERRUPTED;
    }

    public abstract void Execute(Entity entity);
    public abstract void Update();
}
