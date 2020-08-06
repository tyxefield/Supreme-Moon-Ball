using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VectorData
{
    private static Vector3 v;
    private static Vector2 v2;
    public static Vector2 zero = Vector3.zero;
    public static Vector2 one = Vector3.one;

    public static Vector3 Set(float x, float y, float z)
    {
        v.x = x;
        v.y = y;
        v.z = z;
        return v;
    }
    public static Vector2 Set2D(float x, float y)
    {
        v2.x = x;
        v2.y = y;
        return v2;
    }

    public static float GetDistance(Vector3 p, Vector3 p2)
    {
        float x = p.x - p2.x;
        float y = p.y - p2.y;
        float z = p.z - p2.z;
        return x * x + y * y + z * z;
    }
}
