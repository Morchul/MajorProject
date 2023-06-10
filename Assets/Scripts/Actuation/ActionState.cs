using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ActionState
{
    WAITING = 0,
    ACTIVE = 1,
    INTERRUPTED = 2,
    FINISHED = 3,
    SLEEPING = 4,
    INACTIVE = 5,
}

[System.Flags]
public enum ActionLayers
{
    ARMS = 1,
    LEGS = 2,
}
