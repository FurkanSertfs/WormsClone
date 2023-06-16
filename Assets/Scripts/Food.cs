using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using System;

public class Food : MonoBehaviour
{
    CollisionController collisionController;

    FoodCircleCollider circleCollider;


    [SerializeField]
    List<int> foodInSquare = new List<int>();

    int controller;

    private void Awake()
    {
        collisionController = CollisionController.instance;

    }

    private void Start()
    {
       
        circleCollider = new FoodCircleCollider(1,gameObject);
        circleCollider.center = transform.position;
        UpdateTheSquare();

       

    }

    public void FollowTheWorm(Transform target)
    {   
        StartCoroutine(MoveToTarget(transform.position,target,0.2f));
       

        


            
            
    }
    private IEnumerator MoveToTarget(Vector2 startPosition,Transform targetPosition,float duration)
    {
        float elapsedTime = 0f;
        while (elapsedTime < duration)
        {
            float t = elapsedTime / duration;
            transform.position = Vector2.Lerp(startPosition, targetPosition.position, t);
            transform.localScale = Vector2.Lerp(transform.localScale, transform.localScale/2, t/5);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.position = targetPosition.position;
        Destroy(gameObject, 0.25f);
    }

    

   


    void UpdateTheSquare()
    {
        for (int i = 0; i < collisionController.squares.Count; i++)
        {
            if (Vector2.SqrMagnitude(circleCollider.center - collisionController.squares[i]) < Mathf.Pow(circleCollider.radius + collisionController.diagonalLineLength / 2f, 2))

            {

                collisionController.squareFoodDictionary[i].CircleColliderCenterList.Add(circleCollider);

               // Debug.Log(collisionController.squareFoodDictionary[i].CircleColliderCenterList.Count+" food sayisi i: "+i);

                foodInSquare.Add(i);
            }

        }

       
        
    }


}
