public abstract class AbstractDecision : IDecision
{
    protected readonly IPlan plan;

    private DecisionModifier activeModifier;

    public float Modifier { get; private set; }

    //The three modifier who determine how the decision is modified based on the recent performance of this decision
    public DecisionModifier RunningDecisionModifier = DecisionModifier.Default;
    public DecisionModifier FailedDecisionModifier = DecisionModifier.Default;
    public DecisionModifier SuccessDecisionModifier = DecisionModifier.Default;

    public AbstractDecision(IPlan plan)
    {
        this.plan = plan;
    }

    public virtual float CalculateUtility(float deltaTime)
    {
        Modifier = activeModifier.Update(deltaTime);

        return CalculateUtility() + Modifier;
    }

    /// <summary>
    /// Is called after this decision is finished. Either because it failed or succeeded or something triggered a new decision making
    /// It also selectes the modifier which is used during the next utility calculations
    /// </summary>
    public void Stop()
    {
        activeModifier = plan.CurrentState switch
        {
            PlanState.RUNNING => RunningDecisionModifier,
            PlanState.FAILURE => FailedDecisionModifier,
            PlanState.SUCCESSFUL => SuccessDecisionModifier,
            _ => throw new System.Exception("INVALID CODE, this should never happen!")
        };

        activeModifier.Ready();
    }

    /// <summary>
    /// Is called when this decision is selected
    /// </summary>
    public void Select()
    {
        
    }

    /// <summary>
    /// Called every frame if this decision is the current executed one
    /// </summary>
    public virtual void Update()
    {
        plan.Update();
    }

    /// <summary>
    /// Abstract version of CalculateUtility which should include the individual calculation of the decision.
    /// This is executed after the calculation of the modifier and the modifier will automatically added to this result.
    /// </summary>
    protected abstract float CalculateUtility();

    public virtual IPlan GetPlan() => plan;
    public virtual void Reset() => activeModifier.Reset();
}

public class Decision : AbstractDecision
{
    public delegate float UtilityCalculation(float modifier);

    private readonly UtilityCalculation UtilityCalculationMethod;

    public Decision(IPlan plan, UtilityCalculation utilityCalculation) : base(plan)
    {
        UtilityCalculationMethod = utilityCalculation;
    }

    protected override float CalculateUtility()
    {
        return UtilityCalculationMethod(Modifier);
    }
}
