using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpComponent : SmartObjectComponent
{
    public SmartObject Item;

    public override int ID => ComponentIDs.PICK_UP;

    public override ActionComponent[] GetComponentActions()
    {
        return new ActionComponent[]
        {
            new ActionComponent()
            {
                ActionID = ActionID.PICK_UP,
                MaxContainerSize = 0,
                StartContainerSize = 0
            }
        };
    }
}
