using UnityEngine;

public class BTRoot : IPlan
{
    private readonly AbstractBTNode startNode;
    private readonly AI ai;

    public BTRoot(AbstractBTNode startNode, AI ai)
    {
        this.startNode = startNode;
        this.ai = ai;
    }

    public PlanState Update()
    {
        AbstractBTNode.BTStatus status = startNode.Tick();

        //Debug.Log("BT ROOT FINISHED WITH: " + status);
        if(status == AbstractBTNode.BTStatus.SUCCESS ||
            status == AbstractBTNode.BTStatus.FAILURE)
        {
            startNode.CleanUp();
            ai.MakeNewDecision();
        }

        return status.GetPlanState();
    }
}
