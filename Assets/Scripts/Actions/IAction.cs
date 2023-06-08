public interface IAction<in T>
{
    public void Update();
    public void Interrupt();
    public void Execute(T executioner);

    public int Layer { get; }

    public ActionState Status { get; set; }

    public string Name { get; }

    public int ActionUsage { get; }
}
