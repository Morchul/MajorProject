public interface ITriggerTransaction
{
    public int Type { get; }
    public void Trigger();
}

public interface IContinuesTransaction
{
    public void Update();
}
