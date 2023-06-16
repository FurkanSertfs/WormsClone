using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEvents : MonoBehaviour
{
    public static GameEvents instance;
    public delegate void OnFoodDestroyedDelegate(Vector2 spawnPoints);

    public delegate void OnUpdateScoreDeleagate(int score);

    public delegate void OnSprintButtonPressedDeleagate(bool isPressed);

    


    public OnFoodDestroyedDelegate OnFoodDestroyed;
    public OnUpdateScoreDeleagate OnUpdateScore;

    public OnSprintButtonPressedDeleagate OnSprintButtonPressed;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

}
