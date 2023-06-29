using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class BTUtility
{
    public static T SearchClosest<T>(string tag, Vector3 pos, float radius) where T : MonoBehaviour
    {
        Collider[] colliders = Physics.OverlapSphere(pos, radius);
        if (colliders.Length > 0)
        {
            int closestIndex = -1;
            float closestDistanceSqrt = radius * radius + 1;
            for (int i = 0; i < colliders.Length; ++i)
            {
                if (!colliders[i].CompareTag(tag)) continue;

                float distance = (pos - colliders[i].transform.position).sqrMagnitude;
                if (distance < closestDistanceSqrt)
                {
                    closestIndex = i;
                    closestDistanceSqrt = distance;
                }
            }

            if(closestIndex > -1)
                return colliders[closestIndex].GetComponent<T>();
        }
        return default;
    }
}

public class BTExecuteAction : AbstractBTNode
{
    private readonly ActionID actionID;
    private readonly ActionEntity agent;
    private readonly HumanAI ai;

    public BTExecuteAction(HumanAI ai, ActionEntity agent, ActionID actionID)
    {
        this.agent = agent;
        this.actionID = actionID;
        this.ai = ai;
        Name = "Execute action: " + actionID;
    }

    public override BTStatus Tick()
    {
        if(ai.TargetObject.TryGetAction(actionID, out IEntityAction action))
        {
            agent.AddAction(action);
            return BTStatus.SUCCESS;
        }
        Debug.Log($"Can not execute action {actionID} on object {ai.TargetObject.name}");
        return BTStatus.FAILURE;
        
    }
}

public class BTExecuteAgentAction : AbstractBTNode
{
    private readonly IEntityAction action;
    private readonly ActionEntity agent;

    public BTExecuteAgentAction(ActionEntity agent, ActionID actionID)
    {
        this.agent = agent;
        Name = "Execute agent action: " + actionID;
        if (!agent.TryGetAction(actionID, out action))
            throw new System.ArgumentException($"Can not execute action {actionID} on agent {agent.name}");
    }

    public override BTStatus Tick()
    {
        agent.AddAction(action);
        return BTStatus.SUCCESS;
    }
}

public class BTSuccessToRunning : AbstractBTDecorator
{
    public BTSuccessToRunning(AbstractBTNode child) : base(child)
    {
    }

    public override BTStatus Tick()
    {
        BTStatus status = child.Tick();
        return (status == BTStatus.SUCCESS) ? BTStatus.RUNNING : status;
    }
}

public class BTSuccessToFailure : AbstractBTDecorator
{
    public BTSuccessToFailure(AbstractBTNode child) : base(child)
    {
    }

    public override BTStatus Tick()
    {
        BTStatus status = child.Tick();
        return (status == BTStatus.SUCCESS) ? BTStatus.FAILURE : status;
    }
}

public class BTPickRandomSpot : AbstractBTNode
{
    private readonly HumanAI ai;

    public BTPickRandomSpot(HumanAI ai)
    {
        this.ai = ai;
    }

    public override BTStatus Tick()
    {
        if (ai.MoveTarget == Vector3.zero)
        {
            ai.MoveTarget = ai.transform.position + Utility.RandomVector2(-6, 6, -6, 6).ToVector3_XZ();
        }

        return BTStatus.SUCCESS;
    }
}
