using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blob : ActionEntity
{
    protected override void Awake()
    {
        base.Awake();

        GetComponent<HealthComponent>(ComponentIDs.HEALTH).OnDeath += Death;
    }

    private void Death()
    {
        Destroy(gameObject);
    }
}
