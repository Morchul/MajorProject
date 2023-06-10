using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Component
{
    public abstract int ID { get; }
}

public static class ComponentIDs
{
    public const int MOVE = 1;
    public const int HUNGER = 2;
    public const int CARRY = 3;
}
