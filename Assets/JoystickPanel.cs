using UnityEngine;
using UnityEngine.EventSystems;

public class JoystickPanel : MonoBehaviour, IPointerDownHandler,IDragHandler
{
    public RectTransform targetImageRectTransform;

    [SerializeField]
    JoystickController joystickController;

    public void OnPointerDown(PointerEventData eventData)
    {
        // Tıklanan noktanın Canvas içindeki konumunu al
        Vector2 localPoint;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            targetImageRectTransform.parent as RectTransform,
            eventData.position,
            eventData.pressEventCamera,
            out localPoint);

        // Hedef Image'ı tıklanan noktaya taşı
        targetImageRectTransform.localPosition = localPoint;
        //joystickController. OnDrag(eventData);
    }
    public virtual void OnDrag(PointerEventData eventData)
    {
        joystickController. OnDrag(eventData);
    }
}
