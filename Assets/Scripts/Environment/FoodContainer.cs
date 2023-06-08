using UnityEngine;
using System.Collections.Generic;

public class FoodContainer : MonoBehaviour
{
    public Stack<Food> foodStored;

    private void Awake()
    {
        foodStored = new Stack<Food>();
    }

    public void AddFood(Food food)
    {
        foodStored.Push(food);
    }

    public Food TakeFood()
    {
        return foodStored.Pop();
    }

    public bool HasFood() => foodStored.Count > 0;
}
