using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodSpawner : MonoBehaviour
{
    [SerializeField]
    private SmartObject foodPrefab;

    [SerializeField]
    private float interval;

    private float timer;

    private void Awake()
    {
        timer = 0;
    }

    private void Update()
    {
        if((timer += Time.deltaTime) > interval)
        {
            Spawn();
            timer = 0;
        }
    }

    private void Spawn()
    {
        Instantiate(foodPrefab, Extensions.RandomVector2(0, 10, 0, 10).ToVector3_XZ(), Quaternion.identity);
    }
}
