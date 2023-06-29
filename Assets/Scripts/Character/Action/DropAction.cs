using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropAction : BaseAction
{
    public override int Layer => 0;

    public override string Name => "Drop";

    public override ActionID ID => ActionID.DROP;

    private CarryComponent carry;

    protected override int[] GetMandatoryComponentIDs() => new int[] { ComponentIDs.CARRY };

    public override void Init(Entity entity)
    {
        carry = entity.GetComponent<CarryComponent>(ComponentIDs.CARRY);
    }

    public override void Execute(Entity entity)
    {
        if(carry.CarriedItem != null)
        {
            carry.CarriedItem.SetToObject(entity.transform.position);
            carry.CarriedItem = null;
        }
        ActionFinished();
    }
}
