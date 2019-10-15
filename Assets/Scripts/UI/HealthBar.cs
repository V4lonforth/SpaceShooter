using UnityEngine;

public class HealthBar : MonoBehaviour
{
    public string barName;
    public RectTransform bar;

    public void SetHealth(float healthPercent)
    {
        bar.localScale = new Vector3(healthPercent, 1f, 1f);
    }
}
