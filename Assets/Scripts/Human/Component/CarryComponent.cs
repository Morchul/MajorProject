using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarryComponent : EntityComponent
{
    public override int ID => ComponentIDs.CARRY;

    public IItem CarriedItem;
}
