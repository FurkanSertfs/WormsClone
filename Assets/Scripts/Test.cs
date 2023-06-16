using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    private void Update() {
        if (Input.GetKeyDown(KeyCode.T)) 
        {
            transform.localScale *=2 ;
        }
    }
}