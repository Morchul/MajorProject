using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BTMoveTo : AbstractBTNode
{
    private readonly MoveForward moveForward;
    private readonly TurnRightAction turnRight;
    private readonly TurnLeftAction turnLeft;

    private readonly Human human;
    private readonly HumanAI ai;

    public float MaxDistanceSqrt { get; set; }

    public BTMoveTo(Human human, HumanAI ai)
    {
        this.human = human;
        this.ai = ai;

        moveForward = new MoveForward();
        turnRight = new TurnRightAction();
        turnLeft = new TurnLeftAction();
    }

    public override BTStatus Tick()
    {
        if (ai.MoveTarget == Vector3.zero) return BTStatus.FAILURE;

        Vector3 dir = ai.MoveTarget - human.transform.position;
        float dotProduct = Vector2.Dot(human.transform.forward.ToVector2_XZ(), dir.ToVector2_XZ().normalized);
        Vector3 crossProduct = Vector3.Cross(human.transform.forward, dir);

        if(dotProduct < 0.99f)
        {
            if (crossProduct.y > 0)
                human.AddAction(turnRight);
            else
                human.AddAction(turnLeft);
        }
        else
        {
            turnRight.Interrupt();
            turnLeft.Interrupt();
        }
        

        if(dir.sqrMagnitude < MaxDistanceSqrt)
        {
            moveForward.Interrupt();
        }
        else if (dotProduct > 0.7f)
            human.AddAction(moveForward);
        else
            moveForward.Interrupt();

        if (moveForward.Status == ActionState.INACTIVE && turnRight.Status == ActionState.INACTIVE && turnLeft.Status == ActionState.INACTIVE)
            return BTStatus.SUCCESS;
        else
            return BTStatus.RUNNING;
    }
}