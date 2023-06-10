using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ScoreBoard : MonoBehaviour
{
    float comboTimer;

    int score;



    [SerializeField]
    Text scoreText;

    private void OnEnable()
    {
        GameEvents.instance.OnFoodDestroyed += UpdateScore;
    }

    private void Update()
    {
        if (comboTimer > 0)
        {
            comboTimer -= Time.deltaTime;
        }
        else
        {
            score = 0;
        }
    }

    void UpdateScore()
    {

        comboTimer += 1;

        if(comboTimer>1)
        {
              score += (int)(12*comboTimer);
        }
        
      
        scoreText.text = score.ToString();
    }
}
