using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEvents : MonoBehaviour
{

    public static GameEvents instance;
 
    public delegate void OnFoodDestroyedDelegate();
    public delegate void OnBodyUpdatedDelegate();

    public OnFoodDestroyedDelegate OnFoodDestroyed;
    public OnBodyUpdatedDelegate OnBodyUpdated;

    private void Awake()
    {
        instance = this;
    }


}
