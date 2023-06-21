using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tree : SmartObject
{
    [SerializeField]
    private Wood woodPrefab;

    [field: SerializeField]
    public int WoodAmount { private get; set; }

    protected override void Awake()
    {
        base.Awake();

        GetComponent<HealthComponent>(ComponentIDs.HEALTH).OnDeath += Death;
    }

    private void Death()
    {
        Destroy(this.gameObject);

        for(int i = 0; i < WoodAmount; ++i)
        {
            Instantiate(woodPrefab, transform.position + Extensions.RandomVector2(-2,2,-2,2).ToVector3_XZ(), Quaternion.identity);
        }
    }
}
