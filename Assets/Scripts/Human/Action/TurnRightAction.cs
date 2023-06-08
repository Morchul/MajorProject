using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnRightAction : BaseAction<BaseEntity>
{
    public override int Layer => 0;

    public override string Name => "Turn right";

    private BaseEntity human;

    public override void Execute(BaseEntity executioner)
    {
        human = executioner;
    }

    public override void Interrupt()
    {
        if (Status == ActionState.ACTIVE)
            Status = ActionState.FINISHED;
    }

    public override void Update()
    {
        Vector3 rotation = new Vector3(0, Time.deltaTime * human.TurnSpeed, 0);
        human.transform.Rotate(rotation);
    }
}

public class TurnLeftAction : BaseAction<BaseEntity>
{
    public override int Layer => 0;
    public override string Name => "Turn left";

    private BaseEntity human;

    public override void Execute(BaseEntity executioner)
    {
        human = executioner;
    }

    public override void Interrupt()
    {
        if (Status == ActionState.ACTIVE)
            Status = ActionState.FINISHED;
    }

    public override void Update()
    {
        Vector3 rotation = new Vector3(0, Time.deltaTime * -human.TurnSpeed, 0);
        human.transform.Rotate(rotation);
    }
}
