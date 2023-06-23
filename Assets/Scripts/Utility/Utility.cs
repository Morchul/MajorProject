using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Utility
{
    public static Vector2 RandomVector2(float xMin, float xMax, float yMin, float yMax) => new Vector2(Random.Range(xMin, xMax), Random.Range(yMin, yMax));
}
