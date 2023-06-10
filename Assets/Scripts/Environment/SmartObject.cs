using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SmartObject : MonoBehaviour, ISmartItem
{
    public abstract ItemType Type { get; }

    public IAction GetAction(int actionID)
    {
        throw new System.NotImplementedException();
    }

    public IAction[] GetActions()
    {
        throw new System.NotImplementedException();
    }
}
