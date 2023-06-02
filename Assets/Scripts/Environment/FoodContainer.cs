using UnityEngine;

public class FoodContainer : MonoBehaviour
{
    public float StoredFood { get; private set; }

    public void AddFood(float amount)
    {
        StoredFood += amount;
    }

    public float TakeFood(float maxAmount)
    {
        float amountTaken = Mathf.Min(maxAmount, StoredFood);
        StoredFood -= amountTaken;
        return amountTaken;
    }
}
