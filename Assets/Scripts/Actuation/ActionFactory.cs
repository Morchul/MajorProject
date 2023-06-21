using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionFactory
{
    public readonly ActionID ActionID;

    private readonly Stack<IEntityAction> pool;
    private readonly System.Func<IEntityAction> CreateMethod;

    public ActionFactory(System.Func<IEntityAction> createMethod, ActionID actionID)
    {
        pool = new Stack<IEntityAction>();
        CreateMethod = createMethod;
        ActionID = actionID;
    }

    public IEntityAction GetAction(bool returnWhenInactive)
    {
        IEntityAction action = (pool.Count == 0) ? CreateMethod() : pool.Pop();
        //IAction action;
        //if (pool.Count == 0)
        //{
        //    action = CreateMethod();
        //    Debug.Log("Create action in factory: " + action.Name); 
        //}
        //else
        //{
        //    action = pool.Pop();
        //}

        if (returnWhenInactive)
        {
            action.OnInactive += () => { ReturnAction(action); };
        }
        return action;
    }

    public void ReturnAction(IEntityAction action)
    {
        Debug.Log("Return action: " + action.Name);
        action.OnReturn();
        pool.Push(action);
    }
}
