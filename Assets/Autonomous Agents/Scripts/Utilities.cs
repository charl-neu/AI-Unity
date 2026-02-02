using System;
using UnityEngine;

public class Utilities : MonoBehaviour
{
    public static Color white = new(1, 1, 1, 0.5f);
    public static Color green = new(0, 1, 0, 0.5f);
    public static Color red = new(1, 0, 0, 0.5f);
    public static Color blue = new(0, 0, 1, 0.5f);

    public static Color ColorAlpha(Color color, float alpha = 0.5f)
    {
        return new Color(color.r, color.g, color.b, alpha);
    }

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



    public static Vector3[] GetDirectionsInCircle(int num, float halfAngle)
    {
        if (num <= 0) return Array.Empty<Vector3>();
        if (num == 1) return new Vector3[] { Vector3.forward };

        // create array of vector3
        Vector3[] result = new Vector3[num];
        int index = 0;

        // set forward direction if odd number
        if (num % 2 != 0)
	    {
            result[index++] = Vector3.forward;
            num--;
        }

        // compute angle between rays (angle * 2 / num rays - 1)
        float angleOffset = ((halfAngle * 2 ) / (num));

        // add directions symmetrically around the circle
        for (int i = 1; i <= num / 2; i++)
        {
            result[index++] = Quaternion.AngleAxis(+angleOffset * i, Vector3.up) * Vector3.forward;
            result[index++] = Quaternion.AngleAxis(-angleOffset * i, Vector3.up) * Vector3.forward;
        }

        return result;
    }
}
