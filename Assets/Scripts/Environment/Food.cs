using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : SmartObject 
{
    [SerializeField]
    private float amount;
    public float Amount => amount;

    public override ItemType Type => ItemType.FOOD;

    public EatAction eatAction;
    public PickUpAction PickUpAction;

    private void Awake()
    {
        eatAction = new EatAction(Amount, Destroy);
        PickUpAction = new PickUpAction(this, Destroy);
    }

    private void Destroy()
    {
        Destroy(this.gameObject);
    }
}
