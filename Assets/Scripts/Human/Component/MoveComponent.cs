using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveComponent : EntityComponent
{
    public float MoveSpeed;
    public float TurnSpeed;

    public override int ID => ComponentIDs.MOVE;
}
