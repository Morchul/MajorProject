public class BTCheckTargetObject : AbstractBTNode
{
    private readonly int checkForComponentID;

    private readonly HumanAI ai;
    public BTCheckTargetObject(HumanAI ai, int checkForComponentID)
    {
        this.ai = ai;
        this.checkForComponentID = checkForComponentID;
        Name = "Check target object for component: " + checkForComponentID;
    }

    public override BTStatus Tick()
    {
        if(ai.TargetObject != null && checkForComponentID > 0)
        {
            if (ai.TargetObject.HasComponent(checkForComponentID))
            {
                return BTStatus.SUCCESS;
            }
        }
        return BTStatus.FAILURE;
    }
}

public class BTCheckTargetObjectType : AbstractBTNode
{
    private readonly ObjectType type;

    private readonly HumanAI ai;
    public BTCheckTargetObjectType(HumanAI ai, ObjectType type)
    {
        this.ai = ai;
        this.type = type;
    }

    public override BTStatus Tick()
    {
        if (ai.TargetObject != null)
        {
            if (ai.TargetObject.Type == type)
            {
                return BTStatus.SUCCESS;
            }
        }
        return BTStatus.FAILURE;
    }
}
