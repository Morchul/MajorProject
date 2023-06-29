public class CarryComponent : EntityComponent
{
    public override int ID => ComponentIDs.CARRY;

    //ONLY FOR DEBUG
    public bool IsNull;

    private SmartObject tmp;
    public SmartObject CarriedItem
    {
        get => tmp;
        set
        {
            tmp = value;
        }
    }

    private void Update()
    {
        IsNull = CarriedItem == null;
    }

    public override ActionComponent[] GetComponentActions()
    {
        return new ActionComponent[]
        {
            new ActionComponent()
            {
                ActionID = ActionID.DROP,
                MaxContainerSize = 1,
                StartContainerSize = 1
            }
        };
    }
}
