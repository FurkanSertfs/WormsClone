using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowThePlayer : MonoBehaviour
{
    [SerializeField]
    Transform target;

    private void Update()
    {


        transform.position = Vector3.MoveTowards(transform.position, new Vector3(target.position.x,target.position.y,-10), 5 * Time.deltaTime);

       


    }
}
