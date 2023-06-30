using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildComponent : EntityComponent
{
    public override int ID => ComponentIDs.BUILD;

    private int progress;
    public event System.Action OnBuildFinished;

    public override ActionComponent[] GetComponentActions()
    {
        return new ActionComponent[]
        {
            new ActionComponent()
            {
                ActionID = ActionID.BUILD,
                MaxContainerSize = 2,
                StartContainerSize = 1
            }
        };
    }

    private ContainerComponent container;
    public override void Init(Entity entity)
    {
        container = entity.GetComponent<ContainerComponent>(ComponentIDs.CONTAINER);
    }

    public virtual bool AbleToBuild() => container.IsFull;

    public void Build(int strength)
    {
        if((progress += 1 + strength) >= 100)
        {
            container.Clear();
            OnBuildFinished?.Invoke();
        }
    }
}
