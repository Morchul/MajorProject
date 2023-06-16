using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StackContainer : ContainerComponent
{
    public Stack<ISmartObject> foodStored;

    protected void Awake()
    {
        foodStored = new Stack<ISmartObject>(capacity);

        #region DEBUG
        renderer = GetComponent<Renderer>();
        #endregion
    }

    public override void PutIn(ISmartObject food)
    {
        foodStored.Push(food);
        #region DEBUG
        Debug.Log($"Put food into Storage. Elements in container: {foodStored.Count}");
        SetColor();
        #endregion
    }

    public override ISmartObject TakeOut()
    {
        if (foodStored.Count == 0) return null;
        ISmartObject item = foodStored.Pop();
        #region DEBUG
        Debug.Log($"Took food out of Storage. Elements in container: {foodStored.Count}");
        SetColor();
        #endregion
        return item;
    }

    public override bool Empty => foodStored.Count == 0;

    public override bool CanTake(ISmartObject item) => foodStored.Count < capacity;

    #region DEBUG
    private Renderer renderer;
    private void SetColor() => renderer.material.color = new Color(0, (float)foodStored.Count / capacity, 0);
    #endregion
}
