using UnityEngine;

public class BTLocateFood : AbstractBTNode
{
    private readonly HumanAI ai;

    public BTLocateFood(HumanAI ai)
    {
        this.ai = ai;
    }

    public override BTStatus Tick()
    {
        if (ai.TargetFood != null) return BTStatus.SUCCESS;

        Vector3 pos = ai.transform.position;
        Collider[] colliders = Physics.OverlapSphere(pos, 10);
        if(colliders.Length > 0)
        {

            int closestIndex = 0;
            float closestDistanceSqrt = 100_000;
            for(int i = 0; i < colliders.Length; ++i)
            {
                if (!colliders[i].CompareTag("Food")) continue;

                float distance = (pos - colliders[i].transform.position).sqrMagnitude;
                if (distance < closestDistanceSqrt)
                {
                    closestIndex = i;
                    closestDistanceSqrt = distance;
                }
            }

            ai.TargetFood = colliders[closestIndex].GetComponent<Food>();
            if(ai.TargetFood != null)
            {
                ai.MoveTarget = ai.TargetFood.transform.position;
                return BTStatus.SUCCESS;
            }
        }
        return BTStatus.FAILURE;
    }
}
