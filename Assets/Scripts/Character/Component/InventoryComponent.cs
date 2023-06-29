using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryComponent : EntityComponent
{
    public override int ID => throw new System.NotImplementedException();

    [SerializeField]
    private int inventorySize;

    public Inventory Inventory { get; private set; }

    public override void Init(Entity entity)
    {
        Inventory = new Inventory(inventorySize);
    }
}
