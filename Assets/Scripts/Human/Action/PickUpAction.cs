using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpAction : BaseAction
{
    private PickUpComponent pickUpComponent;
    
    public override int Layer => 0;
    public override ActionID ID => ActionID.PICK_UP;
    public override string Name => "Pick up";

    public override void Init(Entity entity)
    {
        pickUpComponent = entity.GetComponent<PickUpComponent>(ComponentIDs.PICK_UP);
    }

    public override void Execute(Entity entity)
    {
        entity.GetComponent<CarryComponent>(ComponentIDs.CARRY).CarriedItem = pickUpComponent.Item;
        pickUpComponent.gameObject.SetActive(false);
        ActionFinished();
    }

    public override void Update()
    {}
}
