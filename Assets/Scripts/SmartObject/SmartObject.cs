using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmartObject : Entity, ISmartObject
{
    [SerializeField]
    protected ObjectType type;
    public ObjectType Type => type;

    [SerializeField]
    protected ActionComponent[] actionComponents;

    protected ActionContainer[] actions;


    private void OnDestroy()
    {
        foreach (ActionContainer container in actions) container.Clear();
    }

    protected virtual void Start()
    {
        actions = new ActionContainer[actionComponents.Length];
        for(int i = 0; i < actionComponents.Length; ++i)
        {
            actions[i] = new ActionContainer(this, actionComponents[i]);
        }
        base.Awake();
    }

    public bool TryGetAction(ActionID actionID, out IAction action)
    {
        action = GetAction(actionID);
        return action != null;
    }

    public IAction GetAction(ActionID actionID)
    {
        for (int i = 0; i < actions.Length; ++i)
        {
            if (actions[i].ActionID == actionID)
            {
                return actions[i].GetAction();
            }
        }
        return null;
    }
}
