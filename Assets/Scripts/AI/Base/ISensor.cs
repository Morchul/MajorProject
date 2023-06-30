public interface ISensor
{
    //Update continues Sensors which have to be checked every frame
    public void Update();

    //Check trigger Sensors, type can specify which type Triggered to ignore not important sensors
    public void Trigger(int type);

    //Sensors determine what to do next
    public IDecision MakeDecision();
}
