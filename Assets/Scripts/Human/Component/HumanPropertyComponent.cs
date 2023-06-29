using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanPropertyComponent : EntityComponent
{
    public override int ID => ComponentIDs.HUMAN_PROPERTY;

    public float Strength;
}
