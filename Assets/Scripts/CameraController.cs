using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform target; // Takip edilecek hedef
    public float smoothSpeed = 0.125f; // Kamera hareketinin yumuşatma hızı
    public Vector2 offset;

    private Vector3 velocity = Vector3.zero;

    private void LateUpdate()
    {
        if (target != null)
        {
            // Hedefin pozisyonunu alarak kamera pozisyonunu hesapla
            Vector3 desiredPosition = new Vector3(target.position.x + offset.x, target.position.y + offset.y, transform.position.z);
            Vector3 smoothedPosition = Vector3.SmoothDamp(transform.position, desiredPosition, ref velocity, smoothSpeed);
            
            // Hesaplanan pozisyonu uygula
            transform.position = smoothedPosition;
        }
    }
}
