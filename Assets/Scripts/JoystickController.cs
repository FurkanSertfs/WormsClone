using UnityEngine;
using UnityEngine.EventSystems;

public class JoystickController : MonoBehaviour, IDragHandler, IPointerUpHandler, IPointerDownHandler
{
    private RectTransform joystickBackground;
    private RectTransform joystickHandle;
    private Vector3 inputVector;

    public bool isFirstClick=true;

    private void Start()
    {
        joystickBackground = GetComponent<RectTransform>();
        joystickHandle = transform.GetChild(0).GetComponent<RectTransform>();
    }

   public virtual void OnDrag(PointerEventData eventData)
{
    Vector2 position;
    if (RectTransformUtility.ScreenPointToLocalPointInRectangle(joystickBackground, eventData.position, eventData.pressEventCamera, out position))
    {
        position.x = (position.x / joystickBackground.sizeDelta.x);
        position.y = (position.y / joystickBackground.sizeDelta.y);

        float x = (joystickBackground.pivot.x == 1f) ? position.x * 2 + 1 : position.x * 2 - 1;
        float y = (joystickBackground.pivot.y == 1f) ? position.y * 2 + 1 : position.y * 2 - 1;

        inputVector = new Vector3(x, 0, y);
        inputVector = (inputVector.magnitude > 1.0f) ? inputVector.normalized : inputVector;

        joystickHandle.anchoredPosition = new Vector3(inputVector.x * (joystickBackground.sizeDelta.x / 3), inputVector.z * (joystickBackground.sizeDelta.y / 3));

        if (isFirstClick)
        {
            isFirstClick = false;
            joystickBackground.position = eventData.position;
        }
    }
}


    public virtual void OnPointerDown(PointerEventData eventData)
    {
        OnDrag(eventData);
    }

    public virtual void OnPointerUp(PointerEventData eventData)
    {
        inputVector = Vector3.zero;
        
        joystickHandle.anchoredPosition = Vector3.zero;
    }

    public float Horizontal()
    {
        return inputVector.x;
    }

    public float Vertical()
    {
        return inputVector.z;
    }
}