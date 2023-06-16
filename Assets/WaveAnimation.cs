using UnityEngine;
using UnityEngine.UI;

public class WaveAnimation : MonoBehaviour
{
    public float amplitude = 20f; // Dalga genliği
    public float speed = 5f; // Dalga hızı

    private RectTransform rectTransform;
    private float startY;
    private float offset;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        startY = rectTransform.anchoredPosition.y;
    }

    private void Start()
    {
        int index = transform.GetSiblingIndex(); // Öğenin indeksini al
        float totalOffset = (float)index / (transform.parent.childCount - 1); // Toplam ofset hesapla (0-1 aralığında)

        offset = Mathf.Lerp(-1f, 1f, totalOffset); // -1 ve 1 arasında ofset değeri hesapla
    }

    private void Update()
    {
        // Yeni pozisyonu hesapla
        float newY = startY + Mathf.Sin((Time.time + offset) * speed) * amplitude;
        rectTransform.anchoredPosition = new Vector2(rectTransform.anchoredPosition.x, newY);
    }
}
