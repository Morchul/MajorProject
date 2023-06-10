using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISmartItem : IItem
{
    public IAction GetAction(int actionID);
    public IAction[] GetActions();
}
