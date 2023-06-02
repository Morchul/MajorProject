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
        if(sensor != null)
            currentPlan = sensor.MakeDecision();
    }

    private void Update()
    {
        sensor.Update();
        if(currentPlan != null)
            currentPlan.Update();
    }
}
