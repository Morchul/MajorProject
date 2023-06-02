public class BTRoot : IPlan
{
    private readonly BTNode startNode;
    private readonly AI ai;

    public BTRoot(BTNode startNode, AI ai)
    {
        this.startNode = startNode;
        this.ai = ai;
    }

    public void Update()
    {
        BTNode.BTStatus status = startNode.Tick();

        if(status == BTNode.BTStatus.SUCCESS ||
            status == BTNode.BTStatus.FAILURE)
        {
            ai.MakeNewDecision();
        }
    }
}
