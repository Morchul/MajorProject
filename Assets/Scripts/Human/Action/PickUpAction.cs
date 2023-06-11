using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpAction : SmartObjectAction
{
    private readonly ISmartObject item;
    public PickUpAction(ISmartObject item, Action onExecute) : base(onExecute)
    {
        this.item = item;
    }

    public override int Layer => 0;
    public override int ID => ActionIDs.PICK_UP;
    public override string Name => "Pick up";

    public override void Execute(Entity entity)
    {
        entity.GetComponent<CarryComponent>(ComponentIDs.CARRY).CarriedItem = item;
        Status = ActionState.FINISHED;
        base.Execute(entity);
    }

    public override void Update()
    {}
}
