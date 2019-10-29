using UnityEngine;

public static class MathHelpers
{
    private const float PI = Mathf.PI * Mathf.Rad2Deg;
    private const float PI2 = PI * 2f;

    public static Vector2 RadianToVector2(float radian)
    {
        return new Vector2(Mathf.Cos(radian), Mathf.Sin(radian));
    }

    public static Vector2 DegreeToVector2(float degree)
    {
        return RadianToVector2(degree * Mathf.Deg2Rad);
    }

    public static float Vector2ToRadian(Vector2 vector)
    {
        return Mathf.Atan2(vector.y, vector.x);
    }

    public static float Vector2ToDegree(Vector2 vector)
    {
        return Vector2ToRadian(vector) * Mathf.Rad2Deg;
    }

    public static Vector2 AddDegreeSpread(Vector2 direction, float spread)
    {
        return AddRadianSpread(direction, Mathf.Deg2Rad * spread);
    }

    public static Vector2 AddRadianSpread(Vector2 direction, float spread)
    {
        return RadianToVector2(Mathf.Atan2(direction.y, direction.x) + Random.Range(-spread, spread));
    }

    public static float LerpAngle(float a, float b, float speed)
    {
        if (a > b)
        {
            if (a - b > PI)
            {
                a += speed;
                if (a > PI)
                {
                    a -= PI2;
                    if (a > b)
                        a = b;
                }
            }
            else
            {
                a -= speed;
                if (a < b)
                    a = b;
                else if (a < -PI)
                    a += PI2;
            }
        }
        else
        {
            if (b - a > PI)
            {
                a -= speed;
                if (a < -PI)
                {
                    a += PI2;
                    if (a < b)
                        a = b;
                }
            }
            else
            {
                a += speed;
                if (a > b)
                    a = b;
                else if (a > PI)
                    a -= PI2;
            }
        }
        return a;
    }

    public static Vector2 LerpAngle(Vector2 a, Vector2 b, float speed)
    {
        float angle = LerpAngle(Mathf.Atan2(a.y, a.x), Mathf.Atan2(b.y, b.x), speed);
        return new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));
    }
}