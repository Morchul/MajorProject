using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Human : MonoBehaviour, IMoveable, IHungry
{
    [SerializeField]
    private float moveSpeed;

    public float Food { get; set; }

    #region IMoveable
    public void MoveForward()
    {
        transform.Translate(Vector3.forward * Time.deltaTime * moveSpeed);
    }

    public void TurnLeft()
    {
        throw new System.NotImplementedException();
    }

    public void TurnRight()
    {
        throw new System.NotImplementedException();
    }
    #endregion

    #region Action Queue
    public ActionRingBuffer ringBuffer;
    public IAction nextAction;

    public void AddAction(IAction action)
    {
        /*if (action.Instant)
        {
            action.Execute();
            return;
        }*/

        nextAction = action;

        if (ringBuffer.Empty)
        {
            ExecuteAction();
        }
        else
        {
            bool intersect = false;
            foreach(IAction curAction in ringBuffer)
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

    private void ActionFinished()
    {
        ringBuffer.Remove();

        if(nextAction != null)
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

    private void ExecuteAction()
    {
        nextAction.Status = ActionState.ACTIVE;
        ringBuffer.Add(nextAction);
        nextAction = null;
    }

    public void UpdateActions()
    {
        foreach(IAction action in ringBuffer)
        {
            action.Update();
            if(action.Status == ActionState.FINISHED)
                ActionFinished();
        }
    }

    #endregion

    private void Update()
    {
        Food -= Time.deltaTime;
    }
}
