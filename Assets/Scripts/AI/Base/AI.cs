using UnityEngine;

public abstract class AI : MonoBehaviour
{
    protected ISensor sensor;

    protected IDecision currentDecision;

    protected virtual void Start()
    {
        MakeNewDecision();
    }

    public void MakeNewDecision()
    {
        if(sensor != null)
        {
            if(currentDecision != null)
                currentDecision.Stop();
            currentDecision = sensor.MakeDecision();
            currentDecision.Select();
            //Debug.Log($"Next decision: {currentDecision}");
        }
    }

    private void Update()
    {
        sensor.Update();
        if(currentDecision != null)
            currentDecision.Update();
    }
}
