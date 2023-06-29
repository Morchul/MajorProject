public class House : SmartObject
{
    protected override void Awake()
    {
        base.Awake();

        GetComponent<HealthComponent>(ComponentIDs.HEALTH).OnDeath += Destroyed;
    }

    private void Destroyed()
    {
        Destroy(this.gameObject);
    }
}
