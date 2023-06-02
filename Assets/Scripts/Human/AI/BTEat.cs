using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BTEat : BTNode
{
    private readonly Human human;

    private EatAction eatAction;

    public BTEat(Human human)
    {
        this.human = human;
        eatAction = new EatAction(human);
    }

    public override BTStatus Tick()
    {
        if(eatAction.Status == ActionState.INACTIVE)
        human.AddAction(eatAction);
        if (eatAction.Status == ActionState.FINISHED)
            return BTStatus.SUCCESS;
        else
            return BTStatus.RUNNING;
    }
}
