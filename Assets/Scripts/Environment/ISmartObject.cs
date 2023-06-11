using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISmartObject
{
    public ObjectType Type { get; }

    public bool TryGetAction(int actionID, out IAction action);
    public IAction GetAction(int actionID);

    public IAction[] GetActions();
}

public enum ObjectType
{
    FOOD
}
