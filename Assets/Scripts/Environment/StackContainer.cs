using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StackContainer : ContainerComponent
{
    public Stack<SmartObject> foodStored;

    protected void Awake()
    {
        foodStored = new Stack<SmartObject>(capacity);

        #region DEBUG
        renderer = GetComponent<Renderer>();
        #endregion
    }

    public override void PutIn(SmartObject food)
    {
        foodStored.Push(food);
        #region DEBUG
        Debug.Log($"Put food into Storage. Elements in container: {foodStored.Count}");
        SetColor();
        #endregion
    }

    public override SmartObject TakeOut()
    {
        if (foodStored.Count == 0) return null;
        SmartObject item = foodStored.Pop();
        #region DEBUG
        Debug.Log($"Took food out of Storage. Elements in container: {foodStored.Count}");
        SetColor();
        #endregion
        return item;
    }

    public override bool Empty => foodStored.Count == 0;

    public override bool CanTake(SmartObject item) => foodStored.Count < capacity;

    #region DEBUG
    private Renderer renderer;
    private void SetColor() => renderer.material.color = new Color(0, (float)foodStored.Count / capacity, 0);
    #endregion
}
