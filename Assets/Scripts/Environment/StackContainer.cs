using System.Collections.Generic;
using UnityEngine;

public class StackContainer : ContainerComponent
{
    public Stack<SmartObject> storage;

    [SerializeField]
    private ObjectType filter;

    [SerializeField]
    private Color debugColor;

    public override void Init(Entity entity)
    {
        storage = new Stack<SmartObject>(capacity);

        #region DEBUG
        renderer = GetComponent<Renderer>();
        #endregion
    }

    public override void PutIn(SmartObject food)
    {
        storage.Push(food);
        #region DEBUG
        Debug.Log($"Put food into Storage. Elements in container: {storage.Count}");
        SetColor();
        #endregion
    }

    public override SmartObject TakeOut()
    {
        if (storage.Count == 0) return null;
        SmartObject item = storage.Pop();
        #region DEBUG
        Debug.Log($"Took food out of Storage. Elements in container: {storage.Count}");
        SetColor();
        #endregion
        return item;
    }

    public override bool Empty => storage.Count == 0;
    public override int Count => storage.Count;

    public override void Clear() => storage.Clear();

    public override bool CanTake(SmartObject item)
    {
        return item.Type == filter && storage.Count < capacity;
    }

    #region DEBUG
    private Renderer renderer;
    private void SetColor() => renderer.material.color = debugColor * storage.Count / capacity;
    #endregion
}
