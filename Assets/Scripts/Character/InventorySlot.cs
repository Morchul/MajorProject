using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventorySlot
{
    public SmartObject Item { get; private set; }

    public int Amount { get; private set; }

    public bool IsEmpty => Item == null;

    public void Set(SmartObject smartObject)
    {
        Item = smartObject;
    }

    public void TryStack(SmartObject smartObject)
    {
        if (Item.ItemComponent.ItemID != smartObject.ItemComponent.ItemID) return;

        IncreaseStack();
    }

    public void IncreaseStack()
    {
        ++Amount;
    }
}
