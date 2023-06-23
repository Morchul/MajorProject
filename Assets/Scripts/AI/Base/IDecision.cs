public interface IDecision
{
    public IPlan GetPlan();
    public float CalculateUtility(float deltaTime);
    public void Update();
    public void Stop();
}
