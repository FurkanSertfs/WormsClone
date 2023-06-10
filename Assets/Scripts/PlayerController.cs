using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    float speed;
    [SerializeField]
    GameObject bodyPrefab;
    [SerializeField]
    Transform tail;

    private void Start() 
    {
        CreateNewBody();
    }

    private void OnEnable() {
        GameEvents.instance.OnFoodDestroyed += CreateNewBody;
    }
   
    void Update()
    {
        
        transform.Translate(Vector3.up * Time.deltaTime * speed);
        
    }

    void CreateNewBody()
    {
        GameObject newTail =  Instantiate(bodyPrefab, new Vector2(tail.position.x,tail.position.y), Quaternion.identity);
        newTail.GetComponent<FollowNode>().targetNode = tail;
        tail = newTail.transform;
    }

   

   
}
