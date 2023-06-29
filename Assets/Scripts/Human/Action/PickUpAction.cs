using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpAction : BaseAction
{
    private ItemComponent itemComponent;
    
    public override int Layer => 0;
    public override ActionID ID => ActionID.PICK_UP;
    public override string Name => "Pick up";

    public override void Init(Entity entity)
    {
        itemComponent = entity.GetComponent<ItemComponent>(ComponentIDs.ITEM);
    }

    public override void Execute(Entity entity)
    {
        CarryComponent carryComp = entity.GetComponent<CarryComponent>(ComponentIDs.CARRY);
        if(carryComp.CarriedItem != null)
        {
            //Drop item
            carryComp.CarriedItem.SetToObject(entity.transform.position);
        }
        carryComp.CarriedItem = itemComponent.PickUpItem();
        ActionFinished();
    }

    public override void Update()
    {}

    protected override int[] GetMandatoryComponentIDs() => new int[] { ComponentIDs.CARRY };
}
