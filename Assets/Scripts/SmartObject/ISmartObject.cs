public interface ISmartObject: IHasActions
{
    public ObjectType Type { get; }
}

public enum ObjectType
{
    FOOD,
    FOOD_CONTAINER,
    TREE,
    WOOD,
    HOUSE,
    HOUSE_CONST_SITE
}
