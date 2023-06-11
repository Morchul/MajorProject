using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PutInAction : SmartObjectAction
{

    private readonly IContainer<Food> container;
    public PutInAction(IContainer<Food> container, Action onExecute) : base(onExecute)
    {
        this.container = container;
    }

    public override int Layer => 0;
    public override int ID => ActionIDs.PUT_IN;
    public override string Name => "Put in";

    public override void Execute(Entity entity)
    {
        CarryComponent carryComponent = entity.GetComponent<CarryComponent>(ComponentIDs.CARRY);
        if(carryComponent.CarriedItem != null && carryComponent.CarriedItem is Food food)
        {
            if (container.CanTake(food))
            {
                container.PutIn(food);
                carryComponent.CarriedItem = null;
            }
        }
        

        Status = ActionState.FINISHED;
        base.Execute(entity);
    }

    public override void Update()
    {}
}
