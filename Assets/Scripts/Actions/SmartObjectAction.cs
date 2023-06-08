using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SmartObjectAction<T> : BaseAction<T>
{
    private System.Action onExecute;

    public SmartObjectAction(System.Action onExecute)
    {
        this.onExecute = onExecute;
    }

    public override void Execute(T executioner)
    {
        onExecute?.Invoke();
    }
}
