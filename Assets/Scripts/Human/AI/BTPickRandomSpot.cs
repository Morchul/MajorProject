using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BTPickRandomSpot : AbstractBTNode
{
    private readonly HumanAI ai;

    public BTPickRandomSpot(HumanAI ai)
    {
        this.ai = ai;
    }

    public override BTStatus Tick()
    {
        if(ai.MoveTarget == Vector3.zero)
        {
            ai.MoveTarget = ai.transform.position + Extensions.RandomVector2(-6,6,-6,6).ToVector3_XZ();
        }

        return BTStatus.SUCCESS;
    }
}