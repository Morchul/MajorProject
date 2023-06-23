public struct DecisionModifier
{
    public static DecisionModifier Default = new DecisionModifier() { ModifierInfluence = 0, ModifierTime = 0 };

    public float ModifierInfluence { get; set; } //Influence of the modifier on max time

    private float modifierTimeInfluence; //rest time until modifier reaches 0

    private float inverseModifierTime; //inverse duration: 1 / ModifierTime
    private float modifierTime;
    public float ModifierTime //Duration until modifier reaches 0
    {
        get => modifierTime;
        set
        {
            modifierTime = value;
            inverseModifierTime = 1 / value;
        }
    }

    public void Set(float modifierInfluence, float modifierTime)
    {
        ModifierInfluence = modifierInfluence;
        ModifierTime = modifierTime;
    }

    public float Update(float deltaTime)
    {
        modifierTimeInfluence -= deltaTime;
        return modifierTimeInfluence > 0 ?
            ModifierInfluence * modifierTimeInfluence * inverseModifierTime :
            0;
    }

    public void Ready()
    {
        modifierTimeInfluence = ModifierTime;
    }
}
