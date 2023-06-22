using UnityEngine;

public abstract class EntityComponent : MonoBehaviour
{
    public abstract int ID { get; }

    public abstract void Init(Entity entity);
    public abstract ActionComponent[] GetComponentActions();
}
