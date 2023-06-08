using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EatAction : SmartObjectAction<IHungry>
{
    public override int Layer => 0; //Instant action does not need animation or anything does not intersect with everything (for the moment)

    public override string Name => "Eat";

    private readonly float amount;

    public EatAction(float amount, System.Action onExecute) : base(onExecute)
    {
        this.amount = amount;
    }

    public override void Execute(IHungry hungry)
    {
        hungry.Food = Mathf.Min(hungry.Food + amount, 100);
        Debug.Log("Eat... new food meter: " + hungry.Food);
        Status = ActionState.FINISHED;
        base.Execute(hungry);
    }

    public override void Update() {}
}
