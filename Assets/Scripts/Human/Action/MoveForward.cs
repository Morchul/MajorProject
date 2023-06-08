using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveForward : BaseAction<BaseEntity>
{
    public override int Layer => (int)ActionLayers.LEGS;

    public override string Name => "Move forward";

    private BaseEntity baseEntity;

    public override void Execute(BaseEntity executioner)
    {
        baseEntity = executioner;
    }

    public override void Interrupt()
    {
        if (Status == ActionState.ACTIVE)
            Status = ActionState.FINISHED;
    }

    public override void Update()
    {
        baseEntity.transform.Translate(Vector3.forward * Time.deltaTime * baseEntity.MoveSpeed);
    }
}
