using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory
{
    private readonly InventorySlot[] inventory;

    public Inventory(int inventorySize)
    {
        inventory = new InventorySlot[inventorySize];
    }

    public bool TryAddItem(SmartObject smartObject)
    {
        if (!smartObject.CanBeItem) return false;

        ItemComponent itemComponent = smartObject.ItemComponent;

        int slotIndex = -1;
        if (itemComponent.Stackable)
        {
            for (int i = 0; i < inventory.Length; ++i)
            {
                if (slotIndex < 0 && inventory[i].IsEmpty)
                {
                    slotIndex = i;
                }
                if(inventory[i].Item.ItemComponent.ItemID == itemComponent.ItemID) //found a place to stack
                {
                    inventory[i].IncreaseStack();
                    return true;
                }
            }

            if (slotIndex < 0) return false;
            else
            {
                inventory[slotIndex].Set(smartObject);
                return true;
            }
        }
        else
        {
            for (int i = 0; i < inventory.Length; ++i)
            {
                if (inventory[i].IsEmpty)
                {
                    inventory[i].Set(smartObject);
                    return true;
                }
            }
        }

        return false;
    }

    public bool GetItemWithComponent(int componentID, out SmartObject smartObject)
    {
        foreach(InventorySlot slot in inventory)
        {
            if (slot.Item.HasComponent(componentID))
            {
                smartObject = slot.Item;
                return true;
            }
        }

        smartObject = null;
        return false;
    }
}
