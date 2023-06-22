using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemComponent : EntityComponent
{
    public override int ID => ComponentIDs.ITEM;

    public override ActionComponent[] GetComponentActions()
    {
        return new ActionComponent[]
        {
            new ActionComponent()
            {
                ActionID = ActionID.PICK_UP,
                MaxContainerSize = 0,
                StartContainerSize = 0
            }
        };
    }

    public override void Init(Entity entity) { }

    public SmartObject PickUpItem()
    {
        Item.SetToItem();
        return Item;
    }

    [SerializeField]
    private SmartObject Item;
    public int ItemID;
    public Sprite Icon;
    public bool Stackable;

}
