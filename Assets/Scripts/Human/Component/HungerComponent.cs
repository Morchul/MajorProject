using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HungerComponent : Component
{
    public override int ID => ComponentIDs.HUNGER;

    public float Food;
}
