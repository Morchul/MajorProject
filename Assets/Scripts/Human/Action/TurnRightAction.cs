public class TurnRightAction : BaseAction
{
    public override int Layer => 0;
    public override string Name => "Turn right";
    public override ActionID ID => ActionID.TURN_RIGHT;

    private MoveComponent moveComponent;
    private AnimationComponent animationComponent;

    public override void Init(Entity entity)
    {
        moveComponent = entity.GetComponent<MoveComponent>(ComponentIDs.MOVE);
        animationComponent = entity.GetComponent<AnimationComponent>(ComponentIDs.ANIMATION);
    }

    public override void Execute(Entity entity)
    {
        animationComponent.Animator.SetFloat("Turn", 1);
    }

    public override void Update()
    {
        if (Status == ActionState.INTERRUPTED)
        {
            animationComponent.Animator.SetFloat("Turn", 0);
            ActionFinished();
            return;
        }     

        moveComponent.TurnRight();
    }
    protected override int[] GetMandatoryComponentIDs() => new int[] { };
}

public class TurnLeftAction : BaseAction
{
    public override int Layer => 0;
    public override string Name => "Turn left";
    public override ActionID ID => ActionID.TURN_LEFT;

    private MoveComponent moveComponent;
    private AnimationComponent animationComponent;

    public override void Init(Entity entity)
    {
        moveComponent = entity.GetComponent<MoveComponent>(ComponentIDs.MOVE);
        animationComponent = entity.GetComponent<AnimationComponent>(ComponentIDs.ANIMATION);
    }

    public override void Execute(Entity entity)
    {
        animationComponent.Animator.SetFloat("Turn", -1);
    }

    public override void Update()
    {
        if (Status == ActionState.INTERRUPTED)
        {
            animationComponent.Animator.SetFloat("Turn", 0);
            ActionFinished();
            return;
        }
        moveComponent.TurnLeft();
    }
    protected override int[] GetMandatoryComponentIDs() => new int[] { };
}
