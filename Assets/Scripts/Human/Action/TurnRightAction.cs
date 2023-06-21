public class TurnRightAction : BaseAction
{
    public override int Layer => 0;
    public override string Name => "Turn right";
    public override ActionID ID => ActionID.TURN_RIGHT;

    private MoveComponent moveComponent;

    public override void Execute(Entity entity)
    {
        moveComponent = entity.GetComponent<MoveComponent>(ComponentIDs.MOVE);
    }

    public override void Interrupt()
    {
        if (Status == ActionState.ACTIVE)
            ActionFinished();
    }

    public override void Update()
    {
        moveComponent.TurnRight();
    }
}

public class TurnLeftAction : BaseAction
{
    public override int Layer => 0;
    public override string Name => "Turn left";
    public override ActionID ID => ActionID.TURN_LEFT;

    private MoveComponent moveComponent;

    public override void Execute(Entity entity)
    {
        moveComponent = entity.GetComponent<MoveComponent>(ComponentIDs.MOVE);
    }

    public override void Interrupt()
    {
        if (Status == ActionState.ACTIVE)
            ActionFinished();
    }

    public override void Update()
    {
        moveComponent.TurnLeft();
    }
}
