using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveComponent : Component
{
    public float MoveSpeed;
    public float TurnSpeed;

    public override int ID => ComponentIDs.MOVE;
}
