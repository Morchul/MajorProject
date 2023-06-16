using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IHasActions
{
    public bool TryGetAction(ActionID actionID, out IAction action);
    public IAction GetAction(ActionID actionID);
}
