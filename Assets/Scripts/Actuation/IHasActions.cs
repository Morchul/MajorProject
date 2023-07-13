public interface IHasActions
{
    public bool TryGetAction(ActionID actionID, out IEntityAction action);
    public IEntityAction GetAction(ActionID actionID);
}
