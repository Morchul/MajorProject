public abstract class AbstractDecision : IDecision
{
    protected readonly IPlan plan;

    public AbstractDecision(IPlan plan)
    {
        this.plan = plan;
    }

    public abstract float CalculateUtility();
    public virtual IPlan GetPlan() => plan;
}

public class Decision : AbstractDecision
{
    private readonly System.Func<float> UtilityCalculation;

    public Decision(IPlan plan, System.Func<float> utilityCalculation) : base(plan)
    {
        UtilityCalculation = utilityCalculation;
    }

    public override float CalculateUtility() => UtilityCalculation();
}
