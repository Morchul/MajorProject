using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemComponent : EntityComponent
{
    public override int ID => throw new System.NotImplementedException();
    public override ActionComponent[] GetComponentActions() => null;


    public int ItemID;
    public Sprite Icon;
    public bool Stackable;

}
