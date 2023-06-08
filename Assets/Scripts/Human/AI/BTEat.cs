using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BTEat : AbstractBTNode
{
    private readonly Human human;
    private readonly HumanAI ai;

    public BTEat(Human human, HumanAI ai)
    {
        this.human = human;
        this.ai = ai;
    }

    public override BTStatus Tick()
    {
        if (ai.TargetFood == null) return BTStatus.FAILURE;

        human.AddAction(ai.TargetFood.eatAction);
        ai.TargetFood = null;
        return BTStatus.SUCCESS;
    }
}
