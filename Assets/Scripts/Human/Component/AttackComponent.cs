using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackComponent : EntityComponent
{
    public float Damage;

    public Vector3 HitArea;

    public override int ID => ComponentIDs.ATTACK;

    public override ActionComponent[] GetComponentActions()
    {
        return new ActionComponent[]
        {
            new ActionComponent()
            {
                ActionID = ActionID.ATTACK,
                MaxContainerSize = 1,
                StartContainerSize = 1
            }
        };
    }
}
