public class MoveForwardAction : BaseAction
{
    public override int Layer => (int)ActionLayers.LEGS;
    public override ActionID ID => ActionID.MOVE_FORWARD;
    public override string Name => "Move forward";

    private MoveComponent moveComponent;
    private AnimationComponent animationComponent;

    public override void Init(Entity entity)
    {
        moveComponent = entity.GetComponent<MoveComponent>(ComponentIDs.MOVE);
        animationComponent = entity.GetComponent<AnimationComponent>(ComponentIDs.ANIMATION);
    }

    public override void Execute(Entity entity)
    {
        animationComponent.Animator.SetFloat("SpeedForward", 1);
    }

    public override void Update()
    {
        if(Status == ActionState.INTERRUPTED)
        {
            animationComponent.Animator.SetFloat("SpeedForward", 0);
            ActionFinished();
            return;
        }

        moveComponent.MoveForward();
    }

    protected override int[] GetMandatoryComponentIDs() => new int[] { };
}
