using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class VirtualJoystick : InteractableUI
{
    public Image backgroundImage;
    public Image joystickImage;

    public Vector2 Axes { get; private set; }

    protected override void Drag(PointerEventData eventData)
    {
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(backgroundImage.rectTransform, eventData.position, eventData.pressEventCamera, out Vector2 localPoint))
        {
            localPoint = (localPoint / backgroundImage.rectTransform.sizeDelta + backgroundImage.rectTransform.pivot) * 2f - Vector2.one;

            float length = localPoint.magnitude;
            if (length > 1f)
                localPoint /= length;

            Axes = localPoint;
            joystickImage.rectTransform.anchoredPosition = localPoint * backgroundImage.rectTransform.sizeDelta / 2f;
        }
    }

    protected override void Press(PointerEventData eventData)
    {
        Drag(eventData);
    }

    protected override void Release(PointerEventData eventData)
    {
        joystickImage.rectTransform.anchoredPosition = Vector2.zero;
        Axes = Vector2.zero;
    }
}