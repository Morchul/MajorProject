using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryComponent : EntityComponent
{
    public override int ID => throw new System.NotImplementedException();

    public override ActionComponent[] GetComponentActions() => null;

    [SerializeField]
    private int inventorySize;

    public Inventory Inventory { get; private set; }

    private void Awake()
    {
        Inventory = new Inventory(inventorySize);
    }
}
