public interface IAction
{
    public void Update();
    public void Interrupt();
    public void Execute(Entity entity);

    public int Layer { get; }

    public ActionState Status { get; set; }

    public string Name { get; }

    public int ActionUsage { get; }
}
