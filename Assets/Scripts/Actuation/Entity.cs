using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Entity : MonoBehaviour
{
    protected EntityComponent[] components;
 
    protected virtual void Awake()
    {
        components = GetComponents<EntityComponent>();

        foreach (EntityComponent component in components)
            component.Init(this);
    }

    public T GetComponent<T>(int componentID) where T : EntityComponent
    {
        foreach (EntityComponent component in components)
            if (component.ID == componentID)
                return (T)component;
        return null;
    }

    public bool TryGetComponent<T>(int componentID, out T entityComponent) where T : EntityComponent
    {
        entityComponent = GetComponent<T>(componentID);
        return entityComponent != null;
    }

    public bool HasComponent(int componentID)
    {
        foreach (EntityComponent component in components)
            if (component.ID == componentID)
                return true;
        return false;
    }
}
