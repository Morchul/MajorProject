using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : MonoBehaviour
{
    [SerializeField]
    private float amount;
    public float Amount => amount;

    private EatAction eatAction;

    private void Awake()
    {
        eatAction = new EatAction(Amount);
    }

    public void EatBy(Human executioner)
    {
        executioner.AddAction(eatAction);
        Destroy(this.gameObject);
    }
}
