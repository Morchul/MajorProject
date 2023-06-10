using UnityEngine;
using System.Collections.Generic;

public class FoodContainer : MonoBehaviour, IContainer<Food>
{
    public Stack<Food> foodStored;

    [SerializeField]
    private int capacity;

    public PutInAction PutInAction;
    public TakeOutAction TakeOutAction;

    #region DEBUG
    private Renderer renderer;
    #endregion

    private void Awake()
    {
        foodStored = new Stack<Food>();
        PutInAction = new PutInAction(this, null);
        TakeOutAction = new TakeOutAction(this, null);

        renderer = GetComponent<Renderer>();
    }

    public void PutIn(Food food)
    {
        Debug.Log($"Put food into Storage. Elements in container: {foodStored.Count}");
        foodStored.Push(food);

        SetColor();
    }

    public Food TakeOut()
    {
        Food item = foodStored.Pop();
        SetColor();
        return item;
    }

    public bool Empty => foodStored.Count == 0;
    public bool CanTake(Food item) => foodStored.Count < capacity;

    #region DEBUG
    private void SetColor() => renderer.material.color = new Color(0, (float)foodStored.Count / capacity, 0);
    #endregion
}
