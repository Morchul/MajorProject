public class MoveForward : BaseAction
{
    public override int Layer => (int)ActionLayers.LEGS;
    public override ActionID ID => ActionID.MOVE_FORWARD;
    public override string Name => "Move forward";

    private MoveComponent moveComponent;

    public override void Init(Entity entity)
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
        moveComponent.MoveForward();
    }
}
