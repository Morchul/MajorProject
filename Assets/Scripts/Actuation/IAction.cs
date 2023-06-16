public interface IAction
{
    public void Init(Entity entity);

    public void Update();
    public void Interrupt();
    public void Execute(Entity entity);

    public int Layer { get; }

    public ActionID ID { get; }

    public ActionState Status { get; set; }

    public string Name { get; }

    public int ActionUsage { get; }

    public event System.Action OnInactive;

    public void Return();
}
