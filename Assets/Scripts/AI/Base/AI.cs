using UnityEngine;

public abstract class AI : MonoBehaviour
{
    public ISensor Sensor { get; protected set; }

    protected IDecision currentDecision;

    protected virtual void Start()
    {
        MakeNewDecision();
    }

    public void MakeNewDecision()
    {
        if(Sensor != null)
        {
            if(currentDecision != null)
                currentDecision.Stop();
            currentDecision = Sensor.MakeDecision();
            currentDecision.Select();
            //Debug.Log($"Next decision: {currentDecision}");
        }
    }

    private void Update()
    {
        Sensor.Update();
        if(currentDecision != null)
            currentDecision.Update();
    }
}
