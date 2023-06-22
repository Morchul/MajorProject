using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HungerComponent : EntityComponent
{
    public override int ID => ComponentIDs.HUNGER;

    public float Food;

    private void Update()
    {
        Food -= Time.deltaTime;
    }

    public override ActionComponent[] GetComponentActions() => null;

    public override void Init(Entity entity) { }
}
