using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    [System.Serializable]
    public struct SpawnDetails
    {
        public SmartObject Prefab;
        public float Interval;
        private float timer;

        public bool ShouldSpawn(float dt)
        {
            if ((timer += dt) > Interval)
            {
                timer = 0;
                return true;
            }
            return false;
        }
    }

    [SerializeField]
    private SpawnDetails[] spawnItems;

    private void Awake()
    {
        //foreach (SpawnDetails spawnDetail in spawnItems)
        //{
        //    if (spawnDetail.ShouldSpawn(Time.deltaTime))
        //    {
        //        Spawn(spawnDetail.Prefab);
        //    }
        //}
    }

    private void Update()
    {
        for (int i = 0; i < spawnItems.Length; ++i)
        {
            if (spawnItems[i].ShouldSpawn(Time.deltaTime))
            {
                Spawn(spawnItems[i].Prefab);
            }
        }
    }

    private void Spawn(SmartObject prefab)
    {
        Instantiate(prefab, Utility.RandomVector2(-10, 10, -10, 10).ToVector3_XZ(), Quaternion.identity);
    }
}
