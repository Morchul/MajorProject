using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SmartItem : ISmartObject
{
    protected IAction[] actions;

    public abstract ObjectType Type { get; }

    public bool TryGetAction(int actionID, out IAction action)
    {
        for(int i = 0; i < actions.Length; ++i)
        {
            if (actions[i].ID == actionID)
            {
                action = actions[i];
                return true;
            }
        }
        action = null;
        return false;
    }

    public IAction GetAction(int actionID)
    {
        for (int i = 0; i < actions.Length; ++i)
        {
            if (actions[i].ID == actionID)
            {
                return actions[i];
            }
        }
        return null;
    }

    public IAction[] GetActions() => actions;
}
