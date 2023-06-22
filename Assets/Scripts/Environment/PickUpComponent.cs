using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpComponent : SmartObjectComponent
{
    public SmartObject Item;

    public override int ID => ComponentIDs.PICK_UP;

    public override ActionComponent[] GetComponentActions() => null;

    public override void Init(Entity entity) { }
}
