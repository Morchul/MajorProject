using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BTMoveTo : AbstractBTNode
{
    private readonly IEntityAction moveForward;
    private readonly IEntityAction turnRight;
    private readonly IEntityAction turnLeft;

    private readonly Agent entity;
    private readonly CharacterBaseAI ai;

    public float MaxDistanceSqrt { get; set; }

    public BTMoveTo(Agent entity, CharacterBaseAI ai)
    {
        this.entity = entity;
        this.ai = ai;

        moveForward = entity.GetAction(ActionID.MOVE_FORWARD);
        turnLeft = entity.GetAction(ActionID.TURN_LEFT);
        turnRight = entity.GetAction(ActionID.TURN_RIGHT);
    }

    public override BTStatus Tick()
    {
        if (ai.MoveTarget == Vector3.zero) return BTStatus.FAILURE;

        Vector3 dir = ai.MoveTarget - entity.transform.position;
        float dotProduct = Vector2.Dot(entity.transform.forward.ToVector2_XZ(), dir.ToVector2_XZ().normalized);
        Vector3 crossProduct = Vector3.Cross(entity.transform.forward, dir);

        if(dotProduct < 0.99f)
        {
            if (crossProduct.y > 0)
            {
                entity.AddAction(turnRight);
                turnLeft.Interrupt();
            }
            else
            {
                entity.AddAction(turnLeft);
                turnRight.Interrupt();
            }
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
            entity.AddAction(moveForward);
        else
            moveForward.Interrupt();

        if (moveForward.Status == ActionState.INACTIVE && turnRight.Status == ActionState.INACTIVE && turnLeft.Status == ActionState.INACTIVE)
        {
            ai.MoveTarget = Vector3.zero; //Target reached
            return BTStatus.SUCCESS;
        }
        else
            return BTStatus.RUNNING;
    }

    public override void CleanUp()
    {
        turnRight.Interrupt();
        turnLeft.Interrupt();
        moveForward.Interrupt();
    }
}
