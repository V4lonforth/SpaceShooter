using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Radar : MonoBehaviour
{
    public GameObject dotPrefab;
    public RectTransform canvas;

    public float screenBorderOffset;

    public float maxDistance;

    private List<GameObject> activeRadarDots;
    private List<GameObject> inactiveRadarDots;

    private RectTransform rectTransform;

    private static Rect viewpointRect = new Rect(0f, 0f, 1f, 1f);
    private const int startDotsAmount = 10;

    private void Awake()
    {
        activeRadarDots = new List<GameObject>(startDotsAmount);
        inactiveRadarDots = new List<GameObject>(startDotsAmount);
        for (int i = 0; i > startDotsAmount; i++)
        {
            GameObject dot = Instantiate(dotPrefab, transform);
            dot.SetActive(false);
            inactiveRadarDots.Add(dot);
        }

        rectTransform = GetComponent<RectTransform>();
    }

    private GameObject ActivateDot()
    {
        if (inactiveRadarDots.Count == 0)
            return Instantiate(dotPrefab, transform);
        GameObject dot = inactiveRadarDots[inactiveRadarDots.Count - 1];
        inactiveRadarDots.RemoveAt(inactiveRadarDots.Count - 1);
        dot.SetActive(true);
        return dot;
    }

    private GameObject GetDot(int index)
    {
        if (index >= activeRadarDots.Count)
        {
            GameObject dot = ActivateDot();
            activeRadarDots.Add(dot);
            return dot;
        }
        else
            return activeRadarDots[index];
    }

    private void DeactivateDots(int amount)
    {
        int resultSize = activeRadarDots.Count - amount;
        for (int i = activeRadarDots.Count - 1; i >= resultSize; i--)
        {
            inactiveRadarDots.Add(activeRadarDots[i]);
            activeRadarDots[i].SetActive(false);
            activeRadarDots.RemoveAt(i);
        }
    }

    private void Update()
    {
        List<AIShip> ships = ShipsController.Instance.Ships;

        int currentDotsAmount = 0;
        foreach (AIShip ship in ships)
        {
            Vector2 direction = ship.transform.position - ShipsController.Instance.PlayerShip.transform.position;
            if (!viewpointRect.Contains(Camera.main.WorldToViewportPoint(ship.transform.position)) && direction.sqrMagnitude <= maxDistance * maxDistance)
            {
                GameObject dot = GetDot(currentDotsAmount);
                currentDotsAmount++;

                RectTransform rectTransform = dot.GetComponent<RectTransform>();
                rectTransform.anchoredPosition = GetPointOnEllipse(direction);//GetPointOnScreenBorder(direction);
                rectTransform.rotation = Quaternion.Euler(0f, 0f, Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg);

                Image image = dot.GetComponent<Image>();
                Color color = image.color;
                color.a = 1 - direction.sqrMagnitude / (maxDistance * maxDistance);
                image.color = color;
            }
        }

        DeactivateDots(activeRadarDots.Count - currentDotsAmount);
    }

    private Vector2 GetPointOnEllipse(Vector2 direction)
    {
        Vector2 size = rectTransform.sizeDelta / 2f;
        float k = direction.y / (size.y * direction.x);
        float x = Mathf.Sqrt(1 / (1 / (size.x * size.x) + k * k));
        if (direction.x < 0)
            x = -x;
        float y = x * direction.y / direction.x;
        return new Vector2(x, y);
    }

    private Vector2 GetPointOnScreenBorder(Vector2 direction)
    {
        Vector2 halfScreenSize = canvas.sizeDelta / 2f - screenBorderOffset * canvas.sizeDelta.x * Vector2.one;
        float x = halfScreenSize.y * (direction.x / direction.y);
        float y = halfScreenSize.y;
        if (direction.y < 0f)
        {
            x = -x;
            y = -y;
        }

        if (x > halfScreenSize.x)
            return new Vector2(halfScreenSize.x, halfScreenSize.x * (direction.y / direction.x));
        else if (x < -halfScreenSize.x)
            return new Vector2(-halfScreenSize.x, -halfScreenSize.x * (direction.y / direction.x));
        
        return new Vector2(x, y);
    }
}