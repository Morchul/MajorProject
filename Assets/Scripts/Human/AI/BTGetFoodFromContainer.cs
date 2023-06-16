using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BTGetFoodFromContainer : AbstractBTNode
{
    private readonly List<FoodContainer> knownFoodContainer;
    private readonly HumanAI ai;

    public BTGetFoodFromContainer(HumanAI ai)
    {
        knownFoodContainer = new List<FoodContainer>();
        this.ai = ai;
    }


    //Looks for a food container. If one is found it remembers it.
    //Improvements would be: check how far away the known container is. If far maybe look for another one, therefore remember more than one
    public override BTStatus Tick()
    {
        if (knownFoodContainer.Count > 0)
        {
            //if(!knownFoodContainer[0].Empty)
            {
                ai.MoveTarget = knownFoodContainer[0].transform.position;
                return BTStatus.SUCCESS;
            }
        }
        else
        {
            Collider[] colliders = Physics.OverlapSphere(ai.transform.position, 10);
            if (colliders.Length > 0)
            {
                for(int i = 0; i < colliders.Length; ++i)
                {
                    if(colliders[i].CompareTag("FoodContainer"))
                    {
                        FoodContainer container = colliders[i].GetComponent<FoodContainer>();
                        if (container == null)
                            return BTStatus.FAILURE;
                        else
                        {
                            knownFoodContainer.Add(container);
                            return BTStatus.RUNNING;
                        }
                    }
                }
            }
        }
        return BTStatus.FAILURE;
    }
}
