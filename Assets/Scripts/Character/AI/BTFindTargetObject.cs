using UnityEngine;

public class BTFindTargetObject : AbstractBTNode
{
    private readonly string searchTag;
    private readonly Transform pos;
    private readonly float radius;
    private readonly CharacterBaseAI ai;

    public SmartObject TargetObject { get; private set; }

    public BTFindTargetObject(CharacterBaseAI ai, string searchTag, Transform searchPos, float radius)
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

        TargetObject = BTUtility.SearchClosest<SmartObject>(searchTag, pos.position, radius);
        if (TargetObject == null)
        {
            ai.TargetObject = null;
            return BTStatus.FAILURE;
        }
        else
        {
            SetActive();
            return BTStatus.SUCCESS;
        }

        //Collider[] colliders = Physics.OverlapSphere(pos.position, radius);
        //if (colliders.Length > 0)
        //{
        //    int closestIndex = -1;
        //    float closestDistanceSqrt = radius * radius + 1;
        //    for (int i = 0; i < colliders.Length; ++i)
        //    {
        //        if (!colliders[i].CompareTag(searchTag)) continue;

        //        float distance = (pos.position - colliders[i].transform.position).sqrMagnitude;
        //        if (distance < closestDistanceSqrt)
        //        {
        //            closestIndex = i;
        //            closestDistanceSqrt = distance;
        //        }
        //    }

        //    if (closestIndex > -1)
        //    {
        //        TargetObject = colliders[closestIndex].GetComponent<SmartObject>();
        //        SetActive();
        //        return BTStatus.SUCCESS;
        //    }
        //    else
        //    {
        //        TargetObject = null;
        //        ai.TargetObject = null;
        //    }

        //}
        //return BTStatus.FAILURE;
    }
}

public class BTSetTargetObject : AbstractBTNode
{
    private readonly BTFindTargetObject btFindTarget;
    public BTSetTargetObject(BTFindTargetObject btFindTarget)
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

public class BTFindTargetEntity : AbstractBTNode
{
    private readonly string searchTag;
    private readonly Transform pos;
    private readonly float radius;
    private readonly CharacterBaseAI ai;

    public Entity TargetEntity { get; private set; }

    public BTFindTargetEntity(CharacterBaseAI ai, string searchTag, Transform searchPos, float radius)
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
        TargetEntity = null;
    }

    public void SetActive()
    {
        ai.TargetEntity = TargetEntity;
        ai.MoveTarget = TargetEntity.transform.position;
    }

    public override BTStatus Tick()
    {
        TargetEntity = BTUtility.SearchClosest<Entity>(searchTag, pos.position, radius);
        if (TargetEntity == null)
        {
            ai.TargetObject = null;
            return BTStatus.FAILURE;
        }
        else
        {
            SetActive();
            return BTStatus.SUCCESS;
        }
    }
}

public class BTSetTargetEntity : AbstractBTNode
{
    private readonly BTFindTargetEntity btFindTarget;
    public BTSetTargetEntity(BTFindTargetEntity btFindTarget)
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
