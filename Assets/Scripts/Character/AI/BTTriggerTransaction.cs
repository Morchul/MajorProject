using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BTTriggerTransaction : AbstractBTNode
{
    private readonly ITriggerTransaction triggerTransaction;

    public BTTriggerTransaction(ITriggerTransaction triggerTransaction)
    {
        this.triggerTransaction = triggerTransaction;
    }

    public override BTStatus Tick()
    {
        triggerTransaction.Trigger();
        return BTStatus.SUCCESS;
    }
}
