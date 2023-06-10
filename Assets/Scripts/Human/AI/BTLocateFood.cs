using UnityEngine;

public class BTLocateFood : AbstractBTNode
{
    private readonly HumanAI ai;

    public BTLocateFood(HumanAI ai)
    {
        this.ai = ai;
    }

    public override BTStatus Tick()
    {
        if (ai.TargetFood != null) return BTStatus.SUCCESS;

        ai.TargetFood = BTFindSomething.SearchClosest<Food>("Food", ai.transform.position, 10);

        if (ai.TargetFood != null)
        {
            ai.MoveTarget = ai.TargetFood.transform.position;
            return BTStatus.SUCCESS;
        }

        return BTStatus.FAILURE;
    }
}
