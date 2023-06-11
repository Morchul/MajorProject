using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveForward : BaseAction
{
    public override int Layer => (int)ActionLayers.LEGS;
    public override int ID => ActionIDs.MOVE_FORWARD;
    public override string Name => "Move forward";

    private Entity entity;
    private MoveComponent moveComponent;

    public override void Execute(Entity entity)
    {
        this.entity = entity;
        moveComponent = this.entity.GetComponent<MoveComponent>(ComponentIDs.MOVE);
    }

    public override void Interrupt()
    {
        if (Status == ActionState.ACTIVE)
            Status = ActionState.FINISHED;
    }

    public override void Update()
    {
        entity.transform.Translate(Vector3.forward * Time.deltaTime * moveComponent.MoveSpeed);
    }
}
