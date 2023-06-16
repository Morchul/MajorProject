public interface ISmartObject: IHasActions
{
    public ObjectType Type { get; }
}

public enum ObjectType
{
    FOOD,
    FOOD_CONTAINER
}
