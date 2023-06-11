using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarryComponent : EntityComponent
{
    public override int ID => ComponentIDs.CARRY;

    //ONLY FOR DEBUG
    public bool IsNull;

    private ISmartObject tmp;
    public ISmartObject CarriedItem
    {
        get => tmp;
        set
        {
            tmp = value;
        }
    }

    private void Update()
    {
        IsNull = CarriedItem == null;
    }
}
