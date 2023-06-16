public class BTRoot : IPlan
{
    private readonly AbstractBTNode startNode;
    private readonly AI ai;

    private PlanState lastRunState;

    public BTRoot(AbstractBTNode startNode, AI ai)
    {
        this.startNode = startNode;
        this.ai = ai;
        lastRunState = PlanState.NONE;
    }

    public PlanState GetLastRunState() => lastRunState;

    public void Update()
    {
        AbstractBTNode.BTStatus status = startNode.Tick();

        if(status == AbstractBTNode.BTStatus.SUCCESS ||
            status == AbstractBTNode.BTStatus.FAILURE)
        {
            startNode.CleanUp();
            lastRunState = status.GetPlanState();
            ai.MakeNewDecision();
        }
    }
}
