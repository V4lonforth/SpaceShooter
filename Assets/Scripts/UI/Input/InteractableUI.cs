using UnityEngine;
using UnityEngine.EventSystems;

public class InteractableUI : MonoBehaviour, IDragHandler, IPointerUpHandler, IPointerDownHandler
{
    public InteractableUIType type;

    public int PointerId { get; protected set; }
    public bool IsPressed { get; protected set; }

    public void OnDrag(PointerEventData eventData)
    {
        if (PointerId == eventData.pointerId)
        {
            Press(eventData);
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        IsPressed = true;
        PointerId = eventData.pointerId;
        Drag(eventData);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        IsPressed = false;
        Release(eventData);
    }

    protected virtual void Press(PointerEventData eventData) { }
    protected virtual void Drag(PointerEventData eventData) { }
    protected virtual void Release(PointerEventData eventData) { }
}