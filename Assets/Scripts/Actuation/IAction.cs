public interface IAction
{
    public void Update();
    public void Interrupt();
    public void Execute(Entity entity);

    public int Layer { get; }

    public ActionState Status { get; set; }

    public string Name { get; }
}

public interface IEntityAction : IAction
{
    public void Init(Entity entity);
    public ActionID ID { get; }

    public int ActionUsage { get; }

    public event System.Action OnInactive;

    public void OnReturn();
}
