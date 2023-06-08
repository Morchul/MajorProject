public class BTRoot : IPlan
{
    private readonly AbstractBTNode startNode;
    private readonly AI ai;

    public BTRoot(AbstractBTNode startNode, AI ai)
    {
        this.startNode = startNode;
        this.ai = ai;
    }

    public void Update()
    {
        AbstractBTNode.BTStatus status = startNode.Tick();

        if(status == AbstractBTNode.BTStatus.SUCCESS ||
            status == AbstractBTNode.BTStatus.FAILURE)
        {
            ai.MakeNewDecision();
        }
    }
}
