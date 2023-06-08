using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Human : BaseEntity, IHungry
{
    [SerializeField]
    private int foodDebug;

    public float Food { get; set; }

    private void Awake()
    {
        ringBuffer = new ActionRingBuffer<Human>(5);
        Food = 51;
    }

    private void Update()
    {
        Food -= Time.deltaTime;
        foodDebug = (int)Food;

        UpdateActions();
    }

    #region Action Queue
    public ActionRingBuffer<Human> ringBuffer;
    public IAction<Human> nextAction;

    public void AddAction(IAction<Human> action)
    {
        if (action.IsActive()) return;

        nextAction = action;
        if(nextAction.Status == ActionState.INACTIVE)
            nextAction.Status = ActionState.WAITING;

        if (ringBuffer.Empty)
        {
            ExecuteAction();
        }
        else
        {
            bool intersect = false;
            foreach(IAction<Human> curAction in ringBuffer)
            {
                if((action.Layer & curAction.Layer) > 0)
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
        foreach(IAction<Human> action in ringBuffer)
        {
            action.Update();
            if(action.Status == ActionState.FINISHED)
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
            foreach (IAction<Human> curAction in ringBuffer)
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
