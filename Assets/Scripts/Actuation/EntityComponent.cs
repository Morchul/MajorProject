using UnityEngine;

public abstract class EntityComponent : MonoBehaviour
{
    public abstract int ID { get; }

    public virtual void Init(Entity entity) { }
    public virtual ActionComponent[] GetComponentActions() => null;
}
