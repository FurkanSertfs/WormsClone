using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowThePlayer : MonoBehaviour
{
    [SerializeField]
    Transform target;

    private void Update()
    {

        if (transform.position.y > 94)
        {
            transform.position = new Vector3(transform.position.x, 94, -10);
        }
        else if (transform.position.y < 6)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, -10);
        }

        else if (transform.position.x > 89)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, -10);
        }

        else if (transform.position.x < 7)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, -10);
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(target.position.x, target.position.y, -10), 5 * Time.deltaTime);

        }





    }
}
