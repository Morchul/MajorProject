public class HumanBuilderComponent : BuilderComponent
{
    public override int Progress => (int) (10 *properties.Strength);

    private HumanPropertyComponent properties;

    public override void Init(Entity entity)
    {
        properties = entity.GetComponent<HumanPropertyComponent>(ComponentIDs.HUMAN_PROPERTY);
    }
}
