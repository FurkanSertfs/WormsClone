using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    float speed = 5f;

    [SerializeField]
    JoystickController joystick;


    [SerializeField]
    Transform controlStick;

    [SerializeField]
    float rotationSpeed;

    Vector3 direction;

    public int score;

    [SerializeField]
    ComboText comboText;

    [SerializeField]
    Text scoreText;

    bool isSprinting;

    public float sprintTime;

    [SerializeField]
    Sprite[] sprites;

    [SerializeField]
    Transform comboTextParent;

    private void OnEnable()
    {
        GameEvents.instance.OnUpdateScore += UpdateScore;
        GameEvents.instance.OnSprintButtonPressed += SprintButtonPressed;
    }

    private void OnDisable()
    {
        GameEvents.instance.OnUpdateScore -= UpdateScore;
        GameEvents.instance.OnSprintButtonPressed -= SprintButtonPressed;
    }

    private void Awake() 
    {
        int wormsCount= PlayerPrefs.GetInt("character");
        GetComponent<SpriteRenderer>().sprite = sprites[wormsCount];
        List<BodyPartController> bodyParts = GetComponent<WormController>().bodyParts;

        for (int i = 0; i < bodyParts.Count; i++)
        {
            bodyParts[i].GetComponent<SpriteRenderer>().sprite = sprites[wormsCount+5];
        }

        
    }

    private void Start()
    {
        direction = new Vector3(0, 1, 0);
    }

    private void Update()
    {
        float horizontal = joystick.Horizontal();
        float vertical = joystick.Vertical();



        direction = new Vector3(horizontal, vertical, 0);

        

        if (direction.magnitude > 0.1f)
        {
            Quaternion targetRotation = Quaternion.LookRotation(Vector3.forward, direction);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
            controlStick.rotation = Quaternion.RotateTowards(controlStick.rotation, targetRotation, rotationSpeed * 1.5f * Time.deltaTime);

        }

        transform.position += transform.up * speed * Time.deltaTime;
    }

    void UpdateScore(int addScore)
    {
        score += addScore;



        scoreText.text = "Score: " + score.ToString();
        Instantiate(comboText, comboTextParent).ShowScoreText(addScore);
        



    }
   

    void SprintButtonPressed(bool isPressed)
    {
        if (score > 30)
        {
            List<BodyPartController> bodyParts = GetComponent<WormController>().bodyParts;
            isSprinting = isPressed;

            if (isPressed)
            {
                speed = 10f;



                for (int i = 0; i < bodyParts.Count; i++)
                {
                    bodyParts[i].speed = 10f;
                }

                StartCoroutine(DecresingTheScore());
            }
            else
            {
                speed = 5f;
                for (int i = 0; i < bodyParts.Count; i++)
                {
                    bodyParts[i].speed = 5f;
                }

            }

        }


    }

    IEnumerator DecresingTheScore()
    {
        int totalDecrease = 0;
        while (isSprinting &&score-totalDecrease>30)
        {
            yield return new WaitForSeconds(0.25f);
            sprintTime += 0.25f;
            totalDecrease -= 30;


        }
        UpdateScore(totalDecrease);
    }










}
