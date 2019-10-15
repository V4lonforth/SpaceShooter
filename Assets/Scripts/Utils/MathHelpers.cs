using UnityEngine;

public static class MathHelpers
{
    public static Vector2 RadianToVector2(float radian)
    {
        return new Vector2(Mathf.Cos(radian), Mathf.Sin(radian));
    }

    public static Vector2 DegreeToVector2(float degree)
    {
        return RadianToVector2(degree * Mathf.Deg2Rad);
    }

    public static Vector2 AddSpread(Vector2 direction, float spread)
    {
        return RadianToVector2(Mathf.Atan2(direction.y, direction.x) + UnityEngine.Random.Range(-spread, spread));
    }
}