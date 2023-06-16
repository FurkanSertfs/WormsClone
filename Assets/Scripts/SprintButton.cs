using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class SprintButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public Image image;
    private Color originalColor;

    private void Start()
    {
        originalColor = image.color;
    }

  

    public void OnPointerDown(PointerEventData eventData)
    {
      
        image.color = originalColor * 0.5f;

        GameEvents.instance.OnSprintButtonPressed(true);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
       
        image.color = originalColor;
        GameEvents.instance.OnSprintButtonPressed(false);
    }
}