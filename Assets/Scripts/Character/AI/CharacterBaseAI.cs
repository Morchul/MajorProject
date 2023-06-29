using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterBaseAI : AI
{
    [Header("Blackboard")]
    public SmartObject TargetObject;

    public Vector3 MoveTarget;
}
