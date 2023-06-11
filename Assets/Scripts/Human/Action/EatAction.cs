using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EatAction : SmartObjectAction
{
    public override int Layer => 0; //Instant action does not need animation or anything does not intersect with everything (for the moment)

    public override string Name => "Eat";

    public override int ID => ActionIDs.EAT;

    private readonly float amount;

    public EatAction(float amount, System.Action onExecute) : base(onExecute)
    {
        this.amount = amount;
    }

    public override void Execute(Entity entity)
    {
        HungerComponent hunger = entity.GetComponent<HungerComponent>(ComponentIDs.HUNGER);
        hunger.Food = Mathf.Min(hunger.Food + amount, 100);
        Debug.Log("Eat... new food meter: " + hunger.Food);
        Status = ActionState.FINISHED;
        base.Execute(entity);
    }

    public override void Update() {}
}
