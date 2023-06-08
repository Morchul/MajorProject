using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : MonoBehaviour 
{
    [SerializeField]
    private float amount;
    public float Amount => amount;

    public EatAction eatAction;

    private void Awake()
    {
        eatAction = new EatAction(Amount, OnEat);
    }

    public void OnEat()
    {
        Destroy(this.gameObject);
    }
}
