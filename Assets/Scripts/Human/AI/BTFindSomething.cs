using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class BTFindSomething
{
    public static T SearchClosest<T>(string tag, Vector3 pos, float radius) where T : MonoBehaviour
    {
        Collider[] colliders = Physics.OverlapSphere(pos, radius);
        if (colliders.Length > 0)
        {
            int closestIndex = 0;
            float closestDistanceSqrt = 100_000;
            for (int i = 0; i < colliders.Length; ++i)
            {
                if (!colliders[i].CompareTag(tag)) continue;

                float distance = (pos - colliders[i].transform.position).sqrMagnitude;
                if (distance < closestDistanceSqrt)
                {
                    closestIndex = i;
                    closestDistanceSqrt = distance;
                }
            }

            return colliders[closestIndex].GetComponent<T>();
        }
        return default;
    }
}
