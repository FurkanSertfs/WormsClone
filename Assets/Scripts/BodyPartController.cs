using UnityEngine;

public class BodyPartController : MonoBehaviour
{
    public Transform target; 

    public float speed = 5f;
   
    public float maxDistance = 0.85f;



    

  

    private void Update()
    {
        if (target != null)
        {
            if (Vector2.Distance(transform.position, target.position) > maxDistance)
            {
                MoveTowardsTarget();
            }
        }
    }


    private void MoveTowardsTarget()
    {
        transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
    }
}
