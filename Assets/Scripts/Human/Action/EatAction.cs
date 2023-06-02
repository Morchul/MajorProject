using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EatAction : IAction
{
    public int Layer => 0; //Instant action does not need animation or anything does not intersect with everything (for the moment)

    public ActionState Status { get; set ; }

    private readonly IHungry hungry;
    public EatAction(IHungry hungry)
    {
        this.hungry = hungry;
    }

    public void Execute()
    {
        hungry.Food = Mathf.Min(hungry.Food + 10, 100);
        Debug.Log("Eat... new food meter: " + hungry.Food);
        Status = ActionState.FINISHED;
    }

    public void Interrupt()
    {
        
    }

    public void Update()
    {
        
    }
}
