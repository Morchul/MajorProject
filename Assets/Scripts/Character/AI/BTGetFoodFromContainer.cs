using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BTGetFoodFromContainer : AbstractBTNode
{
    private readonly List<ContainerComponent> knownFoodContainer;
    private readonly HumanAI ai;

    public BTGetFoodFromContainer(HumanAI ai)
    {
        knownFoodContainer = new List<ContainerComponent>();
        this.ai = ai;
    }

    public override void CleanUp() { }


    //Looks for a food container. If one is found it remembers it.
    //Improvements would be: check how far away the known container is. If far maybe look for another one, therefore remember more than one
    public override BTStatus Tick()
    {
        if (knownFoodContainer.Count > 0)
        {
            if(!knownFoodContainer[0].Empty)
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
                        ContainerComponent container = colliders[i].GetComponent<ContainerComponent>();
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