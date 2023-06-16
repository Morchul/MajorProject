public abstract class AbstractDecision : IDecision
{
    protected readonly IPlan plan;

    public AbstractDecision(IPlan plan)
    {
        this.plan = plan;
    }

    public abstract float CalculateUtility();
    public virtual IPlan GetPlan() => plan;

    public virtual void Update()
    {
        plan.Update();
    }
}

public class Decision : AbstractDecision
{
    private readonly System.Func<PlanState, float> UtilityCalculation;

    public Decision(IPlan plan, System.Func<PlanState, float> utilityCalculation) : base(plan)
    {
        UtilityCalculation = utilityCalculation;
    }

    public override float CalculateUtility() => UtilityCalculation(plan.GetLastRunState());
}
