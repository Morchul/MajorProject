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
        ai.TargetFood.EatBy(human);
        ai.TargetFood = null;
        return BTStatus.SUCCESS;
    }
}
