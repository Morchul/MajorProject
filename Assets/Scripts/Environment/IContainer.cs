using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IContainer<T>
{
    public bool CanTake(T item);
    public void PutIn(T item);
    public T TakeOut();
    public bool Empty { get; }
}
