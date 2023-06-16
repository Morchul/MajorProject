using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SmartObjectComponent : EntityComponent
{
    [SerializeField]
    private bool itemComponent;
    public bool ItemComponent => itemComponent;

    public abstract ActionComponent[] GetComponentActions();
}
