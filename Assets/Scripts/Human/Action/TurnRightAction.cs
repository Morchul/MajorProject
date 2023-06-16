using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnRightAction : BaseAction
{
    public override int Layer => 0;
    public override string Name => "Turn right";
    public override ActionID ID => ActionID.TURN_RIGHT;

    private Entity human;
    private MoveComponent moveComponent;

    public override void Execute(Entity entity)
    {
        human = entity;
        moveComponent = human.GetComponent<MoveComponent>(ComponentIDs.MOVE);
    }

    public override void Interrupt()
    {
        if (Status == ActionState.ACTIVE)
            Status = ActionState.FINISHED;
    }

    public override void Update()
    {
        Vector3 rotation = new Vector3(0, Time.deltaTime * moveComponent.TurnSpeed, 0);
        human.transform.Rotate(rotation);
    }
}

public class TurnLeftAction : BaseAction
{
    public override int Layer => 0;
    public override string Name => "Turn left";
    public override ActionID ID => ActionID.TURN_LEFT;

    private Entity human;
    private MoveComponent moveComponent;

    public override void Execute(Entity entity)
    {
        this.human = entity;
        moveComponent = human.GetComponent<MoveComponent>(ComponentIDs.MOVE);
    }

    public override void Interrupt()
    {
        if (Status == ActionState.ACTIVE)
            Status = ActionState.FINISHED;
    }

    public override void Update()
    {
        Vector3 rotation = new Vector3(0, Time.deltaTime * -moveComponent.TurnSpeed, 0);
        human.transform.Rotate(rotation);
    }
}
