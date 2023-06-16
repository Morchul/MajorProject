using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EatAction : BaseAction
{
    public override int Layer => 0; //Instant action does not need animation or anything does not intersect with everything (for the moment)

    public override string Name => "Eat";

    public override ActionID ID => ActionID.EAT;

    private Eatable eatable;

    public override void Init(Entity entity)
    {
        eatable = entity.GetComponent<Eatable>(ComponentIDs.EATABLE);
    }

    public override void Execute(Entity entity)
    {
        HungerComponent hunger = entity.GetComponent<HungerComponent>(ComponentIDs.HUNGER);
        hunger.Food = Mathf.Min(hunger.Food + eatable.Amount, 100);
        Debug.Log("Eat... new food meter: " + hunger.Food);
        GameObject.Destroy(eatable.gameObject);
        Status = ActionState.FINISHED;
    }

    public override void Update() {}
}