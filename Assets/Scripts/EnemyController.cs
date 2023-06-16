using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{

    Vector3 direction;

    [SerializeField]
    bool isRotating,isActive,isDebugging;



    private void Start()
    {
        direction = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), 0);

       
    }

    IEnumerator RotateRoutine()
    {
        isRotating = true;
        yield return new WaitForSeconds(1.25f);
        isRotating = false;


    }

    public void RotateDirection(bool turnBack)
    {

        if (!isRotating)
        {
            if (turnBack)
            {
                direction = -direction;

               // Debug.Log(direction);

               
            }

            else
            {
                direction = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), 0);
            }

            StartCoroutine(RotateRoutine());
        }

       






    }




    private void Update()
    {


        if (direction.magnitude > 0.1f)
        {
            Quaternion targetRotation = Quaternion.LookRotation(Vector3.forward, direction);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, 200 * Time.deltaTime);


        }

        transform.position += transform.up * 5 * Time.deltaTime;
    }
}

