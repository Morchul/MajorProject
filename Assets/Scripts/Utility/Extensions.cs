using UnityEngine;

public static class Extensions
{
    public static Vector3 ToVector3_XY(this Vector2 vec2, float z = 0) => new Vector3(vec2.x, vec2.y, z);
    public static Vector3 ToVector3_XZ(this Vector2 vec2, float y = 0) => new Vector3(vec2.x, y, vec2.y);
    public static Vector3 ToVector3_YZ(this Vector2 vec2, float x = 0) => new Vector3(x, vec2.x, vec2.y);
    public static Vector2 ToVector2_XY(this Vector3 vec3) => new Vector2(vec3.x, vec3.y);
    public static Vector2 ToVector2_XZ(this Vector3 vec3) => new Vector2(vec3.x, vec3.z);
    public static Vector2 ToVector2_YZ(this Vector3 vec3) => new Vector2(vec3.y, vec3.z);

    public static Vector3 AddX(this Vector3 vec3, float x) => new Vector3(vec3.x + x, vec3.y, vec3.z);
    public static Vector3 AddY(this Vector3 vec3, float y) => new Vector3(vec3.x, vec3.y + y, vec3.z);
    public static Vector3 AddZ(this Vector3 vec3, float z) => new Vector3(vec3.x, vec3.y, vec3.z + z);

    public static Vector2 AddX(this Vector2 vec2, float x) => new Vector2(vec2.x + x, vec2.y);
    public static Vector2 AddY(this Vector2 vec2, float y) => new Vector2(vec2.x, vec2.y + y);

    public static Vector2 MultiplyX(this Vector2 vec2, float x) => new Vector2(vec2.x * x, vec2.y);
    public static Vector2 MultiplyY(this Vector2 vec2, float y) => new Vector2(vec2.x, vec2.y * y);

    public static Vector2 RandomVector2(float xMin, float xMax, float yMin, float yMax) => new Vector2(Random.Range(xMin, xMax), Random.Range(yMin, yMax));

    public static bool IsActive(this IAction action) => action.Status < ActionState.SLEEPING;
    public static bool IsInactive(this IAction action) => action.Status > ActionState.FINISHED;
}
