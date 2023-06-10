using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakeOutAction : SmartObjectAction
{
    private readonly IContainer<Food> container;
    public TakeOutAction(IContainer<Food> container, Action onExecute) : base(onExecute)
    {
        this.container = container;
    }

    public override int Layer => 0;

    public override string Name => "Take out";

    public override void Execute(Entity entity)
    {
        entity.GetComponent<CarryComponent>(ComponentIDs.CARRY).CarriedItem = container.TakeOut();
        Status = ActionState.FINISHED;
        base.Execute(entity);
    }

    public override void Update()
    {}
}
