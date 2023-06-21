using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthComponent : EntityComponent
{
    public float Health;

    public override int ID => ComponentIDs.HEALTH;

    public event System.Action OnDeath;

    public override ActionComponent[] GetComponentActions() => null;

    public void TakeDamage(float damage)
    {
        Debug.Log($"Take damage: {damage}");
        Health -= damage;
        if(Health <= 0)
        {
            OnDeath?.Invoke();
        }
    }
}
