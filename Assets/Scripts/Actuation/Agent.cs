using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Agent : Entity, IHasActions, IAgent
{
    protected ActionRingBuffer ringBuffer;
    protected IAction nextAction;

    protected IEntityAction[] actions;

    protected override void Awake()
    {
        ringBuffer = new ActionRingBuffer(5);
        base.Awake();

        List<ActionComponent> actionComp = new List<ActionComponent>();
        foreach(EntityComponent component in components)
        {
            ActionComponent[] actionArray = component.GetComponentActions();
            if(actionArray != null)
                actionComp.AddRange(actionArray);
        }

        actions = new IEntityAction[actionComp.Count];
        for(int i = 0; i < actionComp.Count; ++i)
        {
            IEntityAction action = GlobalActionFactory.GetAction(actionComp[i].ActionID, false);
            action.Init(this);
            actions[i] = action;
        }
    }

    protected virtual void ReturnActions()
    {
        foreach (IEntityAction action in actions) GlobalActionFactory.ReturnAction(action);
    }

    protected virtual void Update()
    {
        UpdateActions();
    }

    public bool TryGetAction(ActionID actionID, out IEntityAction action)
    {
        action = GetAction(actionID);
        return action != null;
    }

    public IEntityAction GetAction(ActionID actionID)
    {
        foreach (IEntityAction action in actions)
            if (action.ID == actionID)
                return action;
        return null;
    }

    #region Action Queue
    public void AddAction(IAction action)
    {
        if (action.IsActive()) return;

        if (nextAction != null && (nextAction.Status == ActionState.INTERRUPTED || nextAction.Status == ActionState.WAITING))
            nextAction.Status = ActionState.INACTIVE;

        nextAction = action;
        if (nextAction.Status == ActionState.INACTIVE)
            nextAction.Status = ActionState.WAITING;

        if (ringBuffer.Empty)
        {
            ExecuteAction();
        }
        else
        {
            bool intersect = false;
            foreach (IAction curAction in ringBuffer)
            {
                if ((action.Layer & curAction.Layer) > 0)
                {
                    curAction.Interrupt();
                    intersect = true;
                }
            }

            if (!intersect)
                ExecuteAction();
        }
    }

    private void ExecuteAction()
    {
        if (nextAction.Status == ActionState.INTERRUPTED)
        {
            nextAction.Status = ActionState.INACTIVE;
        }
        else
        {
            ringBuffer.Add(nextAction);
            nextAction.Execute(this);
        }


        nextAction = null;
    }

    private void UpdateActions()
    {
        foreach (IAction action in ringBuffer)
        {
            action.Update();
            if (action.Status == ActionState.FINISHED)
            {
                action.Status = ActionState.SLEEPING;
                ActionFinished();
            }
        }
    }

    private void ActionFinished()
    {
        ringBuffer.Remove();

        //Check if nextAction can now be executed
        if (nextAction != null)
        {
            foreach (IAction curAction in ringBuffer)
            {
                if ((nextAction.Layer & curAction.Layer) > 0)
                {
                    return;
                }
            }
            ExecuteAction();
        }
    }
    #endregion
}
