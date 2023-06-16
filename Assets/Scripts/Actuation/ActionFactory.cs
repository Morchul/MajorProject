using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionFactory
{
    public readonly ActionID ActionID;

    private readonly Stack<IAction> pool;
    private readonly System.Func<IAction> CreateMethod;

    public ActionFactory(System.Func<IAction> createMethod, ActionID actionID)
    {
        pool = new Stack<IAction>();
        CreateMethod = createMethod;
        ActionID = actionID;
    }

    public IAction GetAction(bool returnWhenInactive)
    {
        //IAction action = (Pool.Count == 0) ? CreateAction() : Pool.Pop();
        IAction action;
        if (pool.Count == 0)
        {
            action = CreateMethod();
            Debug.Log("Create action in factory: " + action.Name); 
        }
        else
        {
            action = pool.Pop();
        }

        if (returnWhenInactive)
        {
            action.OnInactive += () => { ReturnAction(action); };
        }
        return action;
    }

    public void ReturnAction(IAction action)
    {
        Debug.Log("Return action: " + action.Name);
        action.Return();
        pool.Push(action);
    }
}
