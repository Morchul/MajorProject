using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EdibleComponent : SmartObjectComponent
{
    public float Amount;

    public override int ID => ComponentIDs.EDIBLE;

    public override ActionComponent[] GetComponentActions()
    {
        return new ActionComponent[]
        {
            new ActionComponent()
            {
                ActionID = ActionID.EAT,
                MaxContainerSize = 0,
                StartContainerSize = 0
            }
        };
    }
}
