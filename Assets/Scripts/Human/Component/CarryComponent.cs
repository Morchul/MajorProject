using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarryComponent : Component
{
    public override int ID => ComponentIDs.CARRY;

    public IItem CarriedItem;
}
