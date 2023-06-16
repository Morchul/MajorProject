public enum ActionID : int
{
    EAT,
    MOVE_FORWARD,
    TURN_RIGHT,
    TURN_LEFT,
    PICK_UP,
    PUT_IN,
    TAKE_OUT
}

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