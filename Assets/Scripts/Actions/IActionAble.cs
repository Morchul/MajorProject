using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Base interface for everything that can execute IAction<>
public interface IActionAble<out T>
{
    public void AddAction(IAction<T> action);
}
