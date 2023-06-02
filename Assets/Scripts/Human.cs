using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Human : MonoBehaviour
{
    public float Food { get; private set; }

    public void Eat()
    {
        Food = Mathf.Min(Food + 10, 100);
    }

    private void Update()
    {
        Food -= Time.deltaTime;
    }
}
