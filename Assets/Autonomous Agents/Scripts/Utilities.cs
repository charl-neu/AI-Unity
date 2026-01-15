using UnityEngine;

public class Utilities : MonoBehaviour
{
    public static float Wrap(float y, float min, float max)
    {
        float range = max - min;
        if (range <= 0) return min; // Avoid division by zero
        while (y < min)
        {
            y += range;
        }
        while (y >= max)
        {
            y -= range;
        }
        return y;
    }

    public static Vector3 Wrap(Vector3 v, Vector3 min, Vector3 max)
    {
        return new Vector3(
            Wrap(v.x, min.x, max.x),
            Wrap(v.y, min.y, max.y),
            Wrap(v.z, min.z, max.z)
        );
    }
}
