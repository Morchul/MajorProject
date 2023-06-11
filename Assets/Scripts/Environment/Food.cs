using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : SmartObject 
{
    [SerializeField]
    private float amount;
    public float Amount => amount;

    public override ObjectType Type => ObjectType.FOOD;

    private void Awake()
    {
        actions = new IAction[] { new EatAction(Amount, SetInactive), new PickUpAction(this, SetInactive) };
    }

    private void SetInactive()
    {
        gameObject.SetActive(false);
    }
}
