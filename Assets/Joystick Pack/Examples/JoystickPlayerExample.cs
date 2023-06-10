using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoystickPlayerExample : MonoBehaviour
{
    public float speed;
    public VariableJoystick variableJoystick;

    public void FixedUpdate()
    {
        Vector2 direction = Vector2.up * variableJoystick.Vertical + Vector2.right * variableJoystick.Horizontal;
      
        if (direction != Vector2.zero)
        {
            transform.rotation = Quaternion.LookRotation(Vector3.forward, direction);
        }

       
        
    }
}