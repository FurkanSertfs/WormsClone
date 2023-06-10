using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowNode : MonoBehaviour
{
    public Transform targetNode;

    private void Update() {
        if(Vector2.Distance(transform.position,targetNode.position) >1)
        {
                    transform.position = Vector3.MoveTowards(transform.position, targetNode.position,5 * Time.deltaTime);

        }
    }
    
       
    
}
