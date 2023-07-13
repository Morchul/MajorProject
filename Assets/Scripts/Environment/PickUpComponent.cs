using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpComponent : EntityComponent
{
    public SmartObject Item;

    public override int ID => ComponentIDs.PICK_UP;
}
