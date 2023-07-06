public class BTRoot : IPlan
{
    private readonly AbstractBTNode startNode;
    private readonly AI ai;

    private AbstractBTNode.BTStatus currentState;
    public PlanState CurrentState => currentState.GetPlanState();

    public BTRoot(AbstractBTNode startNode, AI ai)
    {
        this.startNode = startNode;
        this.ai = ai;
    }

    public void Update()
    {
        currentState = startNode.Tick();

        //Debug.Log("BT ROOT FINISHED WITH: " + status);
        if(currentState == AbstractBTNode.BTStatus.SUCCESS ||
            currentState == AbstractBTNode.BTStatus.FAILURE)
        {
            startNode.CleanUp();
            ai.MakeNewDecision();
        }
    }
}
