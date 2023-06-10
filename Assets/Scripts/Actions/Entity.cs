using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Entity : MonoBehaviour
{
    protected Component[] components;

    protected ActionRingBuffer ringBuffer;
    protected IAction nextAction;

    protected virtual void Awake()
    {
        ringBuffer = new ActionRingBuffer(5);
    }

    protected virtual void Update()
    {
        UpdateActions();
    }

    public T GetComponent<T>(int componentID) where T : Component
    {
        foreach (Component component in components)
            if (component.ID == componentID)
                return (T)component;
        return null;
    }

    #region Action Queue
    public void AddAction(IAction action)
    {
        if (action.IsActive()) return;

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
            Debug.Log("Add new action: " + nextAction.Name);

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
