using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakeOutAction : BaseAction
{
    private ContainerComponent container;

    public override void Init(Entity entity)
    {
        container = entity.GetComponent<ContainerComponent>(ComponentIDs.CONTAINER);
    }

    public override int Layer => 0;
    public override string Name => "Take out";
    public override ActionID ID => ActionID.TAKE_OUT;

    public override void Execute(Entity entity)
    {
        entity.GetComponent<CarryComponent>(ComponentIDs.CARRY).CarriedItem = container.TakeOut();
        ActionFinished();
    }

    public override void Update()
    { }

    protected override int[] GetMandatoryComponentIDs() => new int[] { ComponentIDs.CARRY };
}
