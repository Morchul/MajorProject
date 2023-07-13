using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blob : Agent
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
