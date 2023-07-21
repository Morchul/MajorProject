using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    [System.Serializable]
    public struct SpawnDetails
    {
        public Entity Prefab;
        public float Interval;
        public bool OneTime;
        private float timer;
        private bool spawned;

        public bool ShouldSpawn(float dt)
        {
            if (spawned) return false;

            if ((timer += dt) > Interval)
            {
                timer = 0;
                if (OneTime) spawned = true;
                return true;
            }
            return false;
        }

        public void Init()
        {
            spawned = false;
        }
    }

    [SerializeField]
    private Transform spawnPos;

    [SerializeField]
    private bool randomSpawnPos;

    [SerializeField]
    private SpawnDetails[] spawnItems;

    private void Awake()
    {
        for (int i = 0; i < spawnItems.Length; ++i)
        {
            spawnItems[i].Init();
        }
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

    private void Spawn(Entity prefab)
    {
        Vector3 spawnPos = (randomSpawnPos) ? Utility.RandomVector2(-10, 10, -10, 10).ToVector3_XZ() : this.spawnPos.position;

        Instantiate(prefab, spawnPos, Quaternion.identity);
    }
}
