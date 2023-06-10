using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IItem
{
    public ItemType Type { get; }
}

public enum ItemType
{
    FOOD,

}
