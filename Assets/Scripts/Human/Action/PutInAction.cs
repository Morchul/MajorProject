using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PutInAction : BaseAction
{
    public override int Layer => 0;
    public override ActionID ID => ActionID.PUT_IN;
    public override string Name => "Put in";

    private ContainerComponent container;

    public override void Init(Entity entity)
    {
        container = entity.GetComponent<ContainerComponent>(ComponentIDs.CONTAINER);
    }

    public override void Execute(Entity entity)
    {
        CarryComponent carryComponent = entity.GetComponent<CarryComponent>(ComponentIDs.CARRY);
        if (carryComponent.CarriedItem != null)
        {
            if (container.CanTake(carryComponent.CarriedItem))
            {
                container.PutIn(carryComponent.CarriedItem);
                carryComponent.CarriedItem = null;
            }
        }

        Status = ActionState.FINISHED;
    }

    public override void Update()
    {}
}
