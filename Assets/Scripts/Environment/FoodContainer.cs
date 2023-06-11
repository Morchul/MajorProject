using UnityEngine;
using System.Collections.Generic;

public class FoodContainer : SmartObject, IContainer<Food>
{
    public Stack<Food> foodStored;

    [SerializeField]
    private int capacity;

    #region DEBUG
    private Renderer renderer;
    #endregion

    private void Awake()
    {
        foodStored = new Stack<Food>();

        actions = new IAction[] { new PutInAction(this, null), new TakeOutAction(this, null) };

        renderer = GetComponent<Renderer>();
    }

    public void PutIn(Food food)
    {
        foodStored.Push(food);
        Debug.Log($"Put food into Storage. Elements in container: {foodStored.Count}");

        SetColor();
    }

    public Food TakeOut()
    {
        if (foodStored.Count == 0) return null;
        Food item = foodStored.Pop();
        Debug.Log($"Took food out of Storage. Elements in container: {foodStored.Count}");
        SetColor();
        return item;
    }

    public bool Empty => foodStored.Count == 0;

    public override ObjectType Type => ObjectType.FOOD; //TODO change

    public bool CanTake(Food item) => foodStored.Count < capacity;

    #region DEBUG
    private void SetColor() => renderer.material.color = new Color(0, (float)foodStored.Count / capacity, 0);
    #endregion
}
