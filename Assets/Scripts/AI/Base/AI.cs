using UnityEngine;

public abstract class AI : MonoBehaviour
{
    protected ISensor sensor;
    protected IPlan currentPlan;

    protected virtual void Start()
    {
        MakeNewDecision();
    }

    public void MakeNewDecision()
    {
        currentPlan = sensor.MakeDecision();
    }

    private void Update()
    {
        sensor.Update();
        currentPlan.Update();
    }
}
