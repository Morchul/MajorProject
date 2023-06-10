using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Human : Entity
{
    protected override void Awake()
    {
        base.Awake();
        components = new Component[] { new HungerComponent(), new MoveComponent(), new CarryComponent() };
    }
}
