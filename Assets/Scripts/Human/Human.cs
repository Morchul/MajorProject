using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Human : ActionEntity
{
    [field: SerializeField]
    public float Strength { get; private set; }
}
