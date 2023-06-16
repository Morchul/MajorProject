using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Entity : MonoBehaviour
{
    protected EntityComponent[] components { get; set; }
 
    protected virtual void Awake()
    {
        components = GetComponents<EntityComponent>();
    }

    public T GetComponent<T>(int componentID) where T : EntityComponent
    {
        foreach (EntityComponent component in components)
            if (component.ID == componentID)
                return (T)component;
        return null;
    }
}
