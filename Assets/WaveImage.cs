using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveImage : MonoBehaviour
{

 public float amplitude = 20f;
    public float speed = 1f;
    private RectTransform rectTransform;
    private Vector2 initialPosition;
    private float startY;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        initialPosition = rectTransform.anchoredPosition;
        startY = initialPosition.y;
    }

    private void Update()
    {
        
        float newY = startY + Mathf.Sin((Time.time) * speed) * amplitude;
        rectTransform.anchoredPosition = new Vector2(rectTransform.anchoredPosition.x, newY);
    }
}
