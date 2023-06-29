using UnityEngine;

public class BTFindTarget : AbstractBTNode
{
    private readonly string searchTag;
    private readonly Transform pos;
    private readonly float radius;
    private readonly HumanAI ai;

    public SmartObject TargetObject { get; private set; }

    public BTFindTarget(HumanAI ai, string searchTag, Transform searchPos, float radius)
    {
        this.ai = ai;
        this.searchTag = searchTag;
        this.pos = searchPos;
        this.radius = radius;
        CleanUp();
        Name = "Find " + searchTag;
    }

    public override void CleanUp()
    {
        TargetObject = null;
    }

    public void SetActive()
    {
        ai.TargetObject = TargetObject;
        ai.MoveTarget = TargetObject.transform.position;
    }

    public override BTStatus Tick()
    {
        //if (TargetObject != null)
        //{
        //    SetActive();
        //    return BTStatus.SUCCESS;
        //}

        Collider[] colliders = Physics.OverlapSphere(pos.position, radius);
        if (colliders.Length > 0)
        {
            int closestIndex = -1;
            float closestDistanceSqrt = radius * radius + 1;
            for (int i = 0; i < colliders.Length; ++i)
            {
                if (!colliders[i].CompareTag(searchTag)) continue;

                float distance = (pos.position - colliders[i].transform.position).sqrMagnitude;
                if (distance < closestDistanceSqrt)
                {
                    closestIndex = i;
                    closestDistanceSqrt = distance;
                }
            }

            if (closestIndex > -1)
            {
                TargetObject = colliders[closestIndex].GetComponent<SmartObject>();
                SetActive();
                return BTStatus.SUCCESS;
            }
            else
            {
                TargetObject = null;
                ai.TargetObject = null;
            }
                
        }
        return BTStatus.FAILURE;
    }
}

public class BTSetTarget : AbstractBTNode
{
    private readonly BTFindTarget btFindTarget;
    public BTSetTarget(BTFindTarget btFindTarget)
    {
        this.btFindTarget = btFindTarget;
        Name = "Set value of " + btFindTarget.Name;
    }

    public override BTStatus Tick()
    {
        btFindTarget.SetActive();
        return BTStatus.SUCCESS;
    }
}
