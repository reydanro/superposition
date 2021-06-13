using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VectorUtils
{
    public static Bounds ComputeBoundsFromPoints(Vector2[] points)
    {
        if (points.Length == 0)
        {
            return new Bounds();
        }

        Vector2 min = points[0];
        Vector2 max = points[0];

        foreach (Vector2 v in points)
        {
            min.x = Mathf.Min(min.x, v.x);
            min.y = Mathf.Min(min.y, v.y);

            max.x = Mathf.Max(max.x, v.x);
            max.y = Mathf.Max(max.y, v.y);
        }

        return new Bounds((min + max) / 2, max - min);
    }

    public static Bounds ComputeBoundsFromPoints(Vector3[] points)
    {
        if (points.Length == 0)
        {
            return new Bounds();
        }

        Vector3 min = points[0];
        Vector3 max = points[0];

        foreach (Vector3 v in points)
        {
            min.x = Mathf.Min(min.x, v.x);
            min.y = Mathf.Min(min.y, v.y);
            min.z = Mathf.Min(min.z, v.z);

            max.x = Mathf.Max(max.x, v.x);
            max.y = Mathf.Max(max.y, v.y);
            max.z = Mathf.Max(max.z, v.z);
        }

        return new Bounds((min + max) / 2, max - min);
    }
}
