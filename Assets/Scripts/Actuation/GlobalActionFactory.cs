public static class GlobalActionFactory
{
    public static ActionFactory EatActionFactory = new ActionFactory(() => new EatAction(), ActionID.EAT);
    public static ActionFactory MoveForwardFactory = new ActionFactory(() => new MoveForward(), ActionID.MOVE_FORWARD);
    public static ActionFactory TurnLeftFactory = new ActionFactory(() => new TurnLeftAction(), ActionID.TURN_LEFT);
    public static ActionFactory TurnRightFactory = new ActionFactory(() => new TurnRightAction(), ActionID.TURN_RIGHT);
    public static ActionFactory PickUpFactory = new ActionFactory(() => new PickUpAction(), ActionID.PICK_UP);
    public static ActionFactory TakeOutFactory = new ActionFactory(() => new TakeOutAction(), ActionID.TAKE_OUT);
    public static ActionFactory PutInFactory = new ActionFactory(() => new PutInAction(), ActionID.PUT_IN);

    public static ActionFactory GetFactory(ActionID actionID)
    {
        return actionID switch
        {
            ActionID.EAT => EatActionFactory,
            ActionID.MOVE_FORWARD => MoveForwardFactory,
            ActionID.TURN_LEFT => TurnLeftFactory,
            ActionID.TURN_RIGHT => TurnRightFactory,
            ActionID.PICK_UP => PickUpFactory,
            ActionID.TAKE_OUT => TakeOutFactory,
            ActionID.PUT_IN => PutInFactory,
            _ => null
        };
    }

    public static IAction GetAction(ActionID actionID, bool returnWhenInactive)
    {
        return GetFactory(actionID).GetAction(returnWhenInactive);
    }

    public static void ReturnAction(IAction action)
    {
        GetFactory(action.ID).ReturnAction(action);
    }
}
