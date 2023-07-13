using UnityEngine;

public class MoveComponent : EntityComponent
{
    public float MoveSpeed;
    public float TurnSpeed;

    public override int ID => ComponentIDs.MOVE;

    public override ActionComponent[] GetComponentActions()
    {
        return new ActionComponent[]
        {
            new ActionComponent()
            {
                ActionID = ActionID.MOVE_FORWARD,
                MaxContainerSize = 1,
                StartContainerSize = 1
            },
            new ActionComponent()
            {
                ActionID = ActionID.MOVE,
                MaxContainerSize = 1,
                StartContainerSize = 1
            },
            new ActionComponent()
            {
                ActionID = ActionID.MOVE_BACKWARD,
                MaxContainerSize = 1,
                StartContainerSize = 1
            },
            new ActionComponent()
            {
                ActionID = ActionID.TURN_LEFT,
                MaxContainerSize = 1,
                StartContainerSize = 1
            },
            new ActionComponent()
            {
                ActionID = ActionID.TURN_RIGHT,
                MaxContainerSize = 1,
                StartContainerSize = 1
            }
        };
    }

    public void Move(Vector3 moveDir)
    {
        transform.Translate(moveDir * Time.deltaTime);
    }

    public void MoveForward()
    {
        transform.Translate(Vector3.forward * Time.deltaTime * MoveSpeed);
    }

    public void MoveBackward()
    {
        transform.Translate(Vector3.forward * Time.deltaTime * -MoveSpeed);
    }

    public void TurnRight()
    {
        Vector3 rotation = new Vector3(0, Time.deltaTime * TurnSpeed, 0);
        transform.Rotate(rotation);
    }

    public void TurnLeft()
    {
        Vector3 rotation = new Vector3(0, Time.deltaTime * -TurnSpeed, 0);
        transform.Rotate(rotation);
    }
}
